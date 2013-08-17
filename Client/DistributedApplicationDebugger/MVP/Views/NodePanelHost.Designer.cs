namespace DistributedApplicationDebugger.Views
{
  partial class NodePanelHost
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
      this.hiddenPanelsStrip = new System.Windows.Forms.ToolStrip();
      this.nodesPanel = new System.Windows.Forms.Panel();
      this.runningTimeToolStrip = new System.Windows.Forms.ToolStrip();
      this.runningTimeLabel = new System.Windows.Forms.ToolStripLabel();
      this.runningTimeValueLabel = new System.Windows.Forms.ToolStripLabel();
      this.bufferDetailsGrid = new System.Windows.Forms.DataGridView();
      this.IndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.HexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.nodesGridSplitter = new System.Windows.Forms.SplitContainer();
      this.bufferDetailsToolStrip2 = new System.Windows.Forms.ToolStrip();
      this.bufferDetailsLabel = new System.Windows.Forms.ToolStripLabel();
      this.bufferDetailsToolStrip1 = new System.Windows.Forms.ToolStrip();
      this.commandIdLabel = new System.Windows.Forms.ToolStripLabel();
      this.commandTypeLabel = new System.Windows.Forms.ToolStripLabel();
      this.dataTypeLabel = new System.Windows.Forms.ToolStripLabel();
      this.nodesPanel.SuspendLayout();
      this.runningTimeToolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bufferDetailsGrid)).BeginInit();
      this.nodesGridSplitter.Panel1.SuspendLayout();
      this.nodesGridSplitter.Panel2.SuspendLayout();
      this.nodesGridSplitter.SuspendLayout();
      this.bufferDetailsToolStrip2.SuspendLayout();
      this.bufferDetailsToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // hiddenPanelsStrip
      // 
      this.hiddenPanelsStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.hiddenPanelsStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.hiddenPanelsStrip.Location = new System.Drawing.Point(0, 350);
      this.hiddenPanelsStrip.Name = "hiddenPanelsStrip";
      this.hiddenPanelsStrip.Size = new System.Drawing.Size(888, 25);
      this.hiddenPanelsStrip.TabIndex = 1;
      this.hiddenPanelsStrip.Text = "toolStrip1";
      // 
      // nodesPanel
      // 
      this.nodesPanel.AutoScroll = true;
      this.nodesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.nodesPanel.BackColor = System.Drawing.SystemColors.Control;
      this.nodesPanel.Controls.Add(this.runningTimeToolStrip);
      this.nodesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodesPanel.Location = new System.Drawing.Point(0, 0);
      this.nodesPanel.Name = "nodesPanel";
      this.nodesPanel.Size = new System.Drawing.Size(617, 350);
      this.nodesPanel.TabIndex = 8;
      // 
      // runningTimeToolStrip
      // 
      this.runningTimeToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.runningTimeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runningTimeLabel,
            this.runningTimeValueLabel});
      this.runningTimeToolStrip.Location = new System.Drawing.Point(0, 0);
      this.runningTimeToolStrip.Name = "runningTimeToolStrip";
      this.runningTimeToolStrip.Size = new System.Drawing.Size(617, 25);
      this.runningTimeToolStrip.TabIndex = 0;
      this.runningTimeToolStrip.Text = "toolStrip1";
      // 
      // runningTimeLabel
      // 
      this.runningTimeLabel.Name = "runningTimeLabel";
      this.runningTimeLabel.Size = new System.Drawing.Size(85, 22);
      this.runningTimeLabel.Text = "Running Time:";
      // 
      // runningTimeValueLabel
      // 
      this.runningTimeValueLabel.AutoSize = false;
      this.runningTimeValueLabel.Name = "runningTimeValueLabel";
      this.runningTimeValueLabel.Size = new System.Drawing.Size(114, 22);
      this.runningTimeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // bufferDetailsGrid
      // 
      this.bufferDetailsGrid.AllowUserToAddRows = false;
      this.bufferDetailsGrid.AllowUserToDeleteRows = false;
      this.bufferDetailsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.bufferDetailsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.bufferDetailsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexColumn,
            this.ValueColumn,
            this.HexColumn});
      this.bufferDetailsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bufferDetailsGrid.Location = new System.Drawing.Point(0, 54);
      this.bufferDetailsGrid.Name = "bufferDetailsGrid";
      this.bufferDetailsGrid.RowHeadersWidth = 20;
      this.bufferDetailsGrid.Size = new System.Drawing.Size(267, 296);
      this.bufferDetailsGrid.TabIndex = 9;
      this.bufferDetailsGrid.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.bufferDetailsGrid_ColumnAdded);
      // 
      // IndexColumn
      // 
      this.IndexColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.IndexColumn.DataPropertyName = "Index";
      this.IndexColumn.HeaderText = "Index";
      this.IndexColumn.Name = "IndexColumn";
      this.IndexColumn.ReadOnly = true;
      this.IndexColumn.Width = 50;
      // 
      // ValueColumn
      // 
      this.ValueColumn.DataPropertyName = "Value";
      this.ValueColumn.HeaderText = "Value";
      this.ValueColumn.Name = "ValueColumn";
      this.ValueColumn.ReadOnly = true;
      // 
      // HexColumn
      // 
      this.HexColumn.DataPropertyName = "HexValue";
      this.HexColumn.HeaderText = "Hex";
      this.HexColumn.Name = "HexColumn";
      this.HexColumn.Visible = false;
      // 
      // nodesGridSplitter
      // 
      this.nodesGridSplitter.BackColor = System.Drawing.SystemColors.ControlLight;
      this.nodesGridSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodesGridSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.nodesGridSplitter.Location = new System.Drawing.Point(0, 0);
      this.nodesGridSplitter.Margin = new System.Windows.Forms.Padding(0);
      this.nodesGridSplitter.Name = "nodesGridSplitter";
      // 
      // nodesGridSplitter.Panel1
      // 
      this.nodesGridSplitter.Panel1.Controls.Add(this.nodesPanel);
      // 
      // nodesGridSplitter.Panel2
      // 
      this.nodesGridSplitter.Panel2.Controls.Add(this.bufferDetailsGrid);
      this.nodesGridSplitter.Panel2.Controls.Add(this.bufferDetailsToolStrip2);
      this.nodesGridSplitter.Panel2.Controls.Add(this.bufferDetailsToolStrip1);
      this.nodesGridSplitter.Size = new System.Drawing.Size(888, 350);
      this.nodesGridSplitter.SplitterDistance = 617;
      this.nodesGridSplitter.TabIndex = 10;
      // 
      // bufferDetailsToolStrip2
      // 
      this.bufferDetailsToolStrip2.BackColor = System.Drawing.SystemColors.Control;
      this.bufferDetailsToolStrip2.CanOverflow = false;
      this.bufferDetailsToolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
      this.bufferDetailsToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.bufferDetailsToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bufferDetailsLabel});
      this.bufferDetailsToolStrip2.Location = new System.Drawing.Point(0, 25);
      this.bufferDetailsToolStrip2.Name = "bufferDetailsToolStrip2";
      this.bufferDetailsToolStrip2.Padding = new System.Windows.Forms.Padding(0);
      this.bufferDetailsToolStrip2.Size = new System.Drawing.Size(267, 29);
      this.bufferDetailsToolStrip2.TabIndex = 12;
      // 
      // bufferDetailsLabel
      // 
      this.bufferDetailsLabel.AutoSize = false;
      this.bufferDetailsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bufferDetailsLabel.Name = "bufferDetailsLabel";
      this.bufferDetailsLabel.Size = new System.Drawing.Size(85, 26);
      this.bufferDetailsLabel.Text = "Buffer Details";
      // 
      // bufferDetailsToolStrip1
      // 
      this.bufferDetailsToolStrip1.BackColor = System.Drawing.SystemColors.Control;
      this.bufferDetailsToolStrip1.CanOverflow = false;
      this.bufferDetailsToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
      this.bufferDetailsToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.bufferDetailsToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandIdLabel,
            this.commandTypeLabel,
            this.dataTypeLabel});
      this.bufferDetailsToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.bufferDetailsToolStrip1.Name = "bufferDetailsToolStrip1";
      this.bufferDetailsToolStrip1.Padding = new System.Windows.Forms.Padding(0);
      this.bufferDetailsToolStrip1.Size = new System.Drawing.Size(267, 25);
      this.bufferDetailsToolStrip1.TabIndex = 11;
      // 
      // commandIdLabel
      // 
      this.commandIdLabel.AutoSize = false;
      this.commandIdLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.commandIdLabel.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.commandIdLabel.Name = "commandIdLabel";
      this.commandIdLabel.Size = new System.Drawing.Size(50, 22);
      this.commandIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // commandTypeLabel
      // 
      this.commandTypeLabel.AutoSize = false;
      this.commandTypeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.commandTypeLabel.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
      this.commandTypeLabel.Name = "commandTypeLabel";
      this.commandTypeLabel.Size = new System.Drawing.Size(80, 22);
      this.commandTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dataTypeLabel
      // 
      this.dataTypeLabel.AutoSize = false;
      this.dataTypeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dataTypeLabel.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.dataTypeLabel.Name = "dataTypeLabel";
      this.dataTypeLabel.Size = new System.Drawing.Size(133, 22);
      this.dataTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // NodePanelHost
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nodesGridSplitter);
      this.Controls.Add(this.hiddenPanelsStrip);
      this.Name = "NodePanelHost";
      this.Size = new System.Drawing.Size(888, 375);
      this.nodesPanel.ResumeLayout(false);
      this.nodesPanel.PerformLayout();
      this.runningTimeToolStrip.ResumeLayout(false);
      this.runningTimeToolStrip.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bufferDetailsGrid)).EndInit();
      this.nodesGridSplitter.Panel1.ResumeLayout(false);
      this.nodesGridSplitter.Panel2.ResumeLayout(false);
      this.nodesGridSplitter.Panel2.PerformLayout();
      this.nodesGridSplitter.ResumeLayout(false);
      this.bufferDetailsToolStrip2.ResumeLayout(false);
      this.bufferDetailsToolStrip2.PerformLayout();
      this.bufferDetailsToolStrip1.ResumeLayout(false);
      this.bufferDetailsToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip hiddenPanelsStrip;
    private System.Windows.Forms.Panel nodesPanel;
    private System.Windows.Forms.DataGridView bufferDetailsGrid;
    private System.Windows.Forms.SplitContainer nodesGridSplitter;
    private System.Windows.Forms.ToolStrip runningTimeToolStrip;
    private System.Windows.Forms.ToolStripLabel runningTimeValueLabel;
    private System.Windows.Forms.ToolStripLabel runningTimeLabel;
    private System.Windows.Forms.ToolStrip bufferDetailsToolStrip1;
    private System.Windows.Forms.ToolStripLabel commandTypeLabel;
    private System.Windows.Forms.ToolStrip bufferDetailsToolStrip2;
    private System.Windows.Forms.ToolStripLabel commandIdLabel;
    private System.Windows.Forms.DataGridViewTextBoxColumn IndexColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn HexColumn;
    private System.Windows.Forms.ToolStripLabel bufferDetailsLabel;
    private System.Windows.Forms.ToolStripLabel dataTypeLabel;
  }
}
