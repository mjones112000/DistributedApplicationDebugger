namespace DistributedApplicationDebugger.MVP.Views
{
  partial class RemoteConnectionsPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteConnectionsPanel));
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.splitTransList = new System.Windows.Forms.SplitContainer();
      this.transToolStrip = new System.Windows.Forms.ToolStrip();
      this.AddButton = new System.Windows.Forms.ToolStripButton();
      this.RemoveButton = new System.Windows.Forms.ToolStripButton();
      this.DownButton = new System.Windows.Forms.ToolStripButton();
      this.UpButton = new System.Windows.Forms.ToolStripButton();
      this.lstTransactions = new System.Windows.Forms.ListBox();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.txtTransferDirectory = new System.Windows.Forms.TextBox();
      this.lblTransferDirectory = new System.Windows.Forms.Label();
      this.txtRemoteComputer = new System.Windows.Forms.TextBox();
      this.lblRemoteComputer = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.lblUserName = new System.Windows.Forms.Label();
      this.connectionLogTextBox = new System.Windows.Forms.TextBox();
      this.connectionLogLabel = new System.Windows.Forms.Label();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.splitTransList.Panel1.SuspendLayout();
      this.splitTransList.Panel2.SuspendLayout();
      this.splitTransList.SuspendLayout();
      this.transToolStrip.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitMain
      // 
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(0, 0);
      this.splitMain.Name = "splitMain";
      // 
      // splitMain.Panel1
      // 
      this.splitMain.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.splitMain.Panel1.Controls.Add(this.splitTransList);
      // 
      // splitMain.Panel2
      // 
      this.splitMain.Panel2.Controls.Add(this.splitContainer3);
      this.splitMain.Size = new System.Drawing.Size(799, 223);
      this.splitMain.SplitterDistance = 211;
      this.splitMain.TabIndex = 2;
      // 
      // splitTransList
      // 
      this.splitTransList.BackColor = System.Drawing.Color.Transparent;
      this.splitTransList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitTransList.IsSplitterFixed = true;
      this.splitTransList.Location = new System.Drawing.Point(0, 0);
      this.splitTransList.Name = "splitTransList";
      this.splitTransList.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitTransList.Panel1
      // 
      this.splitTransList.Panel1.BackColor = System.Drawing.Color.Transparent;
      this.splitTransList.Panel1.Controls.Add(this.transToolStrip);
      // 
      // splitTransList.Panel2
      // 
      this.splitTransList.Panel2.Controls.Add(this.lstTransactions);
      this.splitTransList.Size = new System.Drawing.Size(211, 223);
      this.splitTransList.SplitterDistance = 26;
      this.splitTransList.TabIndex = 0;
      // 
      // transToolStrip
      // 
      this.transToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
      this.transToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.transToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.RemoveButton,
            this.DownButton,
            this.UpButton});
      this.transToolStrip.Location = new System.Drawing.Point(0, 0);
      this.transToolStrip.Name = "transToolStrip";
      this.transToolStrip.Size = new System.Drawing.Size(211, 26);
      this.transToolStrip.TabIndex = 0;
      // 
      // AddButton
      // 
      this.AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
      this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.AddButton.Margin = new System.Windows.Forms.Padding(0);
      this.AddButton.Name = "AddButton";
      this.AddButton.Size = new System.Drawing.Size(23, 26);
      this.AddButton.Text = "toolStripButton4";
      // 
      // RemoveButton
      // 
      this.RemoveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.RemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveButton.Image")));
      this.RemoveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.RemoveButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
      this.RemoveButton.Name = "RemoveButton";
      this.RemoveButton.Size = new System.Drawing.Size(23, 26);
      // 
      // DownButton
      // 
      this.DownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.DownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DownButton.Image = ((System.Drawing.Image)(resources.GetObject("DownButton.Image")));
      this.DownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DownButton.Margin = new System.Windows.Forms.Padding(0);
      this.DownButton.Name = "DownButton";
      this.DownButton.Size = new System.Drawing.Size(23, 26);
      // 
      // UpButton
      // 
      this.UpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.UpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.UpButton.Image = ((System.Drawing.Image)(resources.GetObject("UpButton.Image")));
      this.UpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.UpButton.Margin = new System.Windows.Forms.Padding(0);
      this.UpButton.Name = "UpButton";
      this.UpButton.Size = new System.Drawing.Size(23, 26);
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
      this.lstTransactions.Size = new System.Drawing.Size(211, 193);
      this.lstTransactions.TabIndex = 0;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.txtTransferDirectory);
      this.splitContainer3.Panel1.Controls.Add(this.lblTransferDirectory);
      this.splitContainer3.Panel1.Controls.Add(this.txtRemoteComputer);
      this.splitContainer3.Panel1.Controls.Add(this.lblRemoteComputer);
      this.splitContainer3.Panel1.Controls.Add(this.txtPassword);
      this.splitContainer3.Panel1.Controls.Add(this.lblPassword);
      this.splitContainer3.Panel1.Controls.Add(this.txtUserName);
      this.splitContainer3.Panel1.Controls.Add(this.lblUserName);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.connectionLogTextBox);
      this.splitContainer3.Panel2.Controls.Add(this.connectionLogLabel);
      this.splitContainer3.Size = new System.Drawing.Size(584, 223);
      this.splitContainer3.SplitterDistance = 289;
      this.splitContainer3.TabIndex = 0;
      // 
      // txtTransferDirectory
      // 
      this.txtTransferDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTransferDirectory.Location = new System.Drawing.Point(137, 159);
      this.txtTransferDirectory.Name = "txtTransferDirectory";
      this.txtTransferDirectory.Size = new System.Drawing.Size(136, 20);
      this.txtTransferDirectory.TabIndex = 24;
      // 
      // lblTransferDirectory
      // 
      this.lblTransferDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTransferDirectory.Location = new System.Drawing.Point(10, 159);
      this.lblTransferDirectory.Name = "lblTransferDirectory";
      this.lblTransferDirectory.Size = new System.Drawing.Size(121, 16);
      this.lblTransferDirectory.TabIndex = 23;
      this.lblTransferDirectory.Text = "Transfer Directory:";
      this.lblTransferDirectory.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // txtRemoteComputer
      // 
      this.txtRemoteComputer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRemoteComputer.Location = new System.Drawing.Point(137, 65);
      this.txtRemoteComputer.Name = "txtRemoteComputer";
      this.txtRemoteComputer.Size = new System.Drawing.Size(136, 20);
      this.txtRemoteComputer.TabIndex = 17;
      // 
      // lblRemoteComputer
      // 
      this.lblRemoteComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRemoteComputer.Location = new System.Drawing.Point(10, 65);
      this.lblRemoteComputer.Name = "lblRemoteComputer";
      this.lblRemoteComputer.Size = new System.Drawing.Size(121, 19);
      this.lblRemoteComputer.TabIndex = 22;
      this.lblRemoteComputer.Text = "Remote Computer:";
      this.lblRemoteComputer.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // txtPassword
      // 
      this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPassword.Location = new System.Drawing.Point(137, 126);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '•';
      this.txtPassword.Size = new System.Drawing.Size(136, 20);
      this.txtPassword.TabIndex = 21;
      // 
      // lblPassword
      // 
      this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPassword.Location = new System.Drawing.Point(10, 125);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(121, 20);
      this.lblPassword.TabIndex = 20;
      this.lblPassword.Text = "Password:";
      this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // txtUserName
      // 
      this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUserName.Location = new System.Drawing.Point(137, 95);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(136, 20);
      this.txtUserName.TabIndex = 19;
      // 
      // lblUserName
      // 
      this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblUserName.Location = new System.Drawing.Point(10, 96);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(121, 19);
      this.lblUserName.TabIndex = 18;
      this.lblUserName.Text = "User Name:";
      this.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // connectionLogTextBox
      // 
      this.connectionLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.connectionLogTextBox.Location = new System.Drawing.Point(0, 23);
      this.connectionLogTextBox.Multiline = true;
      this.connectionLogTextBox.Name = "connectionLogTextBox";
      this.connectionLogTextBox.Size = new System.Drawing.Size(291, 200);
      this.connectionLogTextBox.TabIndex = 1;
      // 
      // connectionLogLabel
      // 
      this.connectionLogLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.connectionLogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connectionLogLabel.Location = new System.Drawing.Point(0, 0);
      this.connectionLogLabel.Name = "connectionLogLabel";
      this.connectionLogLabel.Size = new System.Drawing.Size(291, 23);
      this.connectionLogLabel.TabIndex = 0;
      this.connectionLogLabel.Text = "Connection Log";
      this.connectionLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // RemoteConnectionsPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitMain);
      this.Name = "RemoteConnectionsPanel";
      this.Size = new System.Drawing.Size(799, 223);
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      this.splitMain.ResumeLayout(false);
      this.splitTransList.Panel1.ResumeLayout(false);
      this.splitTransList.Panel1.PerformLayout();
      this.splitTransList.Panel2.ResumeLayout(false);
      this.splitTransList.ResumeLayout(false);
      this.transToolStrip.ResumeLayout(false);
      this.transToolStrip.PerformLayout();
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel1.PerformLayout();
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.SplitContainer splitTransList;
    private System.Windows.Forms.ToolStrip transToolStrip;
    private System.Windows.Forms.ToolStripButton AddButton;
    private System.Windows.Forms.ToolStripButton RemoveButton;
    private System.Windows.Forms.ToolStripButton DownButton;
    private System.Windows.Forms.ToolStripButton UpButton;
    private System.Windows.Forms.ListBox lstTransactions;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.TextBox txtTransferDirectory;
    private System.Windows.Forms.Label lblTransferDirectory;
    private System.Windows.Forms.TextBox txtRemoteComputer;
    private System.Windows.Forms.Label lblRemoteComputer;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.TextBox connectionLogTextBox;
    private System.Windows.Forms.Label connectionLogLabel;

  }
}
