using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace AutoUpdate {

    //目标文件清单，按文件名为键，但对应多个文件。
    class TargetFileCollection {
        private TargetFileCollection() {
            _files = new Dictionary<string, List<FileInfo>>();
            _isErrorStatus = true;
        }

        private TargetFileCollection(string path, Dictionary<string, List<FileInfo>> files) {
            _targetPath = path;
            _files = files;
        }

        private static TargetFileCollection Empty = new TargetFileCollection();
        /// <summary>
        /// 从一个指定的路径下获取目标，将进行完整扫描。
        /// </summary>
        /// <param name="targetPath">要检索的路径</param>
        /// <returns>一个目标文件集合。</returns>
        public static TargetFileCollection Load(string targetPath, CancelEventArgs cancelToken) {
            if (string.IsNullOrEmpty(targetPath) ||
                !Directory.Exists(targetPath)) {
                return Empty;
            }

#if !DEBUG
            //可能单个文件没有权限
            try {
#endif
                var tagetFiles = GetTargetFiles(targetPath, cancelToken);
                return new TargetFileCollection(targetPath, tagetFiles);
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

        private static Dictionary<string, List<FileInfo>> GetTargetFiles(string path, CancelEventArgs cancelToken) {
            //以文件名为键，例如：abc.dll
            Dictionary<string, List<FileInfo>> result = new Dictionary<string, List<FileInfo>>();

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
                    List<FileInfo> lst;
                    if (!result.TryGetValue(fi.Name, out lst)) {
                        lst = new List<FileInfo>();
                        result.Add(fi.Name, lst);
                    }
                    lst.Add(fi);
#if !DEBUG
                }
                catch { }
#endif
            }

            return result;
        }
        
        //为后台任务准备。
        internal static void DoWork(DoWorkEventArgs e) {
            string targetPath = (string)e.Argument;
            e.Result = Load(targetPath, e);
        }

        private string _targetPath;
        /// <summary>
        /// 目标路径
        /// </summary>
        public string TargetPath {
            get { return _targetPath; }
        }

        public int Count {
            get { return _files.Count; }
        }

        private bool _isErrorStatus;
        public bool IsErrorStatus {
            get { return _isErrorStatus; }
        }

        private readonly Dictionary<string, List<FileInfo>> _files; //已文件名为键，多个路径下的文件为值。

        public bool TryGet(string fileName, out IEnumerable<FileInfo> files) {
            if (string.IsNullOrEmpty(fileName)) {
                files = null;
                return false;
            }

            List<FileInfo> lst;
            if (_files.TryGetValue(fileName, out lst)) {
                files = lst;
                return true;
            }
            else {
                files = lst;
                return false;
            }
        }
    }
}