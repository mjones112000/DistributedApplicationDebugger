using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DistributedApplicationDebugger.Communication.MPICommands;
using DistributedApplicationDebugger.Properties;
using System.Linq;
using System.Collections.Generic;
using DistributedApplicationDebugger.Communication;
using System.Text;

namespace DistributedApplicationDebugger.Views
{
  public partial class MPIPanel : UserControl
  {
    private readonly Image UNKNOWN_IMAGE;
    private readonly Image PASSED_IMAGE;
    private readonly Image WARNING_IMAGE;
    private readonly Image ERROR_IMAGE;

    DataTable dt = new DataTable();

    public MPIPanel()
    {
      InitializeComponent();

      UNKNOWN_IMAGE = Resources.Question;
      PASSED_IMAGE = Resources.Ok;
      WARNING_IMAGE = Resources.Warning;
      ERROR_IMAGE = Resources.Invalid;

      commandDetailsFooter.Visible = true;
      commandDetailsSplitter.Panel2Collapsed = true;

      dt.Columns.Add(new DataColumn("idColumn", typeof(ulong)));
      dt.Columns.Add(new DataColumn("commandColumn", typeof(string)));
      dt.Columns.Add(new DataColumn("lineColumn", typeof(long)));
      dt.Columns.Add(new DataColumn("statusColumn", typeof(object)));
      dt.Columns.Add(new DataColumn("objectColumn", typeof(object)));
      

      mpiGrid.DataSource = dt;

      SetFilter();
    }

    private void mpiGrid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
    {
      if (e.RowIndex > -1)
      {
        DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
        DataRowView dataRow = row.DataBoundItem as DataRowView;

        e.ToolTipText = ((MPICommand)dataRow["objectColumn"]).Tooltip;
      }
      else
      {
        e.ToolTipText = string.Empty;
      }

    }

    public void UpdateMessageCount()
    {
      //messageCountValueLabel.Text = mpiDataSet.Tables[0].Rows.Count.ToString();
      messageCountValueLabel.Text = dt.Rows.Count.ToString();
    }

    public void BeginLoadData()
    {
      //mpiDataSet.Tables[0].BeginLoadData();
      dt.BeginLoadData();
    }

    public void Reset()
    {
      //mpiDataSet.Tables[0].Rows.Clear();
      dt.Rows.Clear();
      mpiGrid.Refresh();
      messageCountValueLabel.Text = string.Empty;
    }

    public void AddCommand(MPICommand command)
    {
      //DataRow newRow = mpiDataSet.Tables[0].NewRow();
      DataRow newRow = dt.NewRow();
      newRow["idColumn"] = command.CommandId;
      newRow["commandColumn"] = command.Name;
      newRow["lineColumn"] = command.LineNum;

      if (!command.IsValid)
      {
          newRow["statusColumn"] = WARNING_IMAGE;
      }
      else if (command.ReturnValue.HasValue)
      {
          newRow["statusColumn"] = command.ReturnValue.Value == MPICommand.MPI_SUCCESS ? PASSED_IMAGE : ERROR_IMAGE;
      }
      else
      {
        newRow["statusColumn"] = UNKNOWN_IMAGE;
      }

      newRow["objectColumn"] = command;

      dt.Rows.Add(newRow);
      //mpiDataSet.Tables[0].Rows.Add(newRow);


    }

    public void UpdateCommand(int commandId)
    {
      DataRow dataRow = dt.Rows[commandId];
      //DataRow dataRow = mpiDataSet.Tables[0].Rows[commandId];
      
      MPICommand command = dataRow["objectColumn"] as MPICommand;

      if (!command.IsValid)
      {
          dataRow["statusColumn"] = WARNING_IMAGE;
      }
      else if (command.ReturnValue.HasValue)
      {
          dataRow["statusColumn"] = command.ReturnValue.Value == MPICommand.MPI_SUCCESS ? PASSED_IMAGE : ERROR_IMAGE;
      }
      else
      {
        dataRow["statusColumn"] = UNKNOWN_IMAGE;
      }
    }

    public void UpdateBuffer(int commandId, List<BufferValue> bufferValues)
    {
      DataRow dataRow = dt.Rows[commandId];
      //DataRow dataRow = mpiDataSet.Tables[0].Rows[commandId];
      IMessageCommand messageCommand = dataRow["objectColumn"] as IMessageCommand;
      if (messageCommand != null)
      {
        messageCommand.Buf = bufferValues;
      }
    }

    public void EndLoadData()
    {
      dt.EndLoadData();
      //mpiDataSet.Tables[0].EndLoadData();
    }

    public MPICommand SelectedCommand
    {
      get
      {
        if (mpiGrid.SelectedRows.Count > 0)
        {
          DataRowView dataRow = mpiGrid.SelectedRows[0].DataBoundItem as DataRowView;
          return dataRow["objectColumn"] as MPICommand;
        }
        else
        {
          return null;
        }
      }
    }

