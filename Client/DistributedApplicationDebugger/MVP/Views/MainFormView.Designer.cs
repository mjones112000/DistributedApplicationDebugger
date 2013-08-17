namespace DistributedApplicationDebugger.Views
{
  partial class MainFormView
  {
    /// <summary>
    /// Required designer variable.t
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormView));
            this.numberOfNodesLabel = new System.Windows.Forms.Label();
            this.nodesCounter = new System.Windows.Forms.NumericUpDown();
            this.mainSplitter = new System.Windows.Forms.SplitContainer();
            this.nodePanelHost1 = new DistributedApplicationDebugger.Views.NodePanelHost();
            this.remoteConnectionTabs = new System.Windows.Forms.TabControl();
            this.configurationTab = new System.Windows.Forms.TabPage();
            this.remoteConnectionsSplitter = new System.Windows.Forms.SplitContainer();
            this.splitTransList = new System.Windows.Forms.SplitContainer();
            this.downButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.downButton = new System.Windows.Forms.ToolStripButton();
            this.upButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.upButton = new System.Windows.Forms.ToolStripButton();
            this.removeButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.addButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.lstTransactions = new System.Windows.Forms.ListBox();
            this.remoteComputerLabel = new System.Windows.Forms.Label();
            this.connectionPortTextBox = new System.Windows.Forms.TextBox();
            this.remoteComputerTextBox = new System.Windows.Forms.TextBox();
            this.transferDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.connectionPortLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.transferDirectoryLabel = new System.Windows.Forms.Label();
            this.connectionLogTab = new System.Windows.Forms.TabPage();
            this.connectionLogSplitter = new System.Windows.Forms.SplitContainer();
            this.connectionLogTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.rawDataTextBox = new System.Windows.Forms.TextBox();
            this.rawDataLabel = new System.Windows.Forms.Label();
            this.remoteConnectionsHeaderPanel = new System.Windows.Forms.Panel();
            this.readFromFileButton = new System.Windows.Forms.Button();
            this.connectinfoCollapseButtonToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectinfoCollapseButton = new System.Windows.Forms.ToolStripButton();
            this.sshConnectionLabel1 = new System.Windows.Forms.Label();
            this.displayGridButtonPanel = new System.Windows.Forms.Panel();
            this.displayGridButtonToolStrip = new System.Windows.Forms.ToolStrip();
            this.displayGridButton = new System.Windows.Forms.ToolStripButton();
            this.executableSplitContainer = new System.Windows.Forms.SplitContainer();
            this.executablePrefixLabel = new System.Windows.Forms.Label();
            this.executableTextBox = new System.Windows.Forms.TextBox();
            this.replaySessionLabel = new System.Windows.Forms.Label();
            this.replayLabel = new System.Windows.Forms.Label();
            this.sessionNameLabel = new System.Windows.Forms.Label();
            this.cancelReplayPanel = new System.Windows.Forms.Panel();
            this.cancelReplayButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.cancelReplayButton = new System.Windows.Forms.ToolStripButton();
            this.hostFileLabel = new System.Windows.Forms.Label();
            this.sessionNameTextBox = new System.Windows.Forms.TextBox();
            this.replayModeLabel = new System.Windows.Forms.Label();
            this.hostFileTextBox = new System.Windows.Forms.TextBox();
            this.paramsLabel = new System.Windows.Forms.Label();
            this.paramsTextBox = new System.Windows.Forms.TextBox();
            this.statusPictureBox = new System.Windows.Forms.PictureBox();
            this.navButtonsPanel = new System.Windows.Forms.Panel();
            this.connectionButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.connectionButton = new System.Windows.Forms.ToolStripButton();
            this.cancelButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.cancelButton = new System.Windows.Forms.ToolStripButton();
            this.playButtonPanel = new System.Windows.Forms.Panel();
            this.playButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.playButton = new DistributedApplicationDebugger.RedrawToolStripSplitButton();
            this.recordButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.recordButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.executableLabel = new System.Windows.Forms.Label();
            this.mainViewFooter = new System.Windows.Forms.Panel();
            this.sshConnectionLabel2 = new System.Windows.Forms.Label();
            this.connectinfoDisplayButtonToolStrip = new DistributedApplicationDebugger.RedrawToolStrip();
            this.connectinfoDisplayButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusImages = new System.Windows.Forms.ImageList(this.components);
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nodesCounter)).BeginInit();
            this.mainSplitter.Panel1.SuspendLayout();
            this.mainSplitter.Panel2.SuspendLayout();
            this.mainSplitter.SuspendLayout();
            this.remoteConnectionTabs.SuspendLayout();
            this.configurationTab.SuspendLayout();
            this.remoteConnectionsSplitter.Panel1.SuspendLayout();
            this.remoteConnectionsSplitter.Panel2.SuspendLayout();
            this.remoteConnectionsSplitter.SuspendLayout();
            this.splitTransList.Panel1.SuspendLayout();
            this.splitTransList.Panel2.SuspendLayout();
            this.splitTransList.SuspendLayout();
            this.downButtonToolStrip.SuspendLayout();
            this.upButtonToolStrip.SuspendLayout();
            this.removeButtonToolStrip.SuspendLayout();
            this.addButtonToolStrip.SuspendLayout();
            this.connectionLogTab.SuspendLayout();
            this.connectionLogSplitter.Panel1.SuspendLayout();
            this.connectionLogSplitter.Panel2.SuspendLayout();
            this.connectionLogSplitter.SuspendLayout();
            this.remoteConnectionsHeaderPanel.SuspendLayout();
            this.connectinfoCollapseButtonToolStrip.SuspendLayout();
            this.displayGridButtonPanel.SuspendLayout();
            this.displayGridButtonToolStrip.SuspendLayout();
            this.executableSplitContainer.Panel1.SuspendLayout();
            this.executableSplitContainer.Panel2.SuspendLayout();
            this.executableSplitContainer.SuspendLayout();
            this.cancelReplayPanel.SuspendLayout();
            this.cancelReplayButtonToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).BeginInit();
            this.navButtonsPanel.SuspendLayout();
            this.connectionButtonToolStrip.SuspendLayout();
            this.cancelButtonToolStrip.SuspendLayout();
            this.playButtonPanel.SuspendLayout();
            this.playButtonToolStrip.SuspendLayout();
            this.recordButtonToolStrip.SuspendLayout();
            this.settingsButtonToolStrip.SuspendLayout();
            this.mainViewFooter.SuspendLayout();
            this.connectinfoDisplayButtonToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // numberOfNodesLabel
            // 
            this.numberOfNodesLabel.Location = new System.Drawing.Point(80, 6);
            this.numberOfNodesLabel.Name = "numberOfNodesLabel";
            this.numberOfNodesLabel.Size = new System.Drawing.Size(43, 18);
            this.numberOfNodesLabel.TabIndex = 0;
            this.numberOfNodesLabel.Text = "Nodes:";
            this.numberOfNodesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nodesCounter
            // 
            this.nodesCounter.Location = new System.Drawing.Point(123, 7);
            this.nodesCounter.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nodesCounter.Name = "nodesCounter";
            this.nodesCounter.Size = new System.Drawing.Size(65, 20);
            this.nodesCounter.TabIndex = 1;
            // 
            // mainSplitter
            // 
            this.mainSplitter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitter.Location = new System.Drawing.Point(0, 64);
            this.mainSplitter.Name = "mainSplitter";
            this.mainSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitter.Panel1
            // 
            this.mainSplitter.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.mainSplitter.Panel1.Controls.Add(this.nodePanelHost1);
            this.mainSplitter.Panel1MinSize = 0;
            // 
            // mainSplitter.Panel2
            // 
            this.mainSplitter.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.mainSplitter.Panel2.Controls.Add(this.remoteConnectionTabs);
            this.mainSplitter.Panel2.Controls.Add(this.remoteConnectionsHeaderPanel);
            this.mainSplitter.Panel2MinSize = 0;
            this.mainSplitter.Size = new System.Drawing.Size(888, 627);
            this.mainSplitter.SplitterDistance = 338;
            this.mainSplitter.TabIndex = 9;
            // 
            // nodePanelHost1
            // 
            this.nodePanelHost1.BARReplacement = null;
            this.nodePanelHost1.DisplayBufferGrid = false;
            this.nodePanelHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodePanelHost1.EOTReplacement = null;
            this.nodePanelHost1.Location = new System.Drawing.Point(0, 0);
            this.nodePanelHost1.Name = "nodePanelHost1";
            this.nodePanelHost1.NodeCount = 0;
            this.nodePanelHost1.ReplayMode = false;
            this.nodePanelHost1.SessionIdle = false;
            this.nodePanelHost1.SessionMode = DistributedApplicationDebugger.Views.SessionMode.Play;
            this.nodePanelHost1.SessionRunning = false;
            this.nodePanelHost1.Size = new System.Drawing.Size(888, 338);
            this.nodePanelHost1.SOHReplacement = null;
            this.nodePanelHost1.TabIndex = 1;
            // 
            // remoteConnectionTabs
            // 
            this.remoteConnectionTabs.Controls.Add(this.configurationTab);
            this.remoteConnectionTabs.Controls.Add(this.connectionLogTab);
            this.remoteConnectionTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteConnectionTabs.Location = new System.Drawing.Point(0, 31);
            this.remoteConnectionTabs.Name = "remoteConnectionTabs";
            this.remoteConnectionTabs.SelectedIndex = 0;
            this.remoteConnectionTabs.Size = new System.Drawing.Size(888, 254);
            this.remoteConnectionTabs.TabIndex = 1;
            // 
            // configurationTab
            // 
            this.configurationTab.Controls.Add(this.remoteConnectionsSplitter);
            this.configurationTab.Location = new System.Drawing.Point(4, 22);
            this.configurationTab.Name = "configurationTab";
            this.configurationTab.Padding = new System.Windows.Forms.Padding(3);
            this.configurationTab.Size = new System.Drawing.Size(880, 228);
            this.configurationTab.TabIndex = 0;
            this.configurationTab.Text = "Configuration";
            this.configurationTab.UseVisualStyleBackColor = true;
            // 
            // remoteConnectionsSplitter
            // 
            this.remoteConnectionsSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteConnectionsSplitter.Location = new System.Drawing.Point(3, 3);
            this.remoteConnectionsSplitter.Name = "remoteConnectionsSplitter";
            // 
            // remoteConnectionsSplitter.Panel1
            // 
            this.remoteConnectionsSplitter.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.remoteConnectionsSplitter.Panel1.Controls.Add(this.splitTransList);
            // 
            // remoteConnectionsSplitter.Panel2
            // 
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.remoteComputerLabel);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.connectionPortTextBox);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.remoteComputerTextBox);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.transferDirectoryTextBox);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.connectionPortLabel);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.passwordTextBox);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.passwordLabel);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.userNameTextBox);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.userNameLabel);
            this.remoteConnectionsSplitter.Panel2.Controls.Add(this.transferDirectoryLabel);
            this.remoteConnectionsSplitter.Size = new System.Drawing.Size(874, 222);
            this.remoteConnectionsSplitter.SplitterDistance = 299;
            this.remoteConnectionsSplitter.TabIndex = 2;
            // 
            // splitTransList
            // 
            this.splitTransList.BackColor = System.Drawing.Color.Transparent;
            this.splitTransList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTransList.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTransList.IsSplitterFixed = true;
            this.splitTransList.Location = new System.Drawing.Point(0, 0);
            this.splitTransList.Name = "splitTransList";
            this.splitTransList.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTransList.Panel1
            // 
            this.splitTransList.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitTransList.Panel1.Controls.Add(this.downButtonToolStrip);
            this.splitTransList.Panel1.Controls.Add(this.upButtonToolStrip);
            this.splitTransList.Panel1.Controls.Add(this.removeButtonToolStrip);
            this.splitTransList.Panel1.Controls.Add(this.addButtonToolStrip);
            // 
            // splitTransList.Panel2
            // 
            this.splitTransList.Panel2.Controls.Add(this.lstTransactions);
            this.splitTransList.Size = new System.Drawing.Size(299, 222);
            this.splitTransList.SplitterDistance = 25;
            this.splitTransList.SplitterWidth = 2;
            this.splitTransList.TabIndex = 0;
            // 
            // downButtonToolStrip
            // 
            this.downButtonToolStrip.AutoSize = false;
            this.downButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.downButtonToolStrip.ButtonImage = null;
            this.downButtonToolStrip.CanOverflow = false;
            this.downButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.downButtonToolStrip.DropDownList = null;
            this.downButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.downButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downButton});
            this.downButtonToolStrip.Location = new System.Drawing.Point(251, 0);
            this.downButtonToolStrip.Name = "downButtonToolStrip";
            this.downButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.downButtonToolStrip.Size = new System.Drawing.Size(24, 25);
            this.downButtonToolStrip.TabIndex = 3;
            this.downButtonToolStrip.ToolTip = "";
            // 
            // downButton
            // 
            this.downButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
            this.downButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(23, 20);
            // 
            // upButtonToolStrip
            // 
            this.upButtonToolStrip.AutoSize = false;
            this.upButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.upButtonToolStrip.ButtonImage = null;
            this.upButtonToolStrip.CanOverflow = false;
            this.upButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.upButtonToolStrip.DropDownList = null;
            this.upButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.upButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upButton});
            this.upButtonToolStrip.Location = new System.Drawing.Point(275, 0);
            this.upButtonToolStrip.Name = "upButtonToolStrip";
            this.upButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.upButtonToolStrip.Size = new System.Drawing.Size(24, 25);
            this.upButtonToolStrip.TabIndex = 2;
            this.upButtonToolStrip.ToolTip = "";
            // 
            // upButton
            // 
            this.upButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(23, 20);
            // 
            // removeButtonToolStrip
            // 
            this.removeButtonToolStrip.AutoSize = false;
            this.removeButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.removeButtonToolStrip.ButtonImage = null;
            this.removeButtonToolStrip.CanOverflow = false;
            this.removeButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.removeButtonToolStrip.DropDownList = null;
            this.removeButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.removeButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeButton});
            this.removeButtonToolStrip.Location = new System.Drawing.Point(24, 0);
            this.removeButtonToolStrip.Name = "removeButtonToolStrip";
            this.removeButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.removeButtonToolStrip.Size = new System.Drawing.Size(24, 25);
            this.removeButtonToolStrip.TabIndex = 1;
            this.removeButtonToolStrip.ToolTip = "";
            // 
            // removeButton
            // 
            this.removeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeButton.Image = ((System.Drawing.Image)(resources.GetObject("removeButton.Image")));
            this.removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(23, 20);
            // 
            // addButtonToolStrip
            // 
            this.addButtonToolStrip.AutoSize = false;
            this.addButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.addButtonToolStrip.ButtonImage = null;
            this.addButtonToolStrip.CanOverflow = false;
            this.addButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.addButtonToolStrip.DropDownList = null;
            this.addButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.addButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton});
            this.addButtonToolStrip.Location = new System.Drawing.Point(0, 0);
            this.addButtonToolStrip.Name = "addButtonToolStrip";
            this.addButtonToolStrip.Size = new System.Drawing.Size(24, 25);
            this.addButtonToolStrip.TabIndex = 0;
            this.addButtonToolStrip.ToolTip = "";
            // 
            // addButton
            // 
            this.addButton.AutoToolTip = false;
            this.addButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
            this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(22, 20);
            this.addButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // lstTransactions
            // 
            this.lstTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTransactions.FormattingEnabled = true;
            this.lstTransactions.IntegralHeight = false;
            this.lstTransactions.ItemHeight = 20;
            this.lstTransactions.Location = new System.Drawing.Point(0, 0);
            this.lstTransactions.Name = "lstTransactions";
            this.lstTransactions.Size = new System.Drawing.Size(299, 195);
            this.lstTransactions.TabIndex = 0;
            // 
            // remoteComputerLabel
            // 
            this.remoteComputerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remoteComputerLabel.Location = new System.Drawing.Point(20, 26);
            this.remoteComputerLabel.Name = "remoteComputerLabel";
            this.remoteComputerLabel.Size = new System.Drawing.Size(145, 26);
            this.remoteComputerLabel.TabIndex = 22;
            this.remoteComputerLabel.Text = "Remote Computer:";
            this.remoteComputerLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // connectionPortTextBox
            // 
            this.connectionPortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionPortTextBox.Location = new System.Drawing.Point(171, 146);
            this.connectionPortTextBox.Name = "connectionPortTextBox";
            this.connectionPortTextBox.Size = new System.Drawing.Size(340, 20);
            this.connectionPortTextBox.TabIndex = 26;
            // 
            // remoteComputerTextBox
            // 
            this.remoteComputerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteComputerTextBox.Location = new System.Drawing.Point(171, 26);
            this.remoteComputerTextBox.Name = "remoteComputerTextBox";
            this.remoteComputerTextBox.Size = new System.Drawing.Size(340, 20);
            this.remoteComputerTextBox.TabIndex = 17;
            // 
            // transferDirectoryTextBox
            // 
            this.transferDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDirectoryTextBox.Location = new System.Drawing.Point(171, 116);
            this.transferDirectoryTextBox.Name = "transferDirectoryTextBox";
            this.transferDirectoryTextBox.Size = new System.Drawing.Size(340, 20);
            this.transferDirectoryTextBox.TabIndex = 24;
            // 
            // connectionPortLabel
            // 
            this.connectionPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionPortLabel.Location = new System.Drawing.Point(37, 146);
            this.connectionPortLabel.Name = "connectionPortLabel";
            this.connectionPortLabel.Size = new System.Drawing.Size(128, 16);
            this.connectionPortLabel.TabIndex = 25;
            this.connectionPortLabel.Text = "Connection Port:";
            this.connectionPortLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(171, 86);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(340, 20);
            this.passwordTextBox.TabIndex = 21;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(44, 86);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(121, 20);
            this.passwordLabel.TabIndex = 20;
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userNameTextBox.Location = new System.Drawing.Point(171, 56);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(340, 20);
            this.userNameTextBox.TabIndex = 19;
            // 
            // userNameLabel
            // 
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.Location = new System.Drawing.Point(44, 56);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(121, 19);
            this.userNameLabel.TabIndex = 18;
            this.userNameLabel.Text = "User Name:";
            this.userNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // transferDirectoryLabel
            // 
            this.transferDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transferDirectoryLabel.Location = new System.Drawing.Point(44, 116);
            this.transferDirectoryLabel.Name = "transferDirectoryLabel";
            this.transferDirectoryLabel.Size = new System.Drawing.Size(121, 16);
            this.transferDirectoryLabel.TabIndex = 23;
            this.transferDirectoryLabel.Text = "Transfer Directory:";
            this.transferDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // connectionLogTab
            // 
            this.connectionLogTab.Controls.Add(this.connectionLogSplitter);
            this.connectionLogTab.Location = new System.Drawing.Point(4, 22);
            this.connectionLogTab.Name = "connectionLogTab";
            this.connectionLogTab.Padding = new System.Windows.Forms.Padding(3);
            this.connectionLogTab.Size = new System.Drawing.Size(880, 228);
            this.connectionLogTab.TabIndex = 1;
            this.connectionLogTab.Text = "Connection Log";
            this.connectionLogTab.UseVisualStyleBackColor = true;
            // 
            // connectionLogSplitter
            // 
            this.connectionLogSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionLogSplitter.IsSplitterFixed = true;
            this.connectionLogSplitter.Location = new System.Drawing.Point(3, 3);
            this.connectionLogSplitter.Name = "connectionLogSplitter";
            // 
            // connectionLogSplitter.Panel1
            // 
            this.connectionLogSplitter.Panel1.Controls.Add(this.connectionLogTextBox);
            this.connectionLogSplitter.Panel1.Controls.Add(this.statusLabel);
            // 
            // connectionLogSplitter.Panel2
            // 
            this.connectionLogSplitter.Panel2.Controls.Add(this.rawDataTextBox);
            this.connectionLogSplitter.Panel2.Controls.Add(this.rawDataLabel);
            this.connectionLogSplitter.Size = new System.Drawing.Size(874, 222);
            this.connectionLogSplitter.SplitterDistance = 436;
            this.connectionLogSplitter.TabIndex = 2;
            // 
            // connectionLogTextBox
            // 
            this.connectionLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionLogTextBox.Location = new System.Drawing.Point(0, 24);
            this.connectionLogTextBox.Multiline = true;
            this.connectionLogTextBox.Name = "connectionLogTextBox";
            this.connectionLogTextBox.ReadOnly = true;
            this.connectionLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.connectionLogTextBox.Size = new System.Drawing.Size(436, 198);
            this.connectionLogTextBox.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(0, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(436, 24);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Status";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rawDataTextBox
            // 
            this.rawDataTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawDataTextBox.Location = new System.Drawing.Point(0, 24);
            this.rawDataTextBox.Multiline = true;
            this.rawDataTextBox.Name = "rawDataTextBox";
            this.rawDataTextBox.ReadOnly = true;
            this.rawDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawDataTextBox.Size = new System.Drawing.Size(434, 198);
            this.rawDataTextBox.TabIndex = 3;
            // 
            // rawDataLabel
            // 
            this.rawDataLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rawDataLabel.Location = new System.Drawing.Point(0, 0);
            this.rawDataLabel.Name = "rawDataLabel";
            this.rawDataLabel.Size = new System.Drawing.Size(434, 24);
            this.rawDataLabel.TabIndex = 4;
            this.rawDataLabel.Text = "Raw Data";
            this.rawDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remoteConnectionsHeaderPanel
            // 
            this.remoteConnectionsHeaderPanel.Controls.Add(this.readFromFileButton);
            this.remoteConnectionsHeaderPanel.Controls.Add(this.connectinfoCollapseButtonToolStrip);
            this.remoteConnectionsHeaderPanel.Controls.Add(this.sshConnectionLabel1);
            this.remoteConnectionsHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.remoteConnectionsHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.remoteConnectionsHeaderPanel.Name = "remoteConnectionsHeaderPanel";
            this.remoteConnectionsHeaderPanel.Size = new System.Drawing.Size(888, 31);
            this.remoteConnectionsHeaderPanel.TabIndex = 3;
            // 
            // readFromFileButton
            // 
            this.readFromFileButton.Location = new System.Drawing.Point(10, 3);
            this.readFromFileButton.Name = "readFromFileButton";
            this.readFromFileButton.Size = new System.Drawing.Size(92, 21);
            this.readFromFileButton.TabIndex = 8;
            this.readFromFileButton.Text = "Read From File";
            this.readFromFileButton.UseVisualStyleBackColor = true;
            this.readFromFileButton.Visible = false;
            this.readFromFileButton.Click += new System.EventHandler(this.readFromFileButton_Click);
            // 
            // connectinfoCollapseButtonToolStrip
            // 
            this.connectinfoCollapseButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.connectinfoCollapseButtonToolStrip.CanOverflow = false;
            this.connectinfoCollapseButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.connectinfoCollapseButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.connectinfoCollapseButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectinfoCollapseButton});
            this.connectinfoCollapseButtonToolStrip.Location = new System.Drawing.Point(858, 0);
            this.connectinfoCollapseButtonToolStrip.Name = "connectinfoCollapseButtonToolStrip";
            this.connectinfoCollapseButtonToolStrip.Size = new System.Drawing.Size(30, 31);
            this.connectinfoCollapseButtonToolStrip.TabIndex = 6;
            // 
            // connectinfoCollapseButton
            // 
            this.connectinfoCollapseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.connectinfoCollapseButton.AutoSize = false;
            this.connectinfoCollapseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectinfoCollapseButton.Image = global::DistributedApplicationDebugger.Properties.Resources.ArrowDown;
            this.connectinfoCollapseButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.connectinfoCollapseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectinfoCollapseButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.connectinfoCollapseButton.Name = "connectinfoCollapseButton";
            this.connectinfoCollapseButton.Size = new System.Drawing.Size(29, 28);
            this.connectinfoCollapseButton.ToolTipText = "Hide Connection Information";
            // 
            // sshConnectionLabel1
            // 
            this.sshConnectionLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sshConnectionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sshConnectionLabel1.Location = new System.Drawing.Point(355, 8);
            this.sshConnectionLabel1.Name = "sshConnectionLabel1";
            this.sshConnectionLabel1.Size = new System.Drawing.Size(117, 16);
            this.sshConnectionLabel1.TabIndex = 2;
            this.sshConnectionLabel1.Text = "SSH Configuration";
            this.sshConnectionLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // displayGridButtonPanel
            // 
            this.displayGridButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.displayGridButtonPanel.Controls.Add(this.displayGridButtonToolStrip);
            this.displayGridButtonPanel.Location = new System.Drawing.Point(853, 36);
            this.displayGridButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.displayGridButtonPanel.Name = "displayGridButtonPanel";
            this.displayGridButtonPanel.Size = new System.Drawing.Size(39, 31);
            this.displayGridButtonPanel.TabIndex = 5;
            // 
            // displayGridButtonToolStrip
            // 
            this.displayGridButtonToolStrip.AutoSize = false;
            this.displayGridButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.displayGridButtonToolStrip.CanOverflow = false;
            this.displayGridButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.displayGridButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.displayGridButtonToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.displayGridButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayGridButton});
            this.displayGridButtonToolStrip.Location = new System.Drawing.Point(-2, 0);
            this.displayGridButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.displayGridButtonToolStrip.Name = "displayGridButtonToolStrip";
            this.displayGridButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.displayGridButtonToolStrip.Size = new System.Drawing.Size(41, 31);
            this.displayGridButtonToolStrip.TabIndex = 4;
            // 
            // displayGridButton
            // 
            this.displayGridButton.AutoSize = false;
            this.displayGridButton.AutoToolTip = false;
            this.displayGridButton.CheckOnClick = true;
            this.displayGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.displayGridButton.Enabled = false;
            this.displayGridButton.Image = global::DistributedApplicationDebugger.Properties.Resources.tableSearch;
            this.displayGridButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.displayGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayGridButton.Margin = new System.Windows.Forms.Padding(0);
            this.displayGridButton.Name = "displayGridButton";
            this.displayGridButton.Size = new System.Drawing.Size(32, 28);
            this.displayGridButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // executableSplitContainer
            // 
            this.executableSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.executableSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.executableSplitContainer.Location = new System.Drawing.Point(255, 6);
            this.executableSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.executableSplitContainer.Name = "executableSplitContainer";
            // 
            // executableSplitContainer.Panel1
            // 
            this.executableSplitContainer.Panel1.Controls.Add(this.executablePrefixLabel);
            this.executableSplitContainer.Panel1MinSize = 0;
            // 
            // executableSplitContainer.Panel2
            // 
            this.executableSplitContainer.Panel2.Controls.Add(this.executableTextBox);
            this.executableSplitContainer.Size = new System.Drawing.Size(274, 23);
            this.executableSplitContainer.SplitterDistance = 0;
            this.executableSplitContainer.SplitterWidth = 1;
            this.executableSplitContainer.TabIndex = 15;
            // 
            // executablePrefixLabel
            // 
            this.executablePrefixLabel.AutoSize = true;
            this.executablePrefixLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.executablePrefixLabel.Location = new System.Drawing.Point(0, 0);
            this.executablePrefixLabel.Margin = new System.Windows.Forms.Padding(0);
            this.executablePrefixLabel.MinimumSize = new System.Drawing.Size(0, 23);
            this.executablePrefixLabel.Name = "executablePrefixLabel";
            this.executablePrefixLabel.Size = new System.Drawing.Size(0, 23);
            this.executablePrefixLabel.TabIndex = 0;
            this.executablePrefixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // executableTextBox
            // 
            this.executableTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.executableTextBox.Location = new System.Drawing.Point(0, 0);
            this.executableTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.executableTextBox.Name = "executableTextBox";
            this.executableTextBox.Size = new System.Drawing.Size(273, 20);
            this.executableTextBox.TabIndex = 2;
            // 
            // replaySessionLabel
            // 
            this.replaySessionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replaySessionLabel.Location = new System.Drawing.Point(577, 38);
            this.replaySessionLabel.Name = "replaySessionLabel";
            this.replaySessionLabel.Size = new System.Drawing.Size(125, 20);
            this.replaySessionLabel.TabIndex = 2;
            this.replaySessionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // replayLabel
            // 
            this.replayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replayLabel.Location = new System.Drawing.Point(535, 36);
            this.replayLabel.Name = "replayLabel";
            this.replayLabel.Size = new System.Drawing.Size(44, 23);
            this.replayLabel.TabIndex = 12;
            this.replayLabel.Text = "Replay:";
            this.replayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sessionNameLabel
            // 
            this.sessionNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionNameLabel.Location = new System.Drawing.Point(532, 5);
            this.sessionNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.sessionNameLabel.Name = "sessionNameLabel";
            this.sessionNameLabel.Size = new System.Drawing.Size(47, 23);
            this.sessionNameLabel.TabIndex = 4;
            this.sessionNameLabel.Text = "Session:";
            this.sessionNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cancelReplayPanel
            // 
            this.cancelReplayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelReplayPanel.Controls.Add(this.cancelReplayButtonToolStrip);
            this.cancelReplayPanel.Location = new System.Drawing.Point(703, 35);
            this.cancelReplayPanel.Name = "cancelReplayPanel";
            this.cancelReplayPanel.Size = new System.Drawing.Size(28, 28);
            this.cancelReplayPanel.TabIndex = 2;
            // 
            // cancelReplayButtonToolStrip
            // 
            this.cancelReplayButtonToolStrip.AutoSize = false;
            this.cancelReplayButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.cancelReplayButtonToolStrip.ButtonImage = null;
            this.cancelReplayButtonToolStrip.CanOverflow = false;
            this.cancelReplayButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelReplayButtonToolStrip.DropDownList = null;
            this.cancelReplayButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.cancelReplayButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelReplayButton});
            this.cancelReplayButtonToolStrip.Location = new System.Drawing.Point(2, 0);
            this.cancelReplayButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.cancelReplayButtonToolStrip.Name = "cancelReplayButtonToolStrip";
            this.cancelReplayButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.cancelReplayButtonToolStrip.Size = new System.Drawing.Size(26, 28);
            this.cancelReplayButtonToolStrip.TabIndex = 14;
            this.cancelReplayButtonToolStrip.ToolTip = "";
            // 
            // cancelReplayButton
            // 
            this.cancelReplayButton.AutoSize = false;
            this.cancelReplayButton.AutoToolTip = false;
            this.cancelReplayButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelReplayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cancelReplayButton.Image = global::DistributedApplicationDebugger.Properties.Resources.link_break_icon16;
            this.cancelReplayButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelReplayButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cancelReplayButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelReplayButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cancelReplayButton.Name = "cancelReplayButton";
            this.cancelReplayButton.Size = new System.Drawing.Size(24, 24);
            this.cancelReplayButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // hostFileLabel
            // 
            this.hostFileLabel.Location = new System.Drawing.Point(3, 31);
            this.hostFileLabel.Name = "hostFileLabel";
            this.hostFileLabel.Size = new System.Drawing.Size(52, 23);
            this.hostFileLabel.TabIndex = 6;
            this.hostFileLabel.Text = "Host File:";
            this.hostFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sessionNameTextBox
            // 
            this.sessionNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionNameTextBox.Location = new System.Drawing.Point(580, 7);
            this.sessionNameTextBox.Name = "sessionNameTextBox";
            this.sessionNameTextBox.Size = new System.Drawing.Size(114, 20);
            this.sessionNameTextBox.TabIndex = 3;
            // 
            // replayModeLabel
            // 
            this.replayModeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replayModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replayModeLabel.ForeColor = System.Drawing.Color.Red;
            this.replayModeLabel.Location = new System.Drawing.Point(733, 39);
            this.replayModeLabel.Name = "replayModeLabel";
            this.replayModeLabel.Size = new System.Drawing.Size(113, 18);
            this.replayModeLabel.TabIndex = 14;
            this.replayModeLabel.Text = "REPLAY MODE";
            this.replayModeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hostFileTextBox
            // 
            this.hostFileTextBox.Location = new System.Drawing.Point(55, 33);
            this.hostFileTextBox.Name = "hostFileTextBox";
            this.hostFileTextBox.Size = new System.Drawing.Size(134, 20);
            this.hostFileTextBox.TabIndex = 4;
            // 
            // paramsLabel
            // 
            this.paramsLabel.Location = new System.Drawing.Point(209, 32);
            this.paramsLabel.Name = "paramsLabel";
            this.paramsLabel.Size = new System.Drawing.Size(48, 23);
            this.paramsLabel.TabIndex = 8;
            this.paramsLabel.Text = "Params:";
            this.paramsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // paramsTextBox
            // 
            this.paramsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramsTextBox.Location = new System.Drawing.Point(257, 35);
            this.paramsTextBox.Name = "paramsTextBox";
            this.paramsTextBox.Size = new System.Drawing.Size(272, 20);
            this.paramsTextBox.TabIndex = 5;
            // 
            // statusPictureBox
            // 
            this.statusPictureBox.Location = new System.Drawing.Point(10, 6);
            this.statusPictureBox.Name = "statusPictureBox";
            this.statusPictureBox.Size = new System.Drawing.Size(20, 20);
            this.statusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.statusPictureBox.TabIndex = 11;
            this.statusPictureBox.TabStop = false;
            // 
            // navButtonsPanel
            // 
            this.navButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.navButtonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.navButtonsPanel.Controls.Add(this.connectionButtonToolStrip);
            this.navButtonsPanel.Controls.Add(this.cancelButtonToolStrip);
            this.navButtonsPanel.Controls.Add(this.playButtonPanel);
            this.navButtonsPanel.Controls.Add(this.recordButtonToolStrip);
            this.navButtonsPanel.Controls.Add(this.settingsButtonToolStrip);
            this.navButtonsPanel.Location = new System.Drawing.Point(699, 0);
            this.navButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.navButtonsPanel.Name = "navButtonsPanel";
            this.navButtonsPanel.Size = new System.Drawing.Size(189, 32);
            this.navButtonsPanel.TabIndex = 7;
            // 
            // connectionButtonToolStrip
            // 
            this.connectionButtonToolStrip.AutoSize = false;
            this.connectionButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.connectionButtonToolStrip.ButtonImage = null;
            this.connectionButtonToolStrip.CanOverflow = false;
            this.connectionButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.connectionButtonToolStrip.DropDownList = null;
            this.connectionButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.connectionButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionButton});
            this.connectionButtonToolStrip.Location = new System.Drawing.Point(6, 0);
            this.connectionButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.connectionButtonToolStrip.Name = "connectionButtonToolStrip";
            this.connectionButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.connectionButtonToolStrip.Size = new System.Drawing.Size(32, 32);
            this.connectionButtonToolStrip.TabIndex = 3;
            this.connectionButtonToolStrip.ToolTip = "";
            // 
            // connectionButton
            // 
            this.connectionButton.AutoToolTip = false;
            this.connectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectionButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Connection;
            this.connectionButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.connectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(31, 28);
            this.connectionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // cancelButtonToolStrip
            // 
            this.cancelButtonToolStrip.AutoSize = false;
            this.cancelButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.cancelButtonToolStrip.ButtonImage = null;
            this.cancelButtonToolStrip.CanOverflow = false;
            this.cancelButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButtonToolStrip.DropDownList = null;
            this.cancelButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.cancelButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelButton});
            this.cancelButtonToolStrip.Location = new System.Drawing.Point(38, 0);
            this.cancelButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.cancelButtonToolStrip.Name = "cancelButtonToolStrip";
            this.cancelButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.cancelButtonToolStrip.Size = new System.Drawing.Size(40, 32);
            this.cancelButtonToolStrip.TabIndex = 2;
            this.cancelButtonToolStrip.ToolTip = "";
            // 
            // cancelButton
            // 
            this.cancelButton.AutoToolTip = false;
            this.cancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cancelButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Cancel;
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(39, 28);
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // playButtonPanel
            // 
            this.playButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.playButtonPanel.Controls.Add(this.playButtonToolStrip);
            this.playButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.playButtonPanel.Location = new System.Drawing.Point(78, 0);
            this.playButtonPanel.Name = "playButtonPanel";
            this.playButtonPanel.Size = new System.Drawing.Size(46, 32);
            this.playButtonPanel.TabIndex = 2;
            // 
            // playButtonToolStrip
            // 
            this.playButtonToolStrip.AutoSize = false;
            this.playButtonToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.playButtonToolStrip.ButtonImage = null;
            this.playButtonToolStrip.CanOverflow = false;
            this.playButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.playButtonToolStrip.DropDownList = null;
            this.playButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.playButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playButton});
            this.playButtonToolStrip.Location = new System.Drawing.Point(0, 0);
            this.playButtonToolStrip.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.playButtonToolStrip.Name = "playButtonToolStrip";
            this.playButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.playButtonToolStrip.Size = new System.Drawing.Size(46, 32);
            this.playButtonToolStrip.TabIndex = 1;
            this.playButtonToolStrip.Text = "toolStrip1";
            this.playButtonToolStrip.ToolTip = "";
            // 
            // playButton
            // 
            this.playButton.AutoToolTip = false;
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Play;
            this.playButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(45, 28);
            this.playButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // recordButtonToolStrip
            // 
            this.recordButtonToolStrip.AutoSize = false;
            this.recordButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.recordButtonToolStrip.ButtonImage = null;
            this.recordButtonToolStrip.CanOverflow = false;
            this.recordButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.recordButtonToolStrip.DropDownList = null;
            this.recordButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.recordButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordButton});
            this.recordButtonToolStrip.Location = new System.Drawing.Point(124, 0);
            this.recordButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.recordButtonToolStrip.Name = "recordButtonToolStrip";
            this.recordButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.recordButtonToolStrip.Size = new System.Drawing.Size(32, 32);
            this.recordButtonToolStrip.TabIndex = 1;
            this.recordButtonToolStrip.Text = "toolStrip3";
            this.recordButtonToolStrip.ToolTip = "";
            // 
            // recordButton
            // 
            this.recordButton.AutoToolTip = false;
            this.recordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recordButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Record;
            this.recordButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.recordButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.recordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(31, 28);
            this.recordButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // settingsButtonToolStrip
            // 
            this.settingsButtonToolStrip.AutoSize = false;
            this.settingsButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.settingsButtonToolStrip.ButtonImage = null;
            this.settingsButtonToolStrip.CanOverflow = false;
            this.settingsButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.settingsButtonToolStrip.DropDownList = null;
            this.settingsButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.settingsButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsButton});
            this.settingsButtonToolStrip.Location = new System.Drawing.Point(156, 0);
            this.settingsButtonToolStrip.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.settingsButtonToolStrip.Name = "settingsButtonToolStrip";
            this.settingsButtonToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.settingsButtonToolStrip.Size = new System.Drawing.Size(33, 32);
            this.settingsButtonToolStrip.TabIndex = 4;
            this.settingsButtonToolStrip.ToolTip = "";
            // 
            // settingsButton
            // 
            this.settingsButton.AutoToolTip = false;
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = global::DistributedApplicationDebugger.Properties.Resources.gear;
            this.settingsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(32, 28);
            this.settingsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // executableLabel
            // 
            this.executableLabel.Location = new System.Drawing.Point(194, 5);
            this.executableLabel.Name = "executableLabel";
            this.executableLabel.Size = new System.Drawing.Size(65, 23);
            this.executableLabel.TabIndex = 2;
            this.executableLabel.Text = "Executable:";
            this.executableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mainViewFooter
            // 
            this.mainViewFooter.Controls.Add(this.sshConnectionLabel2);
            this.mainViewFooter.Controls.Add(this.connectinfoDisplayButtonToolStrip);
            this.mainViewFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainViewFooter.Location = new System.Drawing.Point(0, 691);
            this.mainViewFooter.Name = "mainViewFooter";
            this.mainViewFooter.Size = new System.Drawing.Size(888, 33);
            this.mainViewFooter.TabIndex = 4;
            // 
            // sshConnectionLabel2
            // 
            this.sshConnectionLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sshConnectionLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sshConnectionLabel2.Location = new System.Drawing.Point(355, 8);
            this.sshConnectionLabel2.Name = "sshConnectionLabel2";
            this.sshConnectionLabel2.Size = new System.Drawing.Size(117, 16);
            this.sshConnectionLabel2.TabIndex = 7;
            this.sshConnectionLabel2.Text = "SSH Configuration";
            this.sshConnectionLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectinfoDisplayButtonToolStrip
            // 
            this.connectinfoDisplayButtonToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.connectinfoDisplayButtonToolStrip.ButtonImage = null;
            this.connectinfoDisplayButtonToolStrip.CanOverflow = false;
            this.connectinfoDisplayButtonToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.connectinfoDisplayButtonToolStrip.DropDownList = null;
            this.connectinfoDisplayButtonToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.connectinfoDisplayButtonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectinfoDisplayButton});
            this.connectinfoDisplayButtonToolStrip.Location = new System.Drawing.Point(859, 0);
            this.connectinfoDisplayButtonToolStrip.Name = "connectinfoDisplayButtonToolStrip";
            this.connectinfoDisplayButtonToolStrip.Size = new System.Drawing.Size(29, 33);
            this.connectinfoDisplayButtonToolStrip.TabIndex = 6;
            this.connectinfoDisplayButtonToolStrip.ToolTip = "";
            // 
            // connectinfoDisplayButton
            // 
            this.connectinfoDisplayButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.connectinfoDisplayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectinfoDisplayButton.Image = global::DistributedApplicationDebugger.Properties.Resources.ArrowUp;
            this.connectinfoDisplayButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.connectinfoDisplayButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectinfoDisplayButton.Name = "connectinfoDisplayButton";
            this.connectinfoDisplayButton.Size = new System.Drawing.Size(26, 28);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // statusImages
            // 
            this.statusImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("statusImages.ImageStream")));
            this.statusImages.TransparentColor = System.Drawing.Color.Transparent;
            this.statusImages.Images.SetKeyName(0, "GrayButton");
            this.statusImages.Images.SetKeyName(1, "GreenButton");
            this.statusImages.Images.SetKeyName(2, "RedButton");
            this.statusImages.Images.SetKeyName(3, "UpArrow");
            this.statusImages.Images.SetKeyName(4, "DownArrow");
            this.statusImages.Images.SetKeyName(5, "GrayButtonBlank");
            this.statusImages.Images.SetKeyName(6, "SortDown");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.displayGridButtonPanel);
            this.panel1.Controls.Add(this.executableSplitContainer);
            this.panel1.Controls.Add(this.hostFileLabel);
            this.panel1.Controls.Add(this.replaySessionLabel);
            this.panel1.Controls.Add(this.numberOfNodesLabel);
            this.panel1.Controls.Add(this.replayLabel);
            this.panel1.Controls.Add(this.nodesCounter);
            this.panel1.Controls.Add(this.sessionNameLabel);
            this.panel1.Controls.Add(this.executableLabel);
            this.panel1.Controls.Add(this.cancelReplayPanel);
            this.panel1.Controls.Add(this.navButtonsPanel);
            this.panel1.Controls.Add(this.statusPictureBox);
            this.panel1.Controls.Add(this.sessionNameTextBox);
            this.panel1.Controls.Add(this.paramsTextBox);
            this.panel1.Controls.Add(this.replayModeLabel);
            this.panel1.Controls.Add(this.paramsLabel);
            this.panel1.Controls.Add(this.hostFileTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 64);
            this.panel1.TabIndex = 11;
            // 
            // MainFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 724);
            this.Controls.Add(this.mainSplitter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainViewFooter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 700);
            this.Name = "MainFormView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distributed Application Debugger";
            this.Load += new System.EventHandler(this.MainFormView_Load);
            this.Shown += new System.EventHandler(this.MainFormView_Shown);
            this.Resize += new System.EventHandler(this.MainFormView_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nodesCounter)).EndInit();
            this.mainSplitter.Panel1.ResumeLayout(false);
            this.mainSplitter.Panel2.ResumeLayout(false);
            this.mainSplitter.ResumeLayout(false);
            this.remoteConnectionTabs.ResumeLayout(false);
            this.configurationTab.ResumeLayout(false);
            this.remoteConnectionsSplitter.Panel1.ResumeLayout(false);
            this.remoteConnectionsSplitter.Panel2.ResumeLayout(false);
            this.remoteConnectionsSplitter.Panel2.PerformLayout();
            this.remoteConnectionsSplitter.ResumeLayout(false);
            this.splitTransList.Panel1.ResumeLayout(false);
            this.splitTransList.Panel2.ResumeLayout(false);
            this.splitTransList.ResumeLayout(false);
            this.downButtonToolStrip.ResumeLayout(false);
            this.downButtonToolStrip.PerformLayout();
            this.upButtonToolStrip.ResumeLayout(false);
            this.upButtonToolStrip.PerformLayout();
            this.removeButtonToolStrip.ResumeLayout(false);
            this.removeButtonToolStrip.PerformLayout();
            this.addButtonToolStrip.ResumeLayout(false);
            this.addButtonToolStrip.PerformLayout();
            this.connectionLogTab.ResumeLayout(false);
            this.connectionLogSplitter.Panel1.ResumeLayout(false);
            this.connectionLogSplitter.Panel1.PerformLayout();
            this.connectionLogSplitter.Panel2.ResumeLayout(false);
            this.connectionLogSplitter.Panel2.PerformLayout();
            this.connectionLogSplitter.ResumeLayout(false);
            this.remoteConnectionsHeaderPanel.ResumeLayout(false);
            this.remoteConnectionsHeaderPanel.PerformLayout();
            this.connectinfoCollapseButtonToolStrip.ResumeLayout(false);
            this.connectinfoCollapseButtonToolStrip.PerformLayout();
            this.displayGridButtonPanel.ResumeLayout(false);
            this.displayGridButtonToolStrip.ResumeLayout(false);
            this.displayGridButtonToolStrip.PerformLayout();
            this.executableSplitContainer.Panel1.ResumeLayout(false);
            this.executableSplitContainer.Panel1.PerformLayout();
            this.executableSplitContainer.Panel2.ResumeLayout(false);
            this.executableSplitContainer.Panel2.PerformLayout();
            this.executableSplitContainer.ResumeLayout(false);
            this.cancelReplayPanel.ResumeLayout(false);
            this.cancelReplayButtonToolStrip.ResumeLayout(false);
            this.cancelReplayButtonToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).EndInit();
            this.navButtonsPanel.ResumeLayout(false);
            this.connectionButtonToolStrip.ResumeLayout(false);
            this.connectionButtonToolStrip.PerformLayout();
            this.cancelButtonToolStrip.ResumeLayout(false);
            this.cancelButtonToolStrip.PerformLayout();
            this.playButtonPanel.ResumeLayout(false);
            this.playButtonToolStrip.ResumeLayout(false);
            this.playButtonToolStrip.PerformLayout();
            this.recordButtonToolStrip.ResumeLayout(false);
            this.recordButtonToolStrip.PerformLayout();
            this.settingsButtonToolStrip.ResumeLayout(false);
            this.settingsButtonToolStrip.PerformLayout();
            this.mainViewFooter.ResumeLayout(false);
            this.mainViewFooter.PerformLayout();
            this.connectinfoDisplayButtonToolStrip.ResumeLayout(false);
            this.connectinfoDisplayButtonToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label numberOfNodesLabel;
    private System.Windows.Forms.NumericUpDown nodesCounter;
    private System.Windows.Forms.SplitContainer mainSplitter;
    private System.Windows.Forms.ImageList statusImages;
    private System.Windows.Forms.Label executableLabel;
    private System.Windows.Forms.TextBox executableTextBox;
    private System.Windows.Forms.SplitContainer remoteConnectionsSplitter;
    private System.Windows.Forms.SplitContainer splitTransList;
    private System.Windows.Forms.ListBox lstTransactions;
    private System.Windows.Forms.TextBox transferDirectoryTextBox;
    private System.Windows.Forms.Label transferDirectoryLabel;
    private System.Windows.Forms.TextBox remoteComputerTextBox;
    private System.Windows.Forms.Label remoteComputerLabel;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label passwordLabel;
    private System.Windows.Forms.TextBox userNameTextBox;
    private System.Windows.Forms.Label userNameLabel;
    private System.Windows.Forms.TextBox connectionLogTextBox;
    private System.Windows.Forms.TextBox connectionPortTextBox;
    private System.Windows.Forms.Label connectionPortLabel;
    private System.Windows.Forms.Panel navButtonsPanel;
    private RedrawToolStrip recordButtonToolStrip;
    private System.Windows.Forms.ToolStripButton recordButton;
    private RedrawToolStrip playButtonToolStrip;
    private RedrawToolStripSplitButton playButton;
    private System.Windows.Forms.ToolStrip connectinfoCollapseButtonToolStrip;
    private System.Windows.Forms.ToolStripButton connectinfoCollapseButton;
    private System.Windows.Forms.Panel remoteConnectionsHeaderPanel;
    private System.Windows.Forms.Label sshConnectionLabel1;
    private System.Windows.Forms.TabControl remoteConnectionTabs;
    private System.Windows.Forms.TabPage configurationTab;
    private System.Windows.Forms.TabPage connectionLogTab;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.PictureBox statusPictureBox;
    private RedrawToolStrip connectionButtonToolStrip;
    private RedrawToolStrip cancelButtonToolStrip;
    private System.Windows.Forms.ToolStripButton cancelButton;
    private System.Windows.Forms.ToolStripButton connectionButton;
    private System.Windows.Forms.Panel playButtonPanel;
    private RedrawToolStrip downButtonToolStrip;
    private System.Windows.Forms.ToolStripButton downButton;
    private RedrawToolStrip upButtonToolStrip;
    private System.Windows.Forms.ToolStripButton upButton;
    private RedrawToolStrip removeButtonToolStrip;
    private System.Windows.Forms.ToolStripButton removeButton;
    private RedrawToolStrip addButtonToolStrip;
    private System.Windows.Forms.ToolStripButton addButton;
    private DistributedApplicationDebugger.Views.NodePanelHost nodePanelHost1;
    private System.Windows.Forms.SplitContainer connectionLogSplitter;
    private System.Windows.Forms.Label statusLabel;
    private System.ServiceProcess.ServiceController serviceController1;
    private System.Windows.Forms.TextBox rawDataTextBox;
    private System.Windows.Forms.Label rawDataLabel;
    private System.Windows.Forms.Label hostFileLabel;
    private System.Windows.Forms.TextBox hostFileTextBox;
    private System.Windows.Forms.Label paramsLabel;
    private System.Windows.Forms.TextBox paramsTextBox;
    private System.Windows.Forms.Label sessionNameLabel;
    private System.Windows.Forms.TextBox sessionNameTextBox;
    private System.Windows.Forms.Label replayLabel;
    private RedrawToolStrip cancelReplayButtonToolStrip;
    private System.Windows.Forms.ToolStripButton cancelReplayButton;
    private System.Windows.Forms.Panel cancelReplayPanel;
    private System.Windows.Forms.Label replayModeLabel;
    private System.Windows.Forms.Label replaySessionLabel;
    private System.Windows.Forms.Button readFromFileButton;
    private System.Windows.Forms.SplitContainer executableSplitContainer;
    private System.Windows.Forms.Label executablePrefixLabel;
    private RedrawToolStrip settingsButtonToolStrip;
    private System.Windows.Forms.ToolStripButton settingsButton;
    private System.Windows.Forms.Panel mainViewFooter;
    private RedrawToolStrip connectinfoDisplayButtonToolStrip;
    private System.Windows.Forms.ToolStripButton connectinfoDisplayButton;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label sshConnectionLabel2;
    private System.Windows.Forms.ToolStrip displayGridButtonToolStrip;
    private System.Windows.Forms.ToolStripButton displayGridButton;
    private System.Windows.Forms.Panel displayGridButtonPanel;
  }
}

