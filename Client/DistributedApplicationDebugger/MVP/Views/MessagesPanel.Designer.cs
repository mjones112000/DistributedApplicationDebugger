namespace DistributedApplicationDebugger.Views
{
  partial class MessagesPanel
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagesPanel));
      this.messagesGrid = new System.Windows.Forms.DataGridView();
      this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.srcColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.destColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tagColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.objColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.isMatchedCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.messageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.getBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.messagesToolStrip = new System.Windows.Forms.ToolStrip();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.hideMatchedButton = new System.Windows.Forms.ToolStripButton();
      this.messageCountLabel = new System.Windows.Forms.ToolStripLabel();
      this.messageCountValueLabel = new System.Windows.Forms.ToolStripLabel();
      this.matchModeButton = new System.Windows.Forms.ToolStripButton();
      this.filterDropDown = new System.Windows.Forms.ToolStripDropDownButton();
      this.checkAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uncheckMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.isendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.recvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.irecvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.messageDetailsSplitter = new System.Windows.Forms.SplitContainer();
      this.commandDetailsTextBox = new System.Windows.Forms.TextBox();
      this.commandDetailsToolStrip = new System.Windows.Forms.ToolStrip();
      this.hideButton = new System.Windows.Forms.ToolStripButton();
      this.commandDetailsLabel = new System.Windows.Forms.ToolStripLabel();
      this.messageDetailsFooter = new System.Windows.Forms.ToolStrip();
      this.detailsButton = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.messagesGrid)).BeginInit();
      this.messageContextMenu.SuspendLayout();
      this.messagesToolStrip.SuspendLayout();
      this.messageDetailsSplitter.Panel1.SuspendLayout();
      this.messageDetailsSplitter.Panel2.SuspendLayout();
      this.messageDetailsSplitter.SuspendLayout();
      this.commandDetailsToolStrip.SuspendLayout();
      this.messageDetailsFooter.SuspendLayout();
      this.SuspendLayout();
      // 
      // messagesGrid
      // 
      this.messagesGrid.AllowUserToAddRows = false;
      this.messagesGrid.AllowUserToDeleteRows = false;
      this.messagesGrid.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.messagesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.messagesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.messagesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.messagesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.messagesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.messagesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.sizeColumn,
            this.typeColumn,
            this.srcColumn,
            this.destColumn,
            this.tagColumn,
            this.objColumn,
            this.isMatchedCol});
      this.messagesGrid.ContextMenuStrip = this.messageContextMenu;
      this.messagesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messagesGrid.Location = new System.Drawing.Point(0, 0);
      this.messagesGrid.Name = "messagesGrid";
      this.messagesGrid.RowHeadersVisible = false;
      this.messagesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.messagesGrid.Size = new System.Drawing.Size(300, 69);
      this.messagesGrid.TabIndex = 0;
      this.messagesGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.messagesGrid_CellFormatting);
      this.messagesGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.messagesGrid_CellMouseClick);
      this.messagesGrid.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.messagesGrid_CellToolTipTextNeeded);
      this.messagesGrid.SelectionChanged += new System.EventHandler(this.messagesGrid_SelectionChanged);
      this.messagesGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.messagesGrid_MouseDown);
      // 
      // idColumn
      // 
      this.idColumn.DataPropertyName = "idColumn";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.idColumn.DefaultCellStyle = dataGridViewCellStyle3;
      this.idColumn.FillWeight = 117.7087F;
      this.idColumn.HeaderText = "#";
      this.idColumn.MinimumWidth = 30;
      this.idColumn.Name = "idColumn";
      this.idColumn.ReadOnly = true;
      // 
      // sizeColumn
      // 
      this.sizeColumn.DataPropertyName = "sizeColumn";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.sizeColumn.DefaultCellStyle = dataGridViewCellStyle4;
      this.sizeColumn.FillWeight = 65.31264F;
      this.sizeColumn.HeaderText = "Size";
      this.sizeColumn.MinimumWidth = 25;
      this.sizeColumn.Name = "sizeColumn";
      this.sizeColumn.ReadOnly = true;
      // 
      // typeColumn
      // 
      this.typeColumn.DataPropertyName = "typeColumn";
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.typeColumn.DefaultCellStyle = dataGridViewCellStyle5;
      this.typeColumn.FillWeight = 200.7561F;
      this.typeColumn.HeaderText = "Type";
      this.typeColumn.MinimumWidth = 30;
      this.typeColumn.Name = "typeColumn";
      this.typeColumn.ReadOnly = true;
      // 
      // srcColumn
      // 
      this.srcColumn.DataPropertyName = "srcColumn";
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.srcColumn.DefaultCellStyle = dataGridViewCellStyle6;
      this.srcColumn.FillWeight = 74.76784F;
      this.srcColumn.HeaderText = "Src";
      this.srcColumn.MinimumWidth = 25;
      this.srcColumn.Name = "srcColumn";
      this.srcColumn.ReadOnly = true;
      // 
      // destColumn
      // 
      this.destColumn.DataPropertyName = "destColumn";
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.destColumn.DefaultCellStyle = dataGridViewCellStyle7;
      this.destColumn.FillWeight = 76.14214F;
      this.destColumn.HeaderText = "Dest";
      this.destColumn.MinimumWidth = 25;
      this.destColumn.Name = "destColumn";
      this.destColumn.ReadOnly = true;
      // 
      // tagColumn
      // 
      this.tagColumn.DataPropertyName = "tagColumn";
      dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.tagColumn.DefaultCellStyle = dataGridViewCellStyle8;
      this.tagColumn.FillWeight = 65.31264F;
      this.tagColumn.HeaderText = "Tag";
      this.tagColumn.MinimumWidth = 25;
      this.tagColumn.Name = "tagColumn";
      this.tagColumn.ReadOnly = true;
      // 
      // objColumn
      // 
      this.objColumn.DataPropertyName = "objectColumn";
      this.objColumn.HeaderText = "Object";
      this.objColumn.Name = "objColumn";
      this.objColumn.Visible = false;
      // 
      // isMatchedCol
      // 
      this.isMatchedCol.DataPropertyName = "isMatchedCol";
      this.isMatchedCol.HeaderText = "Matched";
      this.isMatchedCol.Name = "isMatchedCol";
      this.isMatchedCol.Visible = false;
      // 
      // messageContextMenu
      // 
      this.messageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getBufferToolStripMenuItem,
            this.commandToolStripMenuItem});
      this.messageContextMenu.Name = "messageContextMenu";
      this.messageContextMenu.Size = new System.Drawing.Size(132, 48);
      this.messageContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.messageContextMenu_Opening);
      // 
      // getBufferToolStripMenuItem
      // 
      this.getBufferToolStripMenuItem.Name = "getBufferToolStripMenuItem";
      this.getBufferToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.getBufferToolStripMenuItem.Text = "Get Buffer";
      this.getBufferToolStripMenuItem.Click += new System.EventHandler(this.getBufferToolStripMenuItem_Click);
      // 
      // commandToolStripMenuItem
      // 
      this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
      this.commandToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.commandToolStripMenuItem.Text = "Command";
      this.commandToolStripMenuItem.Click += new System.EventHandler(this.commandDetailsToolStripMenuItem_Click);
      // 
      // messagesToolStrip
      // 
      this.messagesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.messagesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.hideMatchedButton,
            this.messageCountLabel,
            this.messageCountValueLabel,
            this.matchModeButton,
            this.filterDropDown});
      this.messagesToolStrip.Location = new System.Drawing.Point(0, 0);
      this.messagesToolStrip.Name = "messagesToolStrip";
      this.messagesToolStrip.Size = new System.Drawing.Size(300, 25);
      this.messagesToolStrip.TabIndex = 1;
      this.messagesToolStrip.Text = "toolStrip1";
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
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // hideMatchedButton
      // 
      this.hideMatchedButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.hideMatchedButton.CheckOnClick = true;
      this.hideMatchedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.hideMatchedButton.Image = ((System.Drawing.Image)(resources.GetObject("hideMatchedButton.Image")));
      this.hideMatchedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.hideMatchedButton.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
      this.hideMatchedButton.Name = "hideMatchedButton";
      this.hideMatchedButton.Size = new System.Drawing.Size(23, 22);
      this.hideMatchedButton.Text = "Hide Matched Messages";
      this.hideMatchedButton.CheckedChanged += new System.EventHandler(this.hideMatchedButton_CheckedChanged);
      // 
      // messageCountLabel
      // 
      this.messageCountLabel.Name = "messageCountLabel";
      this.messageCountLabel.Size = new System.Drawing.Size(43, 22);
      this.messageCountLabel.Text = "Count:";
      // 
      // messageCountValueLabel
      // 
      this.messageCountValueLabel.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
      this.messageCountValueLabel.Name = "messageCountValueLabel";
      this.messageCountValueLabel.Size = new System.Drawing.Size(0, 22);
      this.messageCountValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // matchModeButton
      // 
      this.matchModeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.matchModeButton.CheckOnClick = true;
      this.matchModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.matchModeButton.Image = global::DistributedApplicationDebugger.Properties.Resources.handshake;
      this.matchModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.matchModeButton.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
      this.matchModeButton.Name = "matchModeButton";
      this.matchModeButton.Size = new System.Drawing.Size(23, 22);
      this.matchModeButton.Text = "Track Matched Messages";
      this.matchModeButton.Click += new System.EventHandler(this.matchModeButton_Click);
      // 
      // filterDropDown
      // 
      this.filterDropDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.filterDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.filterDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllMenuItem,
            this.uncheckMenuItem,
            this.sendMenuItem,
            this.isendMenuItem,
            this.recvMenuItem,
            this.irecvMenuItem});
      this.filterDropDown.Image = ((System.Drawing.Image)(resources.GetObject("filterDropDown.Image")));
      this.filterDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.filterDropDown.Name = "filterDropDown";
      this.filterDropDown.Size = new System.Drawing.Size(82, 22);
      this.filterDropDown.Text = "Commands";
      // 
      // checkAllMenuItem
      // 
      this.checkAllMenuItem.Name = "checkAllMenuItem";
      this.checkAllMenuItem.Size = new System.Drawing.Size(137, 22);
      this.checkAllMenuItem.Text = "Check All";
      this.checkAllMenuItem.Click += new System.EventHandler(this.checkAllMenuItem_Click);
      // 
      // uncheckMenuItem
      // 
      this.uncheckMenuItem.Name = "uncheckMenuItem";
      this.uncheckMenuItem.Size = new System.Drawing.Size(137, 22);
      this.uncheckMenuItem.Text = "Uncheck All";
      this.uncheckMenuItem.Click += new System.EventHandler(this.uncheckMenuItem_Click);
      // 
      // sendMenuItem
      // 
      this.sendMenuItem.Checked = true;
      this.sendMenuItem.CheckOnClick = true;
      this.sendMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.sendMenuItem.Name = "sendMenuItem";
      this.sendMenuItem.Size = new System.Drawing.Size(137, 22);
      this.sendMenuItem.Text = "MPI_SEND";
      this.sendMenuItem.Click += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // isendMenuItem
      // 
      this.isendMenuItem.Checked = true;
      this.isendMenuItem.CheckOnClick = true;
      this.isendMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.isendMenuItem.Name = "isendMenuItem";
      this.isendMenuItem.Size = new System.Drawing.Size(137, 22);
      this.isendMenuItem.Text = "MPI_ISEND";
      this.isendMenuItem.Click += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // recvMenuItem
      // 
      this.recvMenuItem.Checked = true;
      this.recvMenuItem.CheckOnClick = true;
      this.recvMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.recvMenuItem.Name = "recvMenuItem";
      this.recvMenuItem.Size = new System.Drawing.Size(137, 22);
      this.recvMenuItem.Text = "MPI_RECV";
      this.recvMenuItem.Click += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // irecvMenuItem
      // 
      this.irecvMenuItem.Checked = true;
      this.irecvMenuItem.CheckOnClick = true;
      this.irecvMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.irecvMenuItem.Name = "irecvMenuItem";
      this.irecvMenuItem.Size = new System.Drawing.Size(137, 22);
      this.irecvMenuItem.Text = "MPI_IRECV";
      this.irecvMenuItem.Click += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // messageDetailsSplitter
      // 
      this.messageDetailsSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messageDetailsSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.messageDetailsSplitter.Location = new System.Drawing.Point(0, 25);
      this.messageDetailsSplitter.Name = "messageDetailsSplitter";
      this.messageDetailsSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // messageDetailsSplitter.Panel1
      // 
      this.messageDetailsSplitter.Panel1.Controls.Add(this.messagesGrid);
      // 
      // messageDetailsSplitter.Panel2
      // 
      this.messageDetailsSplitter.Panel2.Controls.Add(this.commandDetailsTextBox);
      this.messageDetailsSplitter.Panel2.Controls.Add(this.commandDetailsToolStrip);
      this.messageDetailsSplitter.Size = new System.Drawing.Size(300, 300);
      this.messageDetailsSplitter.SplitterDistance = 69;
      this.messageDetailsSplitter.TabIndex = 2;
      // 
      // commandDetailsTextBox
      // 
      this.commandDetailsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.commandDetailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.commandDetailsTextBox.Location = new System.Drawing.Point(0, 25);
      this.commandDetailsTextBox.Multiline = true;
      this.commandDetailsTextBox.Name = "commandDetailsTextBox";
      this.commandDetailsTextBox.ReadOnly = true;
      this.commandDetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.commandDetailsTextBox.Size = new System.Drawing.Size(300, 202);
      this.commandDetailsTextBox.TabIndex = 5;
      // 
      // commandDetailsToolStrip
      // 
      this.commandDetailsToolStrip.AutoSize = false;
      this.commandDetailsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.commandDetailsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideButton,
            this.commandDetailsLabel});
      this.commandDetailsToolStrip.Location = new System.Drawing.Point(0, 0);
      this.commandDetailsToolStrip.Name = "commandDetailsToolStrip";
      this.commandDetailsToolStrip.Size = new System.Drawing.Size(300, 25);
      this.commandDetailsToolStrip.TabIndex = 4;
      // 
      // hideButton
      // 
      this.hideButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.hideButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.hideButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Minimize;
      this.hideButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.hideButton.Name = "hideButton";
      this.hideButton.Size = new System.Drawing.Size(23, 22);
      this.hideButton.ToolTipText = "Hide Details";
      this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
      // 
      // commandDetailsLabel
      // 
      this.commandDetailsLabel.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
      this.commandDetailsLabel.Name = "commandDetailsLabel";
      this.commandDetailsLabel.Size = new System.Drawing.Size(102, 24);
      this.commandDetailsLabel.Text = "Command Details";
      // 
      // messageDetailsFooter
      // 
      this.messageDetailsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.messageDetailsFooter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.messageDetailsFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsButton});
      this.messageDetailsFooter.Location = new System.Drawing.Point(0, 325);
      this.messageDetailsFooter.Name = "messageDetailsFooter";
      this.messageDetailsFooter.Size = new System.Drawing.Size(300, 25);
      this.messageDetailsFooter.TabIndex = 6;
      // 
      // detailsButton
      // 
      this.detailsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.detailsButton.Image = global::DistributedApplicationDebugger.Properties.Resources.Clipboard;
      this.detailsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.detailsButton.Name = "detailsButton";
      this.detailsButton.Size = new System.Drawing.Size(122, 22);
      this.detailsButton.Text = "Command Details";
      this.detailsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.detailsButton.Click += new System.EventHandler(this.detailsButton_Click);
      // 
      // MessagesPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.messageDetailsSplitter);
      this.Controls.Add(this.messagesToolStrip);
      this.Controls.Add(this.messageDetailsFooter);
      this.Name = "MessagesPanel";
      this.Size = new System.Drawing.Size(300, 350);
      ((System.ComponentModel.ISupportInitialize)(this.messagesGrid)).EndInit();
      this.messageContextMenu.ResumeLayout(false);
      this.messagesToolStrip.ResumeLayout(false);
      this.messagesToolStrip.PerformLayout();
      this.messageDetailsSplitter.Panel1.ResumeLayout(false);
      this.messageDetailsSplitter.Panel2.ResumeLayout(false);
      this.messageDetailsSplitter.Panel2.PerformLayout();
      this.messageDetailsSplitter.ResumeLayout(false);
      this.commandDetailsToolStrip.ResumeLayout(false);
      this.commandDetailsToolStrip.PerformLayout();
      this.messageDetailsFooter.ResumeLayout(false);
      this.messageDetailsFooter.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView messagesGrid;
    private System.Windows.Forms.ToolStrip messagesToolStrip;
    private System.Windows.Forms.ToolStripButton hideMatchedButton;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.ToolStripLabel messageCountValueLabel;
    private System.Windows.Forms.ToolStripLabel messageCountLabel;
    private System.Windows.Forms.ToolStripButton matchModeButton;
    private System.Windows.Forms.ContextMenuStrip messageContextMenu;
    private System.Windows.Forms.ToolStripMenuItem getBufferToolStripMenuItem;
    private System.Windows.Forms.SplitContainer messageDetailsSplitter;
    private System.Windows.Forms.TextBox commandDetailsTextBox;
    private System.Windows.Forms.ToolStrip commandDetailsToolStrip;
    private System.Windows.Forms.ToolStripButton hideButton;
    private System.Windows.Forms.ToolStripLabel commandDetailsLabel;
    private System.Windows.Forms.ToolStrip messageDetailsFooter;
    private System.Windows.Forms.ToolStripButton detailsButton;
    private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn sizeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn srcColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn destColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tagColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn objColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn isMatchedCol;
    private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton filterDropDown;
    private System.Windows.Forms.ToolStripMenuItem recvMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sendMenuItem;
    private System.Windows.Forms.ToolStripMenuItem isendMenuItem;
    private System.Windows.Forms.ToolStripMenuItem irecvMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkAllMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uncheckMenuItem;
  }
}
