using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace AutoUpdate {

    //源文件清单，注意，实例是只读的，以便更好的支持并行处理。
    class SourceFileCollection : IEnumerable<FileInfo> {
        private SourceFileCollection() {
            _files = new Dictionary<string, FileInfo>();
            _isErrorStatus = true;
        }

        private SourceFileCollection(string path, Dictionary<string, FileInfo> files) {
            _sourcePath = path;
            _files = files;
        }

        private static SourceFileCollection Empty = new SourceFileCollection();
        /// <summary>
        /// 从一个指定的路径下获取源，将进行完整扫描。
        /// </summary>
        /// <param name="sourcePath">要检索的路径</param>
        /// <returns>一个源文件集合。</returns>
        public static SourceFileCollection Load(string sourcePath,CancelEventArgs cancelToken) {
            if (string.IsNullOrEmpty(sourcePath) ||
                !Directory.Exists(sourcePath)) {
                return Empty;
            }

#if !DEBUG
            //可能单个文件没有权限
            try {
#endif
                var sourceFiles = GetSourceFiles(sourcePath, cancelToken);
                return new SourceFileCollection(sourcePath, sourceFiles);
#if !DEBUG
            }
            catch (OperationCanceledException) {
                throw;
            }
            catch {
                return Empty;
            }
#endif
        }

        private static Dictionary<string, FileInfo> GetSourceFiles(string path, CancelEventArgs cancelToken) {
            //以文件名为键，例如：abc.dll
            Dictionary<string, FileInfo> result = new Dictionary<string, FileInfo>();

            var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            foreach (var file in files) {
                if (cancelToken.Cancel) {
                    throw new OperationCanceledException();
                }

#if !DEBUG
                //可能单个文件没有权限
                try {
#endif

                    FileInfo fi = new FileInfo(file);
                    FileInfo old;
                    if (result.TryGetValue(fi.Name, out old)) {
                        //看谁比较新
                        if (fi.LastWriteTime > old.LastWriteTime) {
                            result[fi.Name] = fi;
                        }
                    }
                    else {
                        result.Add(fi.Name, fi);
                    }
#if !DEBUG                        
                }
                catch { }
#endif
            }

            return result;
        }

        //为后台任务准备。
        internal static void DoWork(DoWorkEventArgs e) {
            string sourcePath = (string)e.Argument;
            e.Result = Load(sourcePath, e);
        }

        private string _sourcePath;
        /// <summary>
        /// 源路径
        /// </summary>
        public string SourcePath {
            get { return _sourcePath; }
        }

        private bool _isErrorStatus;
        public bool IsErrorStatus {
            get { return _isErrorStatus; }
        }

        private readonly Dictionary<string, FileInfo> _files; //已文件名为键，存放所有文件信息。

        public bool TryGet(string fileName, out FileInfo file) {
            if (string.IsNullOrEmpty(fileName)) {
                file = null;
                return false;
            }

            return _files.TryGetValue(fileName, out file);
        }

        //获取最后一小时更新的内容。
        public SourceFileCollection GetLastHourItems() {
            if (_isErrorStatus) {
                return Empty;
            }

            var newDict = new Dictionary<string, FileInfo>(_files.Count/3);
            var now = DateTime.Now;

            foreach (var file in _files.Values) {
                if (file.LastWriteTime.AddHours(1) > now) {
                    newDict.Add(file.Name, file);
                }
            }

            return new SourceFileCollection(this._sourcePath, newDict);
        }

        public int Count {
            get { return _files.Count; }
        }

        public IEnumerator<FileInfo> GetEnumerator() {
            foreach (var item in _files.Values) {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
