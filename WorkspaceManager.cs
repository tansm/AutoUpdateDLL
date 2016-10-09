using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace AutoUpdate {

    /// <summary>
    /// 管理工作区，例如新增、保存等
    /// </summary>
    public class WorkspaceManager {

        public static WorkspaceManager GetManager() {
            var ser = _default.CreateSerializer(typeof(WorkspaceManager));
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"AutoUpdate\user.xml");
            if (File.Exists(path)) {
                try {
                    var xml = File.ReadAllText(path);
                    using (StringReader reader = new StringReader(xml)) {
                        using (XmlTextReader xmlReader = new XmlTextReader(reader)) {
                            return (WorkspaceManager)ser.Deserialize(xmlReader);
                        }
                    }
                }
                catch {
                    return new WorkspaceManager();
                }
            }
            else {
                return new WorkspaceManager();
            }
        }

        public void Save() {
            var ser = _default.CreateSerializer(typeof(WorkspaceManager));
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"AutoUpdate\");
            try {
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "user.xml");

                var sb = new StringBuilder(512);
                using (var xmlWriter = XmlWriter.Create(sb)) {
                    ser.Serialize(xmlWriter, this);
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
                }
            }
            catch  {

            }
        }

        private static XmlSerializerFactory _default = new XmlSerializerFactory();
        public virtual string GetText(Workspace item) {
            var ser = _default.CreateSerializer(typeof(Workspace));
            var sb = new StringBuilder(512);
            using (var xmlWriter = XmlWriter.Create(sb)) {
                ser.Serialize(xmlWriter, item);
                return sb.ToString();
            }
        }

        public virtual Workspace GetWorkspace(string xml) {
            var ser = _default.CreateSerializer(typeof(Workspace));
            using (StringReader reader = new StringReader(xml)) {
                using (XmlTextReader xmlReader = new XmlTextReader(reader)) {
                    return (Workspace)ser.Deserialize(xmlReader);
                }
            }
        }

        private PathHistoryCollection _history;

        public PathHistoryCollection History {
            get {
                if (_history == null) {
                    _history = new PathHistoryCollection();
                }

                return _history;
            }
        }

        public Workspace Open(string path) {
            try {
                string fileName = Path.Combine(path, "AutoUpdate.xml");
                Workspace workspace;
                if (File.Exists(fileName)) {
                    var xml = File.ReadAllText(fileName, Encoding.UTF8);
                    workspace = GetWorkspace(xml);
                    workspace.TargetPath = path;
                    this.History.Add(path);
                }
                else {
                    workspace = new Workspace() {
                        OnlyLastHour = true,
                        TargetPath = path
                    };
                }

                workspace.Dirty = false;
                return workspace;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format(CultureInfo.CurrentUICulture, "打开路径 {0} 下的文件失败。", path), ex);
            }
        }

        public void Save(Workspace workspace) {
            try {
                string fileName = Path.Combine(workspace.TargetPath, "AutoUpdate.xml");
                File.WriteAllText(fileName, GetText(workspace), Encoding.UTF8);
                workspace.Dirty = false;
                this.History.Add(workspace.TargetPath);
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format(CultureInfo.CurrentUICulture, "保存到路径 {0} 下失败。", workspace.TargetPath), ex);
            }
        }

        public Workspace SaveAs(Workspace workspace, string newPath) {
            try {
                string fileName = Path.Combine(newPath, "AutoUpdate.xml");

                //clone
                var xml = GetText(workspace);
                var newWorkspace = GetWorkspace(xml);
                newWorkspace.TargetPath = newPath;
                File.WriteAllText(fileName, GetText(newWorkspace), Encoding.UTF8);
                newWorkspace.Dirty = false;
                this.History.Add(newPath);

                return newWorkspace;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format(CultureInfo.CurrentUICulture, "另存到路径 {0} 下失败。", newPath), ex);
            }
        }
    }
}
