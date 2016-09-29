using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System;

namespace AutoUpdate {
    class Workspace : INotifyPropertyChanged {

        private string _sourcePath;
        /// <summary>
        /// 源路径
        /// </summary>
        public string SourcePath { 
            get{return _sourcePath;}
            set {
                if (_sourcePath != value) {
                    _sourcePath = value;
                    this.OnPropertyChanged("SourcePath");
                    _sourceFilesManager.Start(value);
                }
            }
        }
        private TaskManager<string,SourceFileCollection> _sourceFilesManager = new TaskManager<string,SourceFileCollection>(SourceFileCollection.DoWork);

        private string _targetPath;
        /// <summary>
        /// 目标路径
        /// </summary>
        public string TargetPath {
            get { return _targetPath; }
            set {
                if (_targetPath != value) {
                    _targetPath = value;
                    this.OnPropertyChanged("TargetPath");
                    _targetFilesManager.Start(value);
                }
            }
        }
        private TaskManager<string,TargetFileCollection> _targetFilesManager = new TaskManager<string,TargetFileCollection>(TargetFileCollection.DoWork);

        private bool _onlyLastHour = true;
        /// <summary>
        /// 仅同步最后一小时更新的组件。
        /// </summary>
        public bool OnlyLastHour {
            get { return _onlyLastHour; }
            set {
                if (_onlyLastHour != value) {
                    _onlyLastHour = value;
                    this.OnPropertyChanged("OnlyLastHour");
                }
            }
        }

        public void Sync() {
            AddMessage("获取源目录清单： " + _sourcePath);
            var sourceFiles = _sourceFilesManager.Result;
            var sourcePath = _sourceFilesManager.LastArgs;
            AddMessage("文件数： " + sourceFiles.Count);

            AddMessage("获取目标目录清单： " + _sourcePath);
            var targetFiles = _targetFilesManager.Result;
            var targetPath = _targetFilesManager.LastArgs;
            AddMessage("文件数： " + targetFiles.Count);

            if (sourceFiles == null || sourceFiles.IsErrorStatus ||
                targetFiles == null || targetFiles.IsErrorStatus ||
                !object.ReferenceEquals(sourcePath, _sourcePath) ||
                !object.ReferenceEquals(targetPath, _targetPath)) {
                AddMessage("失败，路径不准确或当前状态无效。");
                return;
            }

            _syncTaskManager.Start(new SyncArgs() {
                SourceFiles = sourceFiles,
                TargetFiles = targetFiles,
                OnlyLastHour = _onlyLastHour,
                Owner = this
            });
        }

        public void CancelAll() {
            _syncTaskManager.Cancel();
            _sourceFilesManager.Cancel();
            _targetFilesManager.Cancel();
        }

        private TaskManager<SyncArgs, bool> _syncTaskManager = new TaskManager<SyncArgs, bool>(DoWork);

        private bool _working;
        public bool Working {
            get { return _working; }
            private set {
                if (_working != value) {
                    _working = value;
                    OnPropertyChanged("Working");
                    OnPropertyChanged("CanSync");
                }
            }
        }

        public bool CanSync {
            get { return !_working; }
        }

        //为后台任务准备。
        internal static void DoWork(DoWorkEventArgs e) {
            var args = (SyncArgs)e.Argument;
            args.Owner.Working = true;

            try {                
                args.DoWork(e);
            }
            finally {
                args.Owner.CompleteCount = 0;
                args.Owner.Working = false;
            }
        }

        private class SyncArgs {
            public SourceFileCollection SourceFiles;
            public TargetFileCollection TargetFiles;
            public bool OnlyLastHour;
            public Workspace Owner;

            public void DoWork(CancelEventArgs e) {
                //找到源中需要更新的文件，
                if (OnlyLastHour) {
                    SourceFiles = SourceFiles.GetLastHourItems();
                }

                List<KeyValuePair<FileInfo, FileInfo>> lst = new List<KeyValuePair<FileInfo, FileInfo>>();

                foreach (var sourceFile in SourceFiles) {
                    IEnumerable<FileInfo> targetList;

                    if (e.Cancel) {
                        throw new OperationCanceledException();
                    }

                    if (TargetFiles.TryGet(sourceFile.Name, out targetList)) {
                        foreach (var targetFile in targetList) {
                            if (sourceFile.LastWriteTime > targetFile.LastWriteTime) {
                                lst.Add(new KeyValuePair<FileInfo, FileInfo>(sourceFile, targetFile));
                            }
                        }
                    }
                }

                Owner.SyncFileCount = lst.Count - 1;
                int completeCount = 0;
                foreach (var item in lst) {
                    if (e.Cancel) {
                        throw new OperationCanceledException();
                    }

                    try {
                        File.Copy(item.Key.FilePath, item.Value.FilePath, true);
                        item.Value.UpdateLastWriteTime();
                        //System.Threading.Thread.Sleep(500);
                        Owner.AddMessage("更新： " + item.Value.FilePath);
                        completeCount++;
                    }
                    catch {
                        Owner.AddMessage("失败： " + item.Value.FilePath);
                    }
                    Owner.CompleteCount++;
                }

                Owner.AddMessage(string.Format(@"已结束，完成数 / 总数： {0} / {1}", completeCount, lst.Count));
            }
        }

        private void AddMessage(string message) {
            if (this.MessageAdded != null) {
                var time = DateTime.Now.ToLongTimeString();
                this.MessageAdded(this,new MessageAddedEventArgs(time + "  " + message));
            }
        }

        public event EventHandler<MessageAddedEventArgs> MessageAdded;

        private int _syncFileCount = 100;
        public int SyncFileCount {
            get { return _syncFileCount; }
            set {
                if (_syncFileCount != value && value > 0) {
                    _syncFileCount = value;
                    this.OnPropertyChanged("SyncFileCount");
                }
            }
        }

        private int _completeCount;
        public int CompleteCount {
            get { return _completeCount; }
            set {
                if (_completeCount != value) {
                    _completeCount = value;
                    this.OnPropertyChanged("CompleteCount");
                }
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    class MessageAddedEventArgs : EventArgs {
        public MessageAddedEventArgs(string message) {
            this.Message = message;
        }
        public string Message { get; private set; }
    }
}
