using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoUpdate {
    public partial class FormWorkspace : Form {
        private Workspace _currentWorkspace;
        private WorkspaceManager _manager;

        public FormWorkspace() {
            InitializeComponent();

            this._manager = WorkspaceManager.GetManager();
            if (this._manager.History.Count > 0) {
                this.CurrentWorkspace = this._manager.Open(this._manager.History[0]);
            }
            else {
                this.CurrentWorkspace = new AutoUpdate.Workspace() {
                    TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    OnlyLastHour = true,
                };
            }
        }

        internal Workspace CurrentWorkspace {
            get { return _currentWorkspace; }
            set {
                if (_currentWorkspace != value) {
                    if (_currentWorkspace != null) {
                        _currentWorkspace.MessageAdded -= _currentWorkspace_MessageAdded;
                        _currentWorkspace.PropertyChanged -= _currentWorkspace_PropertyChanged;
                        _currentWorkspace.Dispose();
                    }

                    _currentWorkspace = value;
                    _currentWorkspaceBindingSource.DataSource = _currentWorkspace;
                    UpdateDirty();
                    txtLog.Items.Clear();

                    if (_currentWorkspace != null) {
                        _currentWorkspace.MessageAdded += _currentWorkspace_MessageAdded;
                        _currentWorkspace.PropertyChanged += _currentWorkspace_PropertyChanged;
                    }
                }
            }
        }

        private void _currentWorkspace_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "Dirty") {
                UpdateDirty();
            }
        }

        private string _formText;
        private void UpdateDirty() {
            if (_formText == null) {
                _formText = this.Text; //clone
            }

            bool canSave;
            if (_currentWorkspace == null || !_currentWorkspace.Dirty) {
                this.Text = _formText;
                canSave = false;
            }
            else {
                this.Text = _formText + " *";
                canSave = true;
            }

            this.mnuSave.Enabled = canSave;
            this.mnuSaveAs.Enabled = canSave;
            this.btnSave.Enabled = canSave;
        }

        public void CloseCurrentWorkspace() {
            if (_currentWorkspace != null && _currentWorkspace.Dirty) {
                var result = MessageBox.Show(this, "当前工作区已修改，是否保存修改？", "是否保存修改", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                _currentWorkspace.CancelAll();
                if (result == DialogResult.Yes) {
                    _manager.Save(_currentWorkspace);
                }
                else if (result == DialogResult.Cancel) {
                    throw new OperationCanceledException();
                }//else if (result == no) ... 继续
            }
        }

        private void _currentWorkspace_MessageAdded(object sender, MessageAddedEventArgs e) {
            if (_addmessageFunc == null) {
                _addmessageFunc = new EventHandler<MessageAddedEventArgs>(this.AddMessage);
            }
            Invoke(_addmessageFunc, this, e);
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
            CloseCurrentWorkspace();
            _manager.Save();
        }

        private void btnAbout_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/MyErpSoft/AutoUpdateDLL");

        }

        private void mnuOpen_Click(object sender, EventArgs e) {
            CloseCurrentWorkspace();
            pathBrowser.Description = "选择您要同步的目标文件夹，当同步时本软件将源路径最新的文件复制到此目标目录。";
            if (pathBrowser.ShowDialog(this) == DialogResult.OK) {
                this.CurrentWorkspace = _manager.Open(pathBrowser.SelectedPath);
            }
        }

        private void mnuSave_Click(object sender, EventArgs e) {
            if (this.CurrentWorkspace != null) {
                _manager.Save(this.CurrentWorkspace);
            }
        }

        private void mnuSaveAs_Click(object sender, EventArgs e) {
            if (this.CurrentWorkspace != null) {
                pathBrowser.Description = "选择您要新保存的目标文件夹。";
                if (pathBrowser.ShowDialog(this) == DialogResult.OK) {
                    this.CurrentWorkspace = _manager.SaveAs(this.CurrentWorkspace, pathBrowser.SelectedPath);
                }
            }
        }

        private void mnuExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void mnuIssues_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/MyErpSoft/AutoUpdateDLL/issues");
        }

        private void mnuRecentlyUsed_DropDownOpening(object sender, EventArgs e) {
            var items = mnuRecentlyUsed.DropDownItems;
            var history = this._manager.History.ToArray();

            var editCount = Math.Min(items.Count, history.Length);
            for (int i = 0; i < editCount; i++) {
                var mnuItem = (ToolStripMenuItem)items[i];
                UpdateRecentlyItem(mnuItem, history,i);
            }

            //add
            if (history.Length > items.Count) {
                for (int i = items.Count; i < history.Length; i++) {
                    var mnuItem = new ToolStripMenuItem();
                    UpdateRecentlyItem(mnuItem, history,i);
                    mnuItem.Click += mnuRecentlyItem_Click;
                    items.Add(mnuItem);
                }
            }

            //remove
            if (history.Length < items.Count) {
                var removeCount = items.Count - history.Length;
                for (int i = 0; i < removeCount; i++) {
                    var mnuItem = items[items.Count - 1];
                    mnuItem.Click -= mnuRecentlyItem_Click;
                    items.RemoveAt(items.Count - 1);
                }
            }
        }

        private void UpdateRecentlyItem(ToolStripMenuItem menu, string[] history, int index) {
            string text;
            string path = history[index];
            if (path.Length > 49) {
                text = "(&" + index.ToString() + ") ..." + path.Substring(path.Length - 49, 49);
            }
            else {
                text = "(&" + index.ToString() + ") " + path;
            }

            menu.Text = text;
            menu.Tag = path;
        }

        private void mnuRecentlyItem_Click(object sender, EventArgs e) {
            var path = (string)((ToolStripMenuItem)sender).Tag;
            CloseCurrentWorkspace();
            this.CurrentWorkspace = _manager.Open(path);
        }

        private void btnSelectSourcePath_Click(object sender, EventArgs e) {
            if (this.CurrentWorkspace != null) {
                pathBrowser.Description = "选择来源文件夹，同步时将检查此目录是否存在最新的文件并将其复制到目标文件夹。";
                pathBrowser.SelectedPath = this.CurrentWorkspace.SourcePath;
                if (pathBrowser.ShowDialog(this) == DialogResult.OK) {
                    this.CurrentWorkspace.SourcePath = pathBrowser.SelectedPath;
                }
            }
        }
    }

}
