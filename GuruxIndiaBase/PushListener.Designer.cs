namespace Gurux_Testing
{
    partial class PushListener
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PushListener));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.notify = new System.Windows.Forms.ToolStripStatusLabel();
            this.serverNotify = new System.Windows.Forms.ToolStripStatusLabel();
            this.resultnotify = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.ch_Ipv6 = new System.Windows.Forms.CheckBox();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.btn_startServer = new System.Windows.Forms.Button();
            this.btn_StopServer = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectMeterToDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.disconnectClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readNameplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notify,
            this.serverNotify,
            this.resultnotify});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1243, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // notify
            // 
            this.notify.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.notify.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(120, 21);
            this.notify.Text = "Push Listener |";
            // 
            // serverNotify
            // 
            this.serverNotify.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.serverNotify.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.serverNotify.Name = "serverNotify";
            this.serverNotify.Size = new System.Drawing.Size(146, 21);
            this.serverNotify.Text = "Server Status : Off";
            // 
            // resultnotify
            // 
            this.resultnotify.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.resultnotify.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.resultnotify.Name = "resultnotify";
            this.resultnotify.Size = new System.Drawing.Size(15, 21);
            this.resultnotify.Text = "|";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1243, 550);
            this.splitContainer1.SplitterDistance = 347;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.ch_Ipv6);
            this.splitContainer2.Panel1.Controls.Add(this.tb_port);
            this.splitContainer2.Panel1.Controls.Add(this.btn_startServer);
            this.splitContainer2.Panel1.Controls.Add(this.btn_StopServer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(347, 550);
            this.splitContainer2.SplitterDistance = 80;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(93, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "TCP SERVER";
            // 
            // ch_Ipv6
            // 
            this.ch_Ipv6.AutoSize = true;
            this.ch_Ipv6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ch_Ipv6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ch_Ipv6.ForeColor = System.Drawing.Color.White;
            this.ch_Ipv6.Location = new System.Drawing.Point(236, 56);
            this.ch_Ipv6.Name = "ch_Ipv6";
            this.ch_Ipv6.Size = new System.Drawing.Size(62, 24);
            this.ch_Ipv6.TabIndex = 7;
            this.ch_Ipv6.Text = "Ipv6";
            this.ch_Ipv6.UseVisualStyleBackColor = true;
            // 
            // tb_port
            // 
            this.tb_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_port.Location = new System.Drawing.Point(236, 12);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(74, 38);
            this.tb_port.TabIndex = 22;
            this.tb_port.Text = "4059";
            // 
            // btn_startServer
            // 
            this.btn_startServer.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btn_startServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_startServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startServer.ForeColor = System.Drawing.Color.White;
            this.btn_startServer.Location = new System.Drawing.Point(36, 12);
            this.btn_startServer.Name = "btn_startServer";
            this.btn_startServer.Size = new System.Drawing.Size(94, 37);
            this.btn_startServer.TabIndex = 20;
            this.btn_startServer.Text = "START";
            this.btn_startServer.UseVisualStyleBackColor = false;
            this.btn_startServer.Click += new System.EventHandler(this.btn_startServer_Click);
            // 
            // btn_StopServer
            // 
            this.btn_StopServer.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btn_StopServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_StopServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopServer.ForeColor = System.Drawing.Color.White;
            this.btn_StopServer.Location = new System.Drawing.Point(136, 12);
            this.btn_StopServer.Name = "btn_StopServer";
            this.btn_StopServer.Size = new System.Drawing.Size(94, 37);
            this.btn_StopServer.TabIndex = 21;
            this.btn_StopServer.Text = "STOP";
            this.btn_StopServer.UseVisualStyleBackColor = false;
            this.btn_StopServer.Click += new System.EventHandler(this.btn_StopServer_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(347, 466);
            this.dataGridView1.TabIndex = 1;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMeterToDownloadToolStripMenuItem,
            this.disconnectMeterToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(211, 48);
            // 
            // selectMeterToDownloadToolStripMenuItem
            // 
            this.selectMeterToDownloadToolStripMenuItem.Name = "selectMeterToDownloadToolStripMenuItem";
            this.selectMeterToDownloadToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.selectMeterToDownloadToolStripMenuItem.Text = "Select Meter to Download";
            this.selectMeterToDownloadToolStripMenuItem.Click += new System.EventHandler(this.selectMeterToDownloadToolStripMenuItem_Click);
            // 
            // disconnectMeterToolStripMenuItem
            // 
            this.disconnectMeterToolStripMenuItem.Name = "disconnectMeterToolStripMenuItem";
            this.disconnectMeterToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.disconnectMeterToolStripMenuItem.Text = "Disconnect Meter";
            this.disconnectMeterToolStripMenuItem.Click += new System.EventHandler(this.disconnectMeterToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(892, 550);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbLogs);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(884, 517);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LOG";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtbLogs
            // 
            this.rtbLogs.ContextMenuStrip = this.contextMenuStrip2;
            this.rtbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLogs.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.rtbLogs.Location = new System.Drawing.Point(3, 3);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.Size = new System.Drawing.Size(878, 511);
            this.rtbLogs.TabIndex = 2;
            this.rtbLogs.Text = "";
            this.rtbLogs.TextChanged += new System.EventHandler(this.rtbLogs_TextChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.saveLogToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(122, 48);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.clearLogToolStripMenuItem.Text = "Clear log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // saveLogToolStripMenuItem
            // 
            this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveLogToolStripMenuItem.Text = "Save log";
            this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.bindingNavigator1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(884, 517);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RECORDS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(878, 486);
            this.dataGridView2.TabIndex = 10;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(3, 489);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(878, 25);
            this.bindingNavigator1.TabIndex = 7;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disconnectClientToolStripMenuItem,
            this.readNameplateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 48);
            // 
            // disconnectClientToolStripMenuItem
            // 
            this.disconnectClientToolStripMenuItem.Name = "disconnectClientToolStripMenuItem";
            this.disconnectClientToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.disconnectClientToolStripMenuItem.Text = "Disconnect Client";
            this.disconnectClientToolStripMenuItem.Click += new System.EventHandler(this.disconnectClientToolStripMenuItem_Click);
            // 
            // readNameplateToolStripMenuItem
            // 
            this.readNameplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameplateToolStripMenuItem,
            this.instantToolStripMenuItem,
            this.billingToolStripMenuItem});
            this.readNameplateToolStripMenuItem.Name = "readNameplateToolStripMenuItem";
            this.readNameplateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.readNameplateToolStripMenuItem.Text = "Read";
            // 
            // nameplateToolStripMenuItem
            // 
            this.nameplateToolStripMenuItem.Name = "nameplateToolStripMenuItem";
            this.nameplateToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.nameplateToolStripMenuItem.Text = "Nameplate";
            this.nameplateToolStripMenuItem.Click += new System.EventHandler(this.nameplateToolStripMenuItem_Click);
            // 
            // instantToolStripMenuItem
            // 
            this.instantToolStripMenuItem.Name = "instantToolStripMenuItem";
            this.instantToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.instantToolStripMenuItem.Text = "Instant";
            this.instantToolStripMenuItem.Click += new System.EventHandler(this.instantToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.billingToolStripMenuItem.Text = "Billing";
            this.billingToolStripMenuItem.Click += new System.EventHandler(this.billingToolStripMenuItem_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.CurrentItemChanged += new System.EventHandler(this.bindingSource1_CurrentItemChanged);
            // 
            // PushListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 576);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PushListener";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP SERVER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PushListener_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel notify;
        private System.Windows.Forms.ToolStripStatusLabel serverNotify;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_StopServer;
        private System.Windows.Forms.Button btn_startServer;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem disconnectClientToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readNameplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instantToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel resultnotify;
        private System.Windows.Forms.CheckBox ch_Ipv6;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem selectMeterToDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectMeterToolStripMenuItem;
    }
}