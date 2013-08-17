namespace DistributedApplicationDebugger.Views
{
  partial class ConsolePanel
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
      this.consoleText = new System.Windows.Forms.TextBox();
      this.consoleToolStrip = new System.Windows.Forms.ToolStrip();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.splitConsoleButton = new System.Windows.Forms.ToolStripButton();
      this.gdbOutMessagesButton = new System.Windows.Forms.ToolStripButton();
      this.consoleOutButton = new System.Windows.Forms.ToolStripButton();
      this.gdbText = new System.Windows.Forms.TextBox();
      this.consoleSplitter = new System.Windows.Forms.SplitContainer();
      this.consoleToolStrip.SuspendLayout();
      this.consoleSplitter.Panel1.SuspendLayout();
      this.consoleSplitter.Panel2.SuspendLayout();
      this.consoleSplitter.SuspendLayout();
      this.SuspendLayout();
      // 
      // consoleText
      // 
      this.consoleText.BackColor = System.Drawing.Color.SteelBlue;
      this.consoleText.Cursor = System.Windows.Forms.Cursors.Default;
      this.consoleText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.consoleText.ForeColor = System.Drawing.SystemColors.Window;
      this.consoleText.Location = new System.Drawing.Point(0, 0);
      this.consoleText.Multiline = true;
      this.consoleText.Name = "consoleText";
      this.consoleText.ReadOnly = true;
      this.consoleText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.consoleText.Size = new System.Drawing.Size(300, 159);
      this.consoleText.TabIndex = 7;
      // 
      // consoleToolStrip
      // 
      this.consoleToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.consoleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.splitConsoleButton,
            this.gdbOutMessagesButton,
            this.consoleOutButton});
      this.consoleToolStrip.Location = new System.Drawing.Point(0, 0);
      this.consoleToolStrip.Name = "consoleToolStrip";
      this.consoleToolStrip.Size = new System.Drawing.Size(300, 25);
      this.consoleToolStrip.TabIndex = 8;
      this.consoleToolStrip.Text = "toolStrip1";
      // 
      // refreshButton
      // 
      this.refreshButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.refreshButton.CheckOnClick = true;
      this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.refreshButton.Image = global::DistributedApplicationDebugger.Properties.Resources.refresh;
      this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(23, 22);
      this.refreshButton.Text = "Refresh";
      // 
      // splitConsoleButton
      // 
      this.splitConsoleButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.splitConsoleButton.CheckOnClick = true;
      this.splitConsoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.splitConsoleButton.Enabled = false;
      this.splitConsoleButton.Image = global::DistributedApplicationDebugger.Properties.Resources.SplitConsole;
      this.splitConsoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.splitConsoleButton.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
      this.splitConsoleButton.Name = "splitConsoleButton";
      this.splitConsoleButton.Size = new System.Drawing.Size(23, 22);
      this.splitConsoleButton.Text = "View GDB Out";
      this.splitConsoleButton.Click += new System.EventHandler(this.splitConsoleButton_Click);
      // 
      // gdbOutMessagesButton
      // 
      this.gdbOutMessagesButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.gdbOutMessagesButton.CheckOnClick = true;
      this.gdbOutMessagesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.gdbOutMessagesButton.Enabled = false;
      this.gdbOutMessagesButton.Image = global::DistributedApplicationDebugger.Properties.Resources.DebuggerEnabled;
      this.gdbOutMessagesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.gdbOutMessagesButton.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
      this.gdbOutMessagesButton.Name = "gdbOutMessagesButton";
      this.gdbOutMessagesButton.Size = new System.Drawing.Size(23, 22);
      this.gdbOutMessagesButton.Text = "View GDB Out";
      this.gdbOutMessagesButton.Click += new System.EventHandler(this.gdbOutMessagesButton_Click);
      // 
      // consoleOutButton
      // 
      this.consoleOutButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.consoleOutButton.Checked = true;
      this.consoleOutButton.CheckOnClick = true;
      this.consoleOutButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.consoleOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.consoleOutButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Console;
      this.consoleOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.consoleOutButton.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
      this.consoleOutButton.Name = "consoleOutButton";
      this.consoleOutButton.Size = new System.Drawing.Size(23, 22);
      this.consoleOutButton.Text = "View Stdout";
      this.consoleOutButton.Click += new System.EventHandler(this.consoleOutButton_Click);
      // 
      // gdbText
      // 
      this.gdbText.BackColor = System.Drawing.Color.Goldenrod;
      this.gdbText.Cursor = System.Windows.Forms.Cursors.Default;
      this.gdbText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gdbText.ForeColor = System.Drawing.Color.Black;
      this.gdbText.Location = new System.Drawing.Point(0, 0);
      this.gdbText.Multiline = true;
      this.gdbText.Name = "gdbText";
      this.gdbText.ReadOnly = true;
      this.gdbText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.gdbText.Size = new System.Drawing.Size(300, 160);
      this.gdbText.TabIndex = 9;
      // 
      // consoleSplitter
      // 
      this.consoleSplitter.BackColor = System.Drawing.SystemColors.Control;
      this.consoleSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.consoleSplitter.Location = new System.Drawing.Point(0, 25);
      this.consoleSplitter.Name = "consoleSplitter";
      this.consoleSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // consoleSplitter.Panel1
      // 
      this.consoleSplitter.Panel1.Controls.Add(this.consoleText);
      this.consoleSplitter.Panel1MinSize = 0;
      // 
      // consoleSplitter.Panel2
      // 
      this.consoleSplitter.Panel2.Controls.Add(this.gdbText);
      this.consoleSplitter.Panel2MinSize = 0;
      this.consoleSplitter.Size = new System.Drawing.Size(300, 323);
      this.consoleSplitter.SplitterDistance = 159;
      this.consoleSplitter.TabIndex = 10;
      // 
      // ConsolePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.consoleSplitter);
      this.Controls.Add(this.consoleToolStrip);
      this.Name = "ConsolePanel";
      this.Size = new System.Drawing.Size(300, 348);
      this.consoleToolStrip.ResumeLayout(false);
      this.consoleToolStrip.PerformLayout();
      this.consoleSplitter.Panel1.ResumeLayout(false);
      this.consoleSplitter.Panel1.PerformLayout();
      this.consoleSplitter.Panel2.ResumeLayout(false);
      this.consoleSplitter.Panel2.PerformLayout();
      this.consoleSplitter.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox consoleText;
    private System.Windows.Forms.ToolStrip consoleToolStrip;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.ToolStripButton gdbOutMessagesButton;
    private System.Windows.Forms.ToolStripButton consoleOutButton;
    private System.Windows.Forms.TextBox gdbText;
    private System.Windows.Forms.ToolStripButton splitConsoleButton;
    private System.Windows.Forms.SplitContainer consoleSplitter;


  }
}
