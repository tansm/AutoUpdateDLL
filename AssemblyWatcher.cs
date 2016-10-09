using System;
using System.IO;
using System.Threading;
using System.Text;

namespace AutoUpdate {

    //观察路径下的dll变更，在更新后的2秒左右刷新结果。
    class AssemblyWatcher : IDisposable {

        private string _path;
        public string Path {
            get { return _path; }
            set {
                if (_path != value) {
                    UnhookFiles();
                    _path = value;
                    HookFiles();
                }
            }
        }
        private FileSystemWatcher _watcher;
        private Timer _timer;

        public event EventHandler Changed;

        private void HookFiles() {
            if (string.IsNullOrEmpty(_path) ||
                !Directory.Exists(_path)) {
                return;
            }

            _watcher = new FileSystemWatcher(_path, "*.dll");
            _watcher.Changed += _watcher_Changed;
            _watcher.Created += _watcher_Changed;
            _watcher.Deleted += _watcher_Changed;
            _watcher.Renamed += _watcher_Changed;
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            _timer = new Timer(OnTime, null, new TimeSpan(0, 0, 2),new TimeSpan(0,0,2));
        }

        private int _lastWorkVersion;
        private void OnTime(object state) {
            var changedVersion = _changedVersion;
            //更新最后一次处理的版本号
            var lastVersion = Interlocked.Exchange(ref _lastWorkVersion, changedVersion);
            if (lastVersion != changedVersion) {
                var onchanged = this.Changed;
                if (onchanged != null) {
                    onchanged(this, EventArgs.Empty);
                }
            }
        }

        private void UnhookFiles() {
            if (string.IsNullOrEmpty(_path) ||
                _watcher == null ||
                !Directory.Exists(_path)) {
                return;
            }

            _watcher.Changed -= _watcher_Changed;
            _watcher.Created -= _watcher_Changed;
            _watcher.Deleted -= _watcher_Changed;
            _watcher.Renamed -= _watcher_Changed;
            _watcher.Dispose();
            _watcher = null;

            if (_timer != null) {
                _timer.Dispose();
                _timer = null;
            }            
        }

        private int _changedVersion;
        private void _watcher_Changed(object sender, FileSystemEventArgs e) {
            //在文件发生变化后，标记文件已改动
            //理论上，在单个文件发生改变时，可以仅更新单个文件的FileInfo，但是我不想把代码写的太复杂，所以就统一使用脏。
            //在标记为脏后，定时器发起同步，很可能在同步的过程中又发生变更，所以我使用版本号的方式，这样就知道之后有没有再次发生改变。
            Interlocked.Increment(ref _changedVersion);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    if (_timer != null) {
                        _timer.Dispose();
                        _timer = null;
                    }

                    if (_watcher != null) {
                        _watcher.Dispose();
                        _watcher = null;
                    }
                }

                disposedValue = true;
            }
        }
        
        public void Dispose() {
            Dispose(true);
        }
        #endregion
    }
}
