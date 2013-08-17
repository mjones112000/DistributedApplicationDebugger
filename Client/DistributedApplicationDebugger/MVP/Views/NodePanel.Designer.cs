namespace DistributedApplicationDebugger.Views
{
  partial class NodePanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodePanel));
      this.nodeImages = new System.Windows.Forms.ImageList(this.components);
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.debugSplitter = new System.Windows.Forms.SplitContainer();
      this.nodePanelSplitter = new System.Windows.Forms.SplitContainer();
      this.rdoNormal = new System.Windows.Forms.RadioButton();
      this.modeLabel = new System.Windows.Forms.Label();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.collapsePanelButton = new System.Windows.Forms.ToolStripButton();
      this.rdoPlayback = new System.Windows.Forms.RadioButton();
      this.nodeTabs = new System.Windows.Forms.TabControl();
      this.consoleTab = new System.Windows.Forms.TabPage();
      this.consolePanel = new DistributedApplicationDebugger.Views.ConsolePanel();
      this.messagesTab = new System.Windows.Forms.TabPage();
      this.messagesPanel = new DistributedApplicationDebugger.Views.MessagesPanel();
      this.mpiTab = new System.Windows.Forms.TabPage();
      this.mpiPanel = new DistributedApplicationDebugger.Views.MPIPanel();
      this.commandLineLabel = new System.Windows.Forms.Label();
      this.commandLineTextBox = new System.Windows.Forms.TextBox();
      this.debugToolStrip = new System.Windows.Forms.ToolStrip();
      this.disableGdbButton = new System.Windows.Forms.ToolStripButton();
      this.stepButton = new System.Windows.Forms.ToolStripButton();
      this.nextButton = new System.Windows.Forms.ToolStripButton();
      this.continueButton = new System.Windows.Forms.ToolStripButton();
      this.debugFooter = new System.Windows.Forms.ToolStrip();
      this.enableGdbButton = new System.Windows.Forms.ToolStripButton();
      this.nodeInfoLabel = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.debugSplitter.Panel1.SuspendLayout();
      this.debugSplitter.Panel2.SuspendLayout();
      this.debugSplitter.SuspendLayout();
      this.nodePanelSplitter.Panel1.SuspendLayout();
      this.nodePanelSplitter.Panel2.SuspendLayout();
      this.nodePanelSplitter.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.nodeTabs.SuspendLayout();
      this.consoleTab.SuspendLayout();
      this.messagesTab.SuspendLayout();
      this.mpiTab.SuspendLayout();
      this.debugToolStrip.SuspendLayout();
      this.debugFooter.SuspendLayout();
      this.SuspendLayout();
      // 
      // nodeImages
      // 
      this.nodeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("nodeImages.ImageStream")));
      this.nodeImages.TransparentColor = System.Drawing.Color.Transparent;
      this.nodeImages.Images.SetKeyName(0, "console.ico");
      this.nodeImages.Images.SetKeyName(1, "mail_exchange.ico");
      this.nodeImages.Images.SetKeyName(2, "clipboard.ico");
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.debugSplitter);
      this.groupBox1.Controls.Add(this.debugFooter);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(316, 383);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Node 0";
      // 
      // debugSplitter
      // 
      this.debugSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.debugSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.debugSplitter.Location = new System.Drawing.Point(3, 16);
      this.debugSplitter.Name = "debugSplitter";
      this.debugSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // debugSplitter.Panel1
      // 
      this.debugSplitter.Panel1.Controls.Add(this.nodePanelSplitter);
      // 
      // debugSplitter.Panel2
      // 
      this.debugSplitter.Panel2.Controls.Add(this.commandLineLabel);
      this.debugSplitter.Panel2.Controls.Add(this.commandLineTextBox);
      this.debugSplitter.Panel2.Controls.Add(this.debugToolStrip);
      this.debugSplitter.Size = new System.Drawing.Size(310, 328);
      this.debugSplitter.SplitterDistance = 257;
      this.debugSplitter.TabIndex = 14;
      // 
      // nodePanelSplitter
      // 
      this.nodePanelSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodePanelSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.nodePanelSplitter.IsSplitterFixed = true;
      this.nodePanelSplitter.Location = new System.Drawing.Point(0, 0);
      this.nodePanelSplitter.Name = "nodePanelSplitter";
      this.nodePanelSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // nodePanelSplitter.Panel1
      // 
      this.nodePanelSplitter.Panel1.Controls.Add(this.rdoNormal);
      this.nodePanelSplitter.Panel1.Controls.Add(this.modeLabel);
      this.nodePanelSplitter.Panel1.Controls.Add(this.toolStrip);
      this.nodePanelSplitter.Panel1.Controls.Add(this.rdoPlayback);
      this.nodePanelSplitter.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Control_DragDrop);
      this.nodePanelSplitter.Panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.Control_DragOver);
      this.nodePanelSplitter.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
      // 
      // nodePanelSplitter.Panel2
      // 
      this.nodePanelSplitter.Panel2.Controls.Add(this.nodeTabs);
      this.nodePanelSplitter.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Control_DragDrop);
      this.nodePanelSplitter.Panel2.DragOver += new System.Windows.Forms.DragEventHandler(this.Control_DragOver);
      this.nodePanelSplitter.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
      this.nodePanelSplitter.Size = new System.Drawing.Size(310, 257);
      this.nodePanelSplitter.SplitterDistance = 34;
      this.nodePanelSplitter.TabIndex = 9;
      // 
      // rdoNormal
      // 
      this.rdoNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.rdoNormal.AutoSize = true;
      this.rdoNormal.Checked = true;
      this.rdoNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoNormal.Location = new System.Drawing.Point(117, 12);
      this.rdoNormal.Name = "rdoNormal";
      this.rdoNormal.Size = new System.Drawing.Size(70, 20);
      this.rdoNormal.TabIndex = 10;
      this.rdoNormal.TabStop = true;
      this.rdoNormal.Text = "Normal";
      this.rdoNormal.UseVisualStyleBackColor = true;
      this.rdoNormal.Visible = false;
      // 
      // modeLabel
      // 
      this.modeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.modeLabel.Location = new System.Drawing.Point(68, 14);
      this.modeLabel.Name = "modeLabel";
      this.modeLabel.Size = new System.Drawing.Size(46, 16);
      this.modeLabel.TabIndex = 12;
      this.modeLabel.Text = "Mode:";
      this.modeLabel.Visible = false;
      // 
      // toolStrip
      // 
      this.toolStrip.AutoSize = false;
      this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.toolStrip.CanOverflow = false;
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
      this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
      this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapsePanelButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStrip.Size = new System.Drawing.Size(24, 34);
      this.toolStrip.TabIndex = 2;
      this.toolStrip.Text = "toolStrip1";
      // 
      // collapsePanelButton
      // 
      this.collapsePanelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.collapsePanelButton.Image = ((System.Drawing.Image)(resources.GetObject("collapsePanelButton.Image")));
      this.collapsePanelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.collapsePanelButton.Margin = new System.Windows.Forms.Padding(0);
      this.collapsePanelButton.Name = "collapsePanelButton";
      this.collapsePanelButton.Size = new System.Drawing.Size(22, 20);
      this.collapsePanelButton.Text = "Collapse";
      this.collapsePanelButton.Click += new System.EventHandler(this.collapsePanelButton_Click);
      // 
      // rdoPlayback
      // 
      this.rdoPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.rdoPlayback.AutoSize = true;
      this.rdoPlayback.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoPlayback.Location = new System.Drawing.Point(189, 12);
      this.rdoPlayback.Name = "rdoPlayback";
      this.rdoPlayback.Size = new System.Drawing.Size(83, 20);
      this.rdoPlayback.TabIndex = 11;
      this.rdoPlayback.Text = "Playback";
      this.rdoPlayback.UseVisualStyleBackColor = true;
      this.rdoPlayback.Visible = false;
      // 
      // nodeTabs
      // 
      this.nodeTabs.Controls.Add(this.consoleTab);
      this.nodeTabs.Controls.Add(this.messagesTab);
      this.nodeTabs.Controls.Add(this.mpiTab);
      this.nodeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodeTabs.ImageList = this.nodeImages;
      this.nodeTabs.Location = new System.Drawing.Point(0, 0);
      this.nodeTabs.Name = "nodeTabs";
      this.nodeTabs.SelectedIndex = 0;
      this.nodeTabs.Size = new System.Drawing.Size(310, 219);
      this.nodeTabs.TabIndex = 8;
      this.nodeTabs.SelectedIndexChanged += new System.EventHandler(this.nodeTabs_SelectedIndexChanged);
      // 
      // consoleTab
      // 
      this.consoleTab.Controls.Add(this.consolePanel);
      this.consoleTab.ImageKey = "console.ico";
      this.consoleTab.Location = new System.Drawing.Point(4, 23);
      this.consoleTab.Name = "consoleTab";
      this.consoleTab.Padding = new System.Windows.Forms.Padding(3);
      this.consoleTab.Size = new System.Drawing.Size(302, 192);
      this.consoleTab.TabIndex = 0;
      this.consoleTab.Text = "Console";
      this.consoleTab.UseVisualStyleBackColor = true;
      // 
      // consolePanel
      // 
      this.consolePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.consolePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.consolePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.consolePanel.GdbEnabled = false;
      this.consolePanel.Location = new System.Drawing.Point(3, 3);
      this.consolePanel.Name = "consolePanel";
      this.consolePanel.Size = new System.Drawing.Size(296, 186);
      this.consolePanel.TabIndex = 0;
      // 
      // messagesTab
      // 
      this.messagesTab.Controls.Add(this.messagesPanel);
      this.messagesTab.ImageKey = "mail_exchange.ico";
      this.messagesTab.Location = new System.Drawing.Point(4, 23);
      this.messagesTab.Name = "messagesTab";
      this.messagesTab.Padding = new System.Windows.Forms.Padding(3);
      this.messagesTab.Size = new System.Drawing.Size(302, 192);
      this.messagesTab.TabIndex = 1;
      this.messagesTab.Text = "Messages";
      this.messagesTab.UseVisualStyleBackColor = true;
      // 
      // messagesPanel
      // 
      this.messagesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.messagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messagesPanel.Location = new System.Drawing.Point(3, 3);
      this.messagesPanel.Name = "messagesPanel";
      this.messagesPanel.NodeId = 0;
      this.messagesPanel.SessionMode = DistributedApplicationDebugger.Views.SessionMode.Play;
      this.messagesPanel.Size = new System.Drawing.Size(296, 186);
      this.messagesPanel.TabIndex = 0;
      // 
      // mpiTab
      // 
      this.mpiTab.Controls.Add(this.mpiPanel);
      this.mpiTab.ImageKey = "clipboard.ico";
      this.mpiTab.Location = new System.Drawing.Point(4, 23);
      this.mpiTab.Name = "mpiTab";
      this.mpiTab.Padding = new System.Windows.Forms.Padding(3);
      this.mpiTab.Size = new System.Drawing.Size(302, 192);
      this.mpiTab.TabIndex = 2;
      this.mpiTab.Text = "MPI";
      this.mpiTab.UseVisualStyleBackColor = true;
      // 
      // mpiPanel
      // 
      this.mpiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mpiPanel.Location = new System.Drawing.Point(3, 3);
      this.mpiPanel.Name = "mpiPanel";
      this.mpiPanel.SessionMode = DistributedApplicationDebugger.Views.SessionMode.Offline;
      this.mpiPanel.Size = new System.Drawing.Size(296, 186);
      this.mpiPanel.TabIndex = 0;
      // 
      // commandLineLabel
      // 
      this.commandLineLabel.AutoSize = true;
      this.commandLineLabel.Enabled = false;
      this.commandLineLabel.Location = new System.Drawing.Point(4, 46);
      this.commandLineLabel.Name = "commandLineLabel";
      this.commandLineLabel.Size = new System.Drawing.Size(80, 13);
      this.commandLineLabel.TabIndex = 16;
      this.commandLineLabel.Text = "Command Line:";
      // 
      // commandLineTextBox
      // 
      this.commandLineTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.commandLineTextBox.Enabled = false;
      this.commandLineTextBox.Location = new System.Drawing.Point(85, 43);
      this.commandLineTextBox.Name = "commandLineTextBox";
      this.commandLineTextBox.Size = new System.Drawing.Size(216, 20);
      this.commandLineTextBox.TabIndex = 15;
      this.commandLineTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLineTextBox_KeyDown);
      // 
      // debugToolStrip
      // 
      this.debugToolStrip.AutoSize = false;
      this.debugToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.debugToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableGdbButton,
            this.stepButton,
            this.nextButton,
            this.continueButton});
      this.debugToolStrip.Location = new System.Drawing.Point(0, 0);
      this.debugToolStrip.Name = "debugToolStrip";
      this.debugToolStrip.Size = new System.Drawing.Size(310, 36);
      this.debugToolStrip.TabIndex = 14;
      // 
      // disableGdbButton
      // 
      this.disableGdbButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.disableGdbButton.Image = global::DistributedApplicationDebugger.Properties.Resources.DebuggerEnabled;
      this.disableGdbButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.disableGdbButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.disableGdbButton.Name = "disableGdbButton";
      this.disableGdbButton.Size = new System.Drawing.Size(32, 33);
      this.disableGdbButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.disableGdbButton.Click += new System.EventHandler(this.disableGdbButton_Click);
      // 
      // stepButton
      // 
      this.stepButton.Enabled = false;
      this.stepButton.Image = ((System.Drawing.Image)(resources.GetObject("stepButton.Image")));
      this.stepButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.stepButton.Margin = new System.Windows.Forms.Padding(60, 5, 0, 5);
      this.stepButton.Name = "stepButton";
      this.stepButton.Size = new System.Drawing.Size(49, 26);
      this.stepButton.Text = "step";
      this.stepButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      this.stepButton.ToolTipText = "Step";
      this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Enabled = false;
      this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
      this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.nextButton.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(49, 26);
      this.nextButton.Text = "next";
      this.nextButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      this.nextButton.ToolTipText = "Next";
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // continueButton
      // 
      this.continueButton.Enabled = false;
      this.continueButton.Image = ((System.Drawing.Image)(resources.GetObject("continueButton.Image")));
      this.continueButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.continueButton.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
      this.continueButton.Name = "continueButton";
      this.continueButton.Size = new System.Drawing.Size(74, 26);
      this.continueButton.Text = "continue";
      this.continueButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      this.continueButton.ToolTipText = "Continue";
      this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
      // 
      // debugFooter
      // 
      this.debugFooter.AutoSize = false;
      this.debugFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.debugFooter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.debugFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableGdbButton});
      this.debugFooter.Location = new System.Drawing.Point(3, 344);
      this.debugFooter.Name = "debugFooter";
      this.debugFooter.Size = new System.Drawing.Size(310, 36);
      this.debugFooter.TabIndex = 13;
      // 
      // enableGdbButton
      // 
      this.enableGdbButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.enableGdbButton.Image = global::DistributedApplicationDebugger.Properties.Resources.DebuggerDisabled;
      this.enableGdbButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.enableGdbButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.enableGdbButton.Name = "enableGdbButton";
      this.enableGdbButton.Size = new System.Drawing.Size(32, 33);
      this.enableGdbButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.enableGdbButton.Click += new System.EventHandler(this.enableGdbButton_Click);
      // 
      // nodeInfoLabel
      // 
      this.nodeInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nodeInfoLabel.ForeColor = System.Drawing.Color.Black;
      this.nodeInfoLabel.Location = new System.Drawing.Point(30, 11);
      this.nodeInfoLabel.Name = "nodeInfoLabel";
      this.nodeInfoLabel.Size = new System.Drawing.Size(283, 17);
      this.nodeInfoLabel.TabIndex = 13;
      this.nodeInfoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // NodePanel
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.nodeInfoLabel);
      this.Controls.Add(this.groupBox1);
      this.Name = "NodePanel";
      this.Size = new System.Drawing.Size(316, 383);
      this.groupBox1.ResumeLayout(false);
      this.debugSplitter.Panel1.ResumeLayout(false);
      this.debugSplitter.Panel2.ResumeLayout(false);
      this.debugSplitter.Panel2.PerformLayout();
      this.debugSplitter.ResumeLayout(false);
      this.nodePanelSplitter.Panel1.ResumeLayout(false);
      this.nodePanelSplitter.Panel1.PerformLayout();
      this.nodePanelSplitter.Panel2.ResumeLayout(false);
      this.nodePanelSplitter.ResumeLayout(false);
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.nodeTabs.ResumeLayout(false);
      this.consoleTab.ResumeLayout(false);
      this.messagesTab.ResumeLayout(false);
      this.mpiTab.ResumeLayout(false);
      this.debugToolStrip.ResumeLayout(false);
      this.debugToolStrip.PerformLayout();
      this.debugFooter.ResumeLayout(false);
      this.debugFooter.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ImageList nodeImages;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TabControl nodeTabs;
    private System.Windows.Forms.TabPage consoleTab;
    private ConsolePanel consolePanel;
    private System.Windows.Forms.TabPage messagesTab;
    private MessagesPanel messagesPanel;
    private System.Windows.Forms.TabPage mpiTab;
    private DistributedApplicationDebugger.Views.MPIPanel mpiPanel;
    private System.Windows.Forms.SplitContainer nodePanelSplitter;
    private System.Windows.Forms.RadioButton rdoPlayback;
    private System.Windows.Forms.RadioButton rdoNormal;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton collapsePanelButton;
    private System.Windows.Forms.Label modeLabel;
    private System.Windows.Forms.ToolStrip debugFooter;
    private System.Windows.Forms.ToolStripButton enableGdbButton;
    private System.Windows.Forms.SplitContainer debugSplitter;
    private System.Windows.Forms.Label commandLineLabel;
    private System.Windows.Forms.TextBox commandLineTextBox;
    private System.Windows.Forms.ToolStrip debugToolStrip;
    private System.Windows.Forms.ToolStripButton disableGdbButton;
    private System.Windows.Forms.ToolStripButton stepButton;
    private System.Windows.Forms.ToolStripButton nextButton;
    private System.Windows.Forms.ToolStripButton continueButton;
    private System.Windows.Forms.Label nodeInfoLabel;
  }
}
