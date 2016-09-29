using System;
using System.IO;

namespace AutoUpdate {

    internal class FileInfo {
        public FileInfo(string fileName) {
            this.FilePath = fileName;
            this.Name = Path.GetFileName(fileName);
            this.LastWriteTime = File.GetLastWriteTime(fileName);
        }

        public string FilePath { get; private set; }

        public string Name { get; private set; }

        public DateTime LastWriteTime { get; private set; }

        public void UpdateLastWriteTime() {
            this.LastWriteTime = File.GetLastWriteTime(this.FilePath);
        }
    }
}
