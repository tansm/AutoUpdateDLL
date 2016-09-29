using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AutoUpdate {
    public partial class Form1 : Form {
        private Workspace _currentWorkspace;

        public Form1() {
            InitializeComponent();
            
            this.CurrentWorkspace = new Workspace() {
                SourcePath = @"C:\Documents\Digiwin\TRServer\Digiwin.Mars.Export\",
                TargetPath = @"C:\Tools\E10_3\E50-20160927\"
            }; 
        }

        internal Workspace CurrentWorkspace {
            get { return _currentWorkspace; }
            set {
                if (_currentWorkspace != value) {
                    if (_currentWorkspace != null) {
                        _currentWorkspace.MessageAdded -= _currentWorkspace_MessageAdded;
                    }

                    _currentWorkspace = value;
                    _currentWorkspaceBindingSource.DataSource = _currentWorkspace;

                    if (_currentWorkspace != null) {
                        _currentWorkspace.MessageAdded += _currentWorkspace_MessageAdded;
                    }
                }
            }
        }

        private void _currentWorkspace_MessageAdded(object sender, MessageAddedEventArgs e) {
            if (_addmessageFunc == null) {
                _addmessageFunc = new EventHandler<MessageAddedEventArgs>(this.AddMessage);
            }
            Invoke(_addmessageFunc, this, e);
            Application.DoEvents();
        }

        private EventHandler<MessageAddedEventArgs> _addmessageFunc;
        private void AddMessage(object sender, MessageAddedEventArgs e) {
            txtLog.Items.Add(e.Message);
            Application.DoEvents();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            _currentWorkspace.Sync();
            //UpdateFiles();
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            _currentWorkspace.CancelAll();
        }

        private void UpdateFiles() {
            var sourceFiles = GetSourceFiles(txtSouce.Text);
            var targetFiles = Directory.GetFiles(txtTarget.Text, "*.dll", SearchOption.AllDirectories);
            var now = DateTime.Now;
            bool isOnlyLastHour = chkOnlyLastHour.Checked;

            foreach (var targetFile in targetFiles) {
                FileInfo targetFileInfo = new FileInfo(targetFile);
                FileInfo sourceFileInfo;
                if (sourceFiles.TryGetValue(targetFileInfo.Name, out sourceFileInfo) &&
                    sourceFileInfo.LastWriteTime > targetFileInfo.LastWriteTime) {
                    try {
                        if (!isOnlyLastHour || sourceFileInfo.LastWriteTime.AddHours(1) > now) {
                            File.Copy(sourceFileInfo.FilePath, targetFile, true);
                            txtLog.Items.Add(targetFile);
                        }
                    }
                    catch (Exception ex) {
                        txtLog.Items.Add("Error:" + ex.Message + ">" + targetFile);
                    }
                }
            }
        }

        private Dictionary<string, FileInfo> GetSourceFiles(string path) {
            Dictionary<string, FileInfo> result = new Dictionary<string, FileInfo>();

            var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            foreach (var file in files) {
                FileInfo fi = new FileInfo(file);
                FileInfo old;
                if (result.TryGetValue(fi.Name,out old)) {
                    //看谁比较新
                    if (fi.LastWriteTime > old.LastWriteTime) {
                        result[fi.Name] = fi;
                    }
                }
                else {
                    result.Add(fi.Name, fi);
                }
            }

            return result;
        }
    }

}
