namespace AutoUpdate {
    partial class FormWorkspace {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkspace));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._currentWorkspaceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSelectSourcePath = new System.Windows.Forms.Button();
            this.chkOnlyLastHour = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.ListBox();
            this.txtSouce = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.palEdit = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecentlyUsed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.pathBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.mnuRecentlyNothing = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._currentWorkspaceBindingSource)).BeginInit();
            this.palEdit.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this._currentWorkspaceBindingSource, "CompleteCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.progressBar1.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this._currentWorkspaceBindingSource, "SyncFileCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.progressBar1.Location = new System.Drawing.Point(623, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(254, 23);
            this.progressBar1.TabIndex = 16;
            // 
            // _currentWorkspaceBindingSource
            // 
            this._currentWorkspaceBindingSource.DataSource = typeof(AutoUpdate.Workspace);
            // 
            // btnSelectSourcePath
            // 
            this.btnSelectSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSourcePath.Location = new System.Drawing.Point(964, 46);
            this.btnSelectSourcePath.Name = "btnSelectSourcePath";
            this.btnSelectSourcePath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSourcePath.TabIndex = 15;
            this.btnSelectSourcePath.Text = "选择...";
            this.btnSelectSourcePath.UseVisualStyleBackColor = true;
            // 
            // chkOnlyLastHour
            // 
            this.chkOnlyLastHour.AutoSize = true;
            this.chkOnlyLastHour.Checked = true;
            this.chkOnlyLastHour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyLastHour.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this._currentWorkspaceBindingSource, "OnlyLastHour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkOnlyLastHour.Location = new System.Drawing.Point(21, 95);
            this.chkOnlyLastHour.Name = "chkOnlyLastHour";
            this.chkOnlyLastHour.Size = new System.Drawing.Size(174, 16);
            this.chkOnlyLastHour.TabIndex = 13;
            this.chkOnlyLastHour.Text = "仅同步最近一小时修改的dll";
            this.chkOnlyLastHour.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.FormattingEnabled = true;
            this.txtLog.ItemHeight = 12;
            this.txtLog.Location = new System.Drawing.Point(21, 117);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1018, 520);
            this.txtLog.TabIndex = 12;
            // 
            // txtSouce
            // 
            this.txtSouce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSouce.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSouce.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtSouce.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._currentWorkspaceBindingSource, "SourcePath", true));
            this.txtSouce.Location = new System.Drawing.Point(90, 48);
            this.txtSouce.Name = "txtSouce";
            this.txtSouce.Size = new System.Drawing.Size(868, 21);
            this.txtSouce.TabIndex = 11;
            this.txtSouce.Text = "C:\\Documents\\Digiwin\\TRServer\\Digiwin.Mars.Export\\";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "源路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "目标路径：";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this._currentWorkspaceBindingSource, "CanSync", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnUpdate.Location = new System.Drawing.Point(883, 83);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "同步";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtTarget
            // 
            this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTarget.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtTarget.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._currentWorkspaceBindingSource, "TargetPath", true));
            this.txtTarget.Location = new System.Drawing.Point(90, 8);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.ReadOnly = true;
            this.txtTarget.Size = new System.Drawing.Size(949, 21);
            this.txtTarget.TabIndex = 7;
            this.txtTarget.Text = "C:\\Tools\\E10_3\\E50-20160927\\";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this._currentWorkspaceBindingSource, "Working", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnCancel.Location = new System.Drawing.Point(964, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // palEdit
            // 
            this.palEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palEdit.Controls.Add(this.progressBar1);
            this.palEdit.Controls.Add(this.btnCancel);
            this.palEdit.Controls.Add(this.btnSelectSourcePath);
            this.palEdit.Controls.Add(this.txtTarget);
            this.palEdit.Controls.Add(this.chkOnlyLastHour);
            this.palEdit.Controls.Add(this.btnUpdate);
            this.palEdit.Controls.Add(this.txtLog);
            this.palEdit.Controls.Add(this.label1);
            this.palEdit.Controls.Add(this.txtSouce);
            this.palEdit.Controls.Add(this.label2);
            this.palEdit.Location = new System.Drawing.Point(0, 62);
            this.palEdit.Name = "palEdit";
            this.palEdit.Size = new System.Drawing.Size(1060, 658);
            this.palEdit.TabIndex = 11;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1060, 25);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuRecentlyUsed,
            this.toolStripSeparator,
            this.mnuSave,
            this.mnuSaveAs,
            this.toolStripSeparator1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(58, 21);
            this.mnuFile.Text = "文件(&F)";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(203, 22);
            this.mnuOpen.Text = "打开(&O)";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuRecentlyUsed
            // 
            this.mnuRecentlyUsed.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRecentlyNothing});
            this.mnuRecentlyUsed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRecentlyUsed.Name = "mnuRecentlyUsed";
            this.mnuRecentlyUsed.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuRecentlyUsed.Size = new System.Drawing.Size(203, 22);
            this.mnuRecentlyUsed.Text = "最近使用的(&F)...";
            this.mnuRecentlyUsed.DropDownOpening += new System.EventHandler(this.mnuRecentlyUsed_DropDownOpening);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(200, 6);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSave.Size = new System.Drawing.Size(203, 22);
            this.mnuSave.Text = "保存(&S)";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(203, 22);
            this.mnuSaveAs.Text = "另存为(&A)...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(203, 22);
            this.mnuExit.Text = "退出(&X)";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuIssues});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(61, 21);
            this.mnuHelp.Text = "帮助(&H)";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(142, 22);
            this.mnuAbout.Text = "源代码(&S)";
            this.mnuAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // mnuIssues
            // 
            this.mnuIssues.Name = "mnuIssues";
            this.mnuIssues.Size = new System.Drawing.Size(142, 22);
            this.mnuIssues.Text = "反馈问题(&Q)";
            this.mnuIssues.Click += new System.EventHandler(this.mnuIssues_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator7,
            this.btnAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1060, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "打开(&O)";
            this.btnOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbout
            // 
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(23, 22);
            this.btnAbout.Text = "帮助(&L)";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // mnuRecentlyNothing
            // 
            this.mnuRecentlyNothing.Name = "mnuRecentlyNothing";
            this.mnuRecentlyNothing.Size = new System.Drawing.Size(152, 22);
            this.mnuRecentlyNothing.Text = "无记录";
            // 
            // FormWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 716);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.palEdit);
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::AutoUpdate.Properties.Resources.sync;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormWorkspace";
            this.Text = "组件同步器";
            ((System.ComponentModel.ISupportInitialize)(this._currentWorkspaceBindingSource)).EndInit();
            this.palEdit.ResumeLayout(false);
            this.palEdit.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkOnlyLastHour;
        private System.Windows.Forms.ListBox txtLog;
        private System.Windows.Forms.TextBox txtSouce;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnSelectSourcePath;
        private System.Windows.Forms.BindingSource _currentWorkspaceBindingSource;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel palEdit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentlyUsed;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.FolderBrowserDialog pathBrowser;
        private System.Windows.Forms.ToolStripMenuItem mnuIssues;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentlyNothing;
    }
}

