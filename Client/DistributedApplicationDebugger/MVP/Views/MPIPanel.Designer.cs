namespace DistributedApplicationDebugger.Views
{
  partial class MPIPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MPIPanel));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.mpiDataSet = new System.Data.DataSet();
      this.MPI = new System.Data.DataTable();
      this.messageColumn = new System.Data.DataColumn();
      this.dataColumn2 = new System.Data.DataColumn();
      this.commandContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.getBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.messageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.mpiPanelToolStrip = new System.Windows.Forms.ToolStrip();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.messageCountLabel = new System.Windows.Forms.ToolStripLabel();
      this.messageCountValueLabel = new System.Windows.Forms.ToolStripLabel();
      this.filterDropDown = new System.Windows.Forms.ToolStripDropDownButton();
      this.checkAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uncheckMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.initMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rankMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.isendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.recvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.irecvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.waitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.probeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.iprobeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.barrierMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.finalizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.commandDetailsToolStrip = new System.Windows.Forms.ToolStrip();
      this.hideButton = new System.Windows.Forms.ToolStripButton();
      this.commandDetailsLabel = new System.Windows.Forms.ToolStripLabel();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
      this.commandDetailsTextBox = new System.Windows.Forms.TextBox();
      this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
      this.commandDetailsSplitter = new System.Windows.Forms.SplitContainer();
      this.mpiGrid = new System.Windows.Forms.DataGridView();
      this.dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
      this.commandDetailsFooter = new System.Windows.Forms.ToolStrip();
      this.detailsButton = new System.Windows.Forms.ToolStripButton();
      this.dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn8 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn10 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewImageColumn11 = new System.Windows.Forms.DataGridViewImageColumn();
      this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.commandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.statusColumn = new System.Windows.Forms.DataGridViewImageColumn();
      this.objectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.mpiDataSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MPI)).BeginInit();
      this.commandContextMenu.SuspendLayout();
      this.mpiPanelToolStrip.SuspendLayout();
      this.commandDetailsToolStrip.SuspendLayout();
      this.commandDetailsSplitter.Panel1.SuspendLayout();
      this.commandDetailsSplitter.Panel2.SuspendLayout();
      this.commandDetailsSplitter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.mpiGrid)).BeginInit();
      this.commandDetailsFooter.SuspendLayout();
      this.SuspendLayout();
      // 
      // mpiDataSet
      // 
      this.mpiDataSet.DataSetName = "NewDataSet";
      this.mpiDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.MPI});
      // 
      // MPI
      // 
      this.MPI.Columns.AddRange(new System.Data.DataColumn[] {
            this.messageColumn,
            this.dataColumn2});
      this.MPI.TableName = "MPI";
      // 
      // messageColumn
      // 
      this.messageColumn.Caption = "Command";
      this.messageColumn.ColumnName = "commandColumn";
      this.messageColumn.ReadOnly = true;
      // 
      // dataColumn2
      // 
      this.dataColumn2.ColumnName = "idColumn";
      this.dataColumn2.DataType = typeof(ulong);
      // 
      // commandContextMenu
      // 
      this.commandContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getBufferToolStripMenuItem,
            this.messageToolStripMenuItem});
      this.commandContextMenu.Name = "messageContextMenu";
      this.commandContextMenu.Size = new System.Drawing.Size(128, 48);
      this.commandContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.commandContextMenu_Opening);
      // 
      // getBufferToolStripMenuItem
      // 
      this.getBufferToolStripMenuItem.Name = "getBufferToolStripMenuItem";
      this.getBufferToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.getBufferToolStripMenuItem.Text = "Get Buffer";
      this.getBufferToolStripMenuItem.Click += new System.EventHandler(this.getBufferToolStripMenuItem_Click);
      // 
      // messageToolStripMenuItem
      // 
      this.messageToolStripMenuItem.Name = "messageToolStripMenuItem";
      this.messageToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.messageToolStripMenuItem.Text = "Message";
      this.messageToolStripMenuItem.Click += new System.EventHandler(this.messageDetailsToolStripMenuItem_Click);
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "statusColumn";
      this.dataGridViewTextBoxColumn1.HeaderText = "Status";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 66;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "statusColumn";
      this.dataGridViewTextBoxColumn2.HeaderText = "Status";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 66;
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.DataPropertyName = "statusColumn";
      this.dataGridViewTextBoxColumn3.HeaderText = "Status";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 66;
      // 
      // mpiPanelToolStrip
      // 
      this.mpiPanelToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.mpiPanelToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton,
            this.messageCountLabel,
            this.messageCountValueLabel,
            this.filterDropDown});
      this.mpiPanelToolStrip.Location = new System.Drawing.Point(0, 0);
      this.mpiPanelToolStrip.Name = "mpiPanelToolStrip";
      this.mpiPanelToolStrip.Size = new System.Drawing.Size(300, 25);
      this.mpiPanelToolStrip.TabIndex = 2;
      this.mpiPanelToolStrip.Text = "toolStrip1";
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
      // filterDropDown
      // 
      this.filterDropDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.filterDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.filterDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllMenuItem,
            this.uncheckMenuItem,
            this.initMenuItem,
            this.rankMenuItem,
            this.sizeMenuItem,
            this.sendMenuItem,
            this.isendMenuItem,
            this.recvMenuItem,
            this.irecvMenuItem,
            this.waitMenuItem,
            this.probeMenuItem,
            this.iprobeMenuItem,
            this.barrierMenuItem,
            this.finalizeMenuItem});
      this.filterDropDown.Image = ((System.Drawing.Image)(resources.GetObject("filterDropDown.Image")));
      this.filterDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.filterDropDown.Name = "filterDropDown";
      this.filterDropDown.Size = new System.Drawing.Size(82, 22);
      this.filterDropDown.Text = "Commands";
      // 
      // checkAllMenuItem
      // 
      this.checkAllMenuItem.Name = "checkAllMenuItem";
      this.checkAllMenuItem.Size = new System.Drawing.Size(152, 22);
      this.checkAllMenuItem.Text = "Check All";
      this.checkAllMenuItem.Click += new System.EventHandler(this.checkAllMenuItem_Click);
      // 
      // uncheckMenuItem
      // 
      this.uncheckMenuItem.Name = "uncheckMenuItem";
      this.uncheckMenuItem.Size = new System.Drawing.Size(152, 22);
      this.uncheckMenuItem.Text = "Uncheck All";
      this.uncheckMenuItem.Click += new System.EventHandler(this.uncheckMenuItem_Click);
      // 
      // initMenuItem
      // 
      this.initMenuItem.Checked = true;
      this.initMenuItem.CheckOnClick = true;
      this.initMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.initMenuItem.Name = "initMenuItem";
      this.initMenuItem.Size = new System.Drawing.Size(152, 22);
      this.initMenuItem.Text = "MPI_INIT";
      this.initMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // rankMenuItem
      // 
      this.rankMenuItem.Checked = true;
      this.rankMenuItem.CheckOnClick = true;
      this.rankMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.rankMenuItem.Name = "rankMenuItem";
      this.rankMenuItem.Size = new System.Drawing.Size(152, 22);
      this.rankMenuItem.Text = "MPI_RANK";
      this.rankMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // sizeMenuItem
      // 
      this.sizeMenuItem.Checked = true;
      this.sizeMenuItem.CheckOnClick = true;
      this.sizeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.sizeMenuItem.Name = "sizeMenuItem";
      this.sizeMenuItem.Size = new System.Drawing.Size(152, 22);
      this.sizeMenuItem.Text = "MPI_SIZE";
      this.sizeMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // sendMenuItem
      // 
      this.sendMenuItem.Checked = true;
      this.sendMenuItem.CheckOnClick = true;
      this.sendMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.sendMenuItem.Name = "sendMenuItem";
      this.sendMenuItem.Size = new System.Drawing.Size(152, 22);
      this.sendMenuItem.Text = "MPI_SEND";
      this.sendMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // isendMenuItem
      // 
      this.isendMenuItem.Checked = true;
      this.isendMenuItem.CheckOnClick = true;
      this.isendMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.isendMenuItem.Name = "isendMenuItem";
      this.isendMenuItem.Size = new System.Drawing.Size(152, 22);
      this.isendMenuItem.Text = "MPI_ISEND";
      this.isendMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // recvMenuItem
      // 
      this.recvMenuItem.Checked = true;
      this.recvMenuItem.CheckOnClick = true;
      this.recvMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.recvMenuItem.Name = "recvMenuItem";
      this.recvMenuItem.Size = new System.Drawing.Size(152, 22);
      this.recvMenuItem.Text = "MPI_RECV";
      this.recvMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // irecvMenuItem
      // 
      this.irecvMenuItem.Checked = true;
      this.irecvMenuItem.CheckOnClick = true;
      this.irecvMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.irecvMenuItem.Name = "irecvMenuItem";
      this.irecvMenuItem.Size = new System.Drawing.Size(152, 22);
      this.irecvMenuItem.Text = "MPI_IRECV";
      this.irecvMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // waitMenuItem
      // 
      this.waitMenuItem.Checked = true;
      this.waitMenuItem.CheckOnClick = true;
      this.waitMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.waitMenuItem.Name = "waitMenuItem";
      this.waitMenuItem.Size = new System.Drawing.Size(152, 22);
      this.waitMenuItem.Text = "MPI_WAIT";
      this.waitMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // probeMenuItem
      // 
      this.probeMenuItem.Checked = true;
      this.probeMenuItem.CheckOnClick = true;
      this.probeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.probeMenuItem.Name = "probeMenuItem";
      this.probeMenuItem.Size = new System.Drawing.Size(152, 22);
      this.probeMenuItem.Text = "MPI_PROBE";
      this.probeMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // iprobeMenuItem
      // 
      this.iprobeMenuItem.Checked = true;
      this.iprobeMenuItem.CheckOnClick = true;
      this.iprobeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.iprobeMenuItem.Name = "iprobeMenuItem";
      this.iprobeMenuItem.Size = new System.Drawing.Size(152, 22);
      this.iprobeMenuItem.Text = "MPI_IPROBE";
      this.iprobeMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // barrierMenuItem
      // 
      this.barrierMenuItem.Checked = true;
      this.barrierMenuItem.CheckOnClick = true;
      this.barrierMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.barrierMenuItem.Name = "barrierMenuItem";
      this.barrierMenuItem.Size = new System.Drawing.Size(152, 22);
      this.barrierMenuItem.Text = "MPI_BARRIER";
      this.barrierMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
      // 
      // finalizeMenuItem
      // 
      this.finalizeMenuItem.Checked = true;
      this.finalizeMenuItem.CheckOnClick = true;
      this.finalizeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.finalizeMenuItem.Name = "finalizeMenuItem";
      this.finalizeMenuItem.Size = new System.Drawing.Size(152, 22);
      this.finalizeMenuItem.Text = "MPI_FINALIZE";
      this.finalizeMenuItem.CheckedChanged += new System.EventHandler(this.filterMenuItem_CheckedChanged);
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
      this.commandDetailsToolStrip.TabIndex = 0;
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
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.DataPropertyName = "statusColumn";
      this.dataGridViewTextBoxColumn4.HeaderText = "Status";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.Width = 93;
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.DataPropertyName = "statusColumn";
      this.dataGridViewTextBoxColumn5.HeaderText = "Status";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Width = 69;
      // 
      // dataGridViewImageColumn1
      // 
      this.dataGridViewImageColumn1.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn1.FillWeight = 2F;
      this.dataGridViewImageColumn1.HeaderText = "Status";
      this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
      this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn1.Width = 74;
      // 
      // dataGridViewImageColumn2
      // 
      this.dataGridViewImageColumn2.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn2.FillWeight = 2F;
      this.dataGridViewImageColumn2.HeaderText = "Status";
      this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
      this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn2.Width = 74;
      // 
      // dataGridViewImageColumn3
      // 
      this.dataGridViewImageColumn3.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn3.FillWeight = 2F;
      this.dataGridViewImageColumn3.HeaderText = "Status";
      this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
      this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn3.Width = 73;
      // 
      // dataGridViewImageColumn4
      // 
      this.dataGridViewImageColumn4.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn4.FillWeight = 1.619751F;
      this.dataGridViewImageColumn4.HeaderText = "Status";
      this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
      this.dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn4.Width = 60;
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
      this.commandDetailsTextBox.TabIndex = 3;
      // 
      // dataGridViewImageColumn5
      // 
      this.dataGridViewImageColumn5.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn5.FillWeight = 2F;
      this.dataGridViewImageColumn5.HeaderText = "Status";
      this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
      this.dataGridViewImageColumn5.ReadOnly = true;
      this.dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn5.Width = 69;
      // 
      // commandDetailsSplitter
      // 
      this.commandDetailsSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.commandDetailsSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.commandDetailsSplitter.Location = new System.Drawing.Point(0, 25);
      this.commandDetailsSplitter.Name = "commandDetailsSplitter";
      this.commandDetailsSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // commandDetailsSplitter.Panel1
      // 
      this.commandDetailsSplitter.Panel1.Controls.Add(this.mpiGrid);
      // 
      // commandDetailsSplitter.Panel2
      // 
      this.commandDetailsSplitter.Panel2.Controls.Add(this.commandDetailsTextBox);
      this.commandDetailsSplitter.Panel2.Controls.Add(this.commandDetailsToolStrip);
      this.commandDetailsSplitter.Size = new System.Drawing.Size(300, 300);
      this.commandDetailsSplitter.SplitterDistance = 69;
      this.commandDetailsSplitter.TabIndex = 4;
      // 
      // mpiGrid
      // 
      this.mpiGrid.AllowUserToAddRows = false;
      this.mpiGrid.AllowUserToDeleteRows = false;
      this.mpiGrid.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.PapayaWhip;
      this.mpiGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.mpiGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.mpiGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.mpiGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.mpiGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.mpiGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.commandColumn,
            this.lineColumn,
            this.statusColumn,
            this.objectColumn});
      this.mpiGrid.ContextMenuStrip = this.commandContextMenu;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.mpiGrid.DefaultCellStyle = dataGridViewCellStyle3;
      this.mpiGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mpiGrid.Location = new System.Drawing.Point(0, 0);
      this.mpiGrid.Name = "mpiGrid";
      this.mpiGrid.ReadOnly = true;
      this.mpiGrid.RowHeadersVisible = false;
      this.mpiGrid.RowHeadersWidth = 20;
      this.mpiGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.mpiGrid.Size = new System.Drawing.Size(300, 69);
      this.mpiGrid.TabIndex = 1;
      this.mpiGrid.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.mpiGrid_CellToolTipTextNeeded);
      this.mpiGrid.SelectionChanged += new System.EventHandler(this.mpiGrid_SelectionChanged);
      this.mpiGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mpiGrid_MouseDown);
      // 
      // dataGridViewImageColumn6
      // 
      this.dataGridViewImageColumn6.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn6.FillWeight = 2F;
      this.dataGridViewImageColumn6.HeaderText = "Status";
      this.dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
      this.dataGridViewImageColumn6.ReadOnly = true;
      this.dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn6.Width = 65;
      // 
      // commandDetailsFooter
      // 
      this.commandDetailsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.commandDetailsFooter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.commandDetailsFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsButton});
      this.commandDetailsFooter.Location = new System.Drawing.Point(0, 325);
      this.commandDetailsFooter.Name = "commandDetailsFooter";
      this.commandDetailsFooter.Size = new System.Drawing.Size(300, 25);
      this.commandDetailsFooter.TabIndex = 5;
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
      // dataGridViewImageColumn7
      // 
      this.dataGridViewImageColumn7.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn7.FillWeight = 2F;
      this.dataGridViewImageColumn7.HeaderText = "Status";
      this.dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
      this.dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn7.Width = 65;
      // 
      // dataGridViewImageColumn8
      // 
      this.dataGridViewImageColumn8.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn8.FillWeight = 2F;
      this.dataGridViewImageColumn8.HeaderText = "Status";
      this.dataGridViewImageColumn8.Name = "dataGridViewImageColumn8";
      this.dataGridViewImageColumn8.ReadOnly = true;
      this.dataGridViewImageColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn8.Width = 70;
      // 
      // dataGridViewImageColumn9
      // 
      this.dataGridViewImageColumn9.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn9.FillWeight = 2F;
      this.dataGridViewImageColumn9.HeaderText = "Status";
      this.dataGridViewImageColumn9.Name = "dataGridViewImageColumn9";
      this.dataGridViewImageColumn9.ReadOnly = true;
      this.dataGridViewImageColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn9.Width = 70;
      // 
      // dataGridViewImageColumn10
      // 
      this.dataGridViewImageColumn10.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn10.FillWeight = 2F;
      this.dataGridViewImageColumn10.HeaderText = "Status";
      this.dataGridViewImageColumn10.Name = "dataGridViewImageColumn10";
      this.dataGridViewImageColumn10.ReadOnly = true;
      this.dataGridViewImageColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn10.Width = 70;
      // 
      // dataGridViewImageColumn11
      // 
      this.dataGridViewImageColumn11.DataPropertyName = "statusColumn";
      this.dataGridViewImageColumn11.FillWeight = 2F;
      this.dataGridViewImageColumn11.HeaderText = "Status";
      this.dataGridViewImageColumn11.Name = "dataGridViewImageColumn11";
      this.dataGridViewImageColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewImageColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.dataGridViewImageColumn11.Width = 70;
      // 
      // idColumn
      // 
      this.idColumn.DataPropertyName = "idColumn";
      this.idColumn.FillWeight = 1.5F;
      this.idColumn.HeaderText = "Id";
      this.idColumn.Name = "idColumn";
      this.idColumn.ReadOnly = true;
      // 
      // commandColumn
      // 
      this.commandColumn.DataPropertyName = "commandColumn";
      this.commandColumn.FillWeight = 3F;
      this.commandColumn.HeaderText = "Command";
      this.commandColumn.Name = "commandColumn";
      this.commandColumn.ReadOnly = true;
      // 
      // lineColumn
      // 
      this.lineColumn.DataPropertyName = "lineColumn";
      this.lineColumn.FillWeight = 2F;
      this.lineColumn.HeaderText = "Line #";
      this.lineColumn.Name = "lineColumn";
      this.lineColumn.ReadOnly = true;
      // 
      // statusColumn
      // 
      this.statusColumn.DataPropertyName = "statusColumn";
      this.statusColumn.FillWeight = 2F;
      this.statusColumn.HeaderText = "Status";
      this.statusColumn.Name = "statusColumn";
      this.statusColumn.ReadOnly = true;
      this.statusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.statusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // objectColumn
      // 
      this.objectColumn.DataPropertyName = "objectColumn";
      this.objectColumn.HeaderText = "objectColumn";
      this.objectColumn.Name = "objectColumn";
      this.objectColumn.ReadOnly = true;
      this.objectColumn.Visible = false;
      // 
      // MPIPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.commandDetailsSplitter);
      this.Controls.Add(this.mpiPanelToolStrip);
      this.Controls.Add(this.commandDetailsFooter);
      this.Name = "MPIPanel";
      this.Size = new System.Drawing.Size(300, 350);
      ((System.ComponentModel.ISupportInitialize)(this.mpiDataSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MPI)).EndInit();
      this.commandContextMenu.ResumeLayout(false);
      this.mpiPanelToolStrip.ResumeLayout(false);
      this.mpiPanelToolStrip.PerformLayout();
      this.commandDetailsToolStrip.ResumeLayout(false);
      this.commandDetailsToolStrip.PerformLayout();
      this.commandDetailsSplitter.Panel1.ResumeLayout(false);
      this.commandDetailsSplitter.Panel2.ResumeLayout(false);
      this.commandDetailsSplitter.Panel2.PerformLayout();
      this.commandDetailsSplitter.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.mpiGrid)).EndInit();
      this.commandDetailsFooter.ResumeLayout(false);
      this.commandDetailsFooter.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Data.DataSet mpiDataSet;
    private System.Data.DataTable MPI;
    private System.Data.DataColumn messageColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn messageColumnDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.ToolStrip commandDetailsToolStrip;
    private System.Data.DataColumn dataColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.ToolStrip mpiPanelToolStrip;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.ToolStripLabel messageCountLabel;
    private System.Windows.Forms.ToolStripLabel messageCountValueLabel;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
    private System.Windows.Forms.ToolStripButton hideButton;
    private System.Windows.Forms.TextBox commandDetailsTextBox;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
    private System.Windows.Forms.SplitContainer commandDetailsSplitter;
    private System.Windows.Forms.ToolStripLabel commandDetailsLabel;
    private System.Windows.Forms.ToolStrip commandDetailsFooter;
    private System.Windows.Forms.ToolStripButton detailsButton;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn7;
    private System.Windows.Forms.ContextMenuStrip commandContextMenu;
    private System.Windows.Forms.ToolStripMenuItem getBufferToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem messageToolStripMenuItem;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn8;
    private System.Windows.Forms.ToolStripDropDownButton filterDropDown;
    private System.Windows.Forms.ToolStripMenuItem initMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rankMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sizeMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sendMenuItem;
    private System.Windows.Forms.ToolStripMenuItem isendMenuItem;
    private System.Windows.Forms.ToolStripMenuItem recvMenuItem;
    private System.Windows.Forms.ToolStripMenuItem irecvMenuItem;
    private System.Windows.Forms.ToolStripMenuItem waitMenuItem;
    private System.Windows.Forms.ToolStripMenuItem probeMenuItem;
    private System.Windows.Forms.ToolStripMenuItem iprobeMenuItem;
    private System.Windows.Forms.ToolStripMenuItem barrierMenuItem;
    private System.Windows.Forms.ToolStripMenuItem finalizeMenuItem;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn9;
    private System.Windows.Forms.ToolStripMenuItem checkAllMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uncheckMenuItem;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn10;
    private System.Windows.Forms.DataGridView mpiGrid;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn11;
    private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn commandColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn lineColumn;
    private System.Windows.Forms.DataGridViewImageColumn statusColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn objectColumn;
  }
}