    public void RefreshCommandDetails()
    {
      if (mpiGrid.SelectedCells.Count == 0)
      {
        commandDetailsTextBox.Clear();
      }
      else
      {
        commandDetailsTextBox.Text = SelectedCommand.Tooltip;
      }
    }

    private void mpiGrid_SelectionChanged(object sender, EventArgs e)
    {
      RefreshCommandDetails();
      NodePanelHost.MPIMessageSelected(this, SelectedCommand);
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      mpiGrid.Refresh();
      commandDetailsTextBox.Refresh();
    }

    private void hideButton_Click(object sender, EventArgs e)
    {
      commandDetailsSplitter.Panel2Collapsed = true;
      commandDetailsFooter.Visible = true;
    }

    private void detailsButton_Click(object sender, EventArgs e)
    {
      commandDetailsFooter.Visible = false;
      commandDetailsSplitter.Panel2Collapsed = false;
    }

    public void SelectCommand(MPICommand command)
    {
      var row = mpiGrid.Rows.OfType<DataGridViewRow>()
        .FirstOrDefault(x => ((MPICommand)((DataRowView)x.DataBoundItem)["objectColumn"]).CommandId == command.CommandId);

      if (row != null)
      {
        mpiGrid.CurrentCell = row.Cells[0];

      }
    }

    public SessionMode SessionMode
    {
      get;
      set;
    }

    private void getBufferToolStripMenuItem_Click(object sender, EventArgs e)
    {
      List<IMessageCommand> messages = new List<IMessageCommand>();
      foreach (DataGridViewRow row in mpiGrid.SelectedRows)
      {
        DataRowView dataRow = row.DataBoundItem as DataRowView;
        IMessageCommand messageCommand = dataRow["objectColumn"] as IMessageCommand;
        if (messageCommand != null)
        {
          messages.Add(messageCommand);
        }
      }
      NodePanelHost.MessageBufferRequest(this, messages);
    }

    private void messageDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataRowView dataRow = mpiGrid.SelectedRows[0].DataBoundItem as DataRowView;
      IMessageCommand messageCommand = dataRow["objectColumn"] as IMessageCommand;
      if (messageCommand != null)
      {
        NodePanelHost.MessageNavigateRequest(this, messageCommand);
      }
    }

    private void commandContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {

      if (mpiGrid.SelectedRows.Count == 0)
      {
        e.Cancel = true;
      }
      else
      {
        getBufferToolStripMenuItem.Enabled = (SessionMode == Views.SessionMode.Record || SessionMode == Views.SessionMode.Replay)
          && mpiGrid.SelectedRows.OfType<DataGridViewRow>().Any(x => (x.DataBoundItem as DataRowView)["objectColumn"] is IMessageCommand);

        messageToolStripMenuItem.Enabled = mpiGrid.SelectedRows.Count == 1 && (mpiGrid.SelectedRows[0].DataBoundItem as DataRowView)["objectColumn"] is IMessageCommand;
      }
    }

    private void SetFilter()
    {
      StringBuilder sb = new StringBuilder();
      List<ToolStripMenuItem> checkedMenuItems = filterDropDown.DropDownItems.OfType<ToolStripMenuItem>().Where(x => x.Checked).ToList();

      if (checkedMenuItems.Count > 0)
      {
        sb.Append(String.Format("commandColumn = '{0}'", checkedMenuItems.First().Text));

        foreach (var checkedMenuItem in checkedMenuItems.Skip(1))
        {
          sb.Append(String.Format(" OR commandColumn = '{0}'", checkedMenuItem.Text));
        }
      }
      else
      {
        //We don't want anything, just make something up
        sb = new StringBuilder("commandColumn = 'XXXX'");
      }

      dt.DefaultView.RowFilter = sb.ToString();

      filterDropDown.Text = String.Format("Commands ({0})", checkedMenuItems.Count);
    }

    private void filterMenuItem_CheckedChanged(object sender, EventArgs e)
    {
      SetFilter();
    }

    private void hideMatchedButton_CheckedChanged(object sender, EventArgs e)
    {
      SetFilter();
    }

    private void checkAllMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var menuItem in filterDropDown.DropDownItems.OfType<ToolStripMenuItem>().Where(x => x != checkAllMenuItem && x != uncheckMenuItem))
      {
        menuItem.Checked = true;
      }

      SetFilter();
    }

    private void uncheckMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var menuItem in filterDropDown.DropDownItems.OfType<ToolStripMenuItem>())
      {
        menuItem.Checked = false;
      }

      SetFilter();
    }

    private void mpiGrid_MouseDown(object sender, MouseEventArgs e)
    {
      RefreshCommandDetails();
      NodePanelHost.MPIMessageSelected(this, SelectedCommand);
    }
  }
}
