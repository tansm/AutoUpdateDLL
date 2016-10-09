using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace AutoUpdate {

    /// <summary>
    /// 管理工作区，例如新增、保存等。注意他是无状态的类
    /// </summary>
    class WorkspaceManager {
        
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
        
        public Workspace Open(string path) {
            try {
                string fileName = Path.Combine(path, "AutoUpdate.xml");
                Workspace workspace;
                if (File.Exists(fileName)) {
                    var xml = File.ReadAllText(fileName, Encoding.UTF8);
                    workspace = GetWorkspace(xml);
                    workspace.TargetPath = path;
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

                return newWorkspace;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format(CultureInfo.CurrentUICulture, "另存到路径 {0} 下失败。", newPath), ex);
            }
        }
    }
}
