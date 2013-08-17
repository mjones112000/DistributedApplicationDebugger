using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributedApplicationDebugger.Communication;
using DistributedApplicationDebugger.Communication.MPICommands;
using System.Text;

namespace DistributedApplicationDebugger.Views
{
  public partial class MessagesPanel : UserControl
  {
    DataTable dt = new DataTable();
    private bool _autoSelectionChanging = false;

    public MessagesPanel()
    {
      InitializeComponent();
      dt.Columns.Add(new DataColumn("idColumn", typeof(int)));
      dt.Columns.Add(new DataColumn("sizeColumn", typeof(int)));
      dt.Columns.Add(new DataColumn("typeColumn", typeof(string)));
      dt.Columns.Add(new DataColumn("srcColumn", typeof(string)));
      dt.Columns.Add(new DataColumn("destColumn", typeof(string)));
      dt.Columns.Add(new DataColumn("tagColumn", typeof(string)));
      dt.Columns.Add(new DataColumn("objectColumn", typeof(IMessageCommand)));
      dt.Columns.Add(new DataColumn("isMatchedCol", typeof(bool)));
      
      messagesGrid.DataSource = dt;

      messageDetailsFooter.Visible = true;
      messageDetailsSplitter.Panel2Collapsed = true;

      SetFilter();
    }

    public int NodeId
    {
      get;
      set;
    }

    public void Reset()
    {
      dt.Clear();
      messagesGrid.Refresh();
      messageCountValueLabel.Text = string.Empty;
    }

    public void BeginLoadData()
    {
      dt.BeginLoadData();
    }

    public void AddMessage(IMessageCommand messageCommand) 
    {
      DataRow newRow = dt.NewRow();
      AssignDataRow(newRow, messageCommand);
                                    
      messageCommand.DisplayState = messageCommand.MatchingMessageCommand == null ? 
          MessageDisplayState.DisplayedUnMatched: MessageDisplayState.DisplayedMatched;

      dt.Rows.Add(newRow);

      messageCountValueLabel.Text = dt.Rows.Count.ToString();
      messageCommand.MessageDataRow = newRow;
    }

    public void UpdateMessage(IMessageCommand messageCommand)
    {
      //This message should be displayed currently
      DataRow dataRow = messageCommand.MessageDataRow;
      if (dataRow != null)
      {
        AssignDataRow(dataRow, messageCommand);
        messageCommand.DisplayState = Communication.MessageDisplayState.DisplayedMatched;
      }
    }

    public void MessageNavigate(IMessageCommand messageCommand)
    {
      var matchingRow = messagesGrid.Rows.OfType<DataGridViewRow>().FirstOrDefault(
                        x => (x.DataBoundItem as DataRowView).Row == messageCommand.MessageDataRow);

      if (matchingRow != null)
      {
        messagesGrid.CurrentCell = null;
        messagesGrid.CurrentCell = matchingRow.Cells["idColumn"];
      }
    }

    public void MessageMatch(IMessageCommand messageCommand)
    {
      var matchingRow = messagesGrid.Rows.OfType<DataGridViewRow>().FirstOrDefault(
                        x => (x.DataBoundItem as DataRowView).Row == messageCommand.MessageDataRow);

      if (matchingRow != null)
      {
        _autoSelectionChanging = true;
        messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
        messagesGrid.CurrentCell = null;
        messagesGrid.CurrentCell = matchingRow.Cells["idColumn"];

        foreach (DataGridViewCell cell in matchingRow.Cells)
        {
          cell.Style = new DataGridViewCellStyle();
          if (((bool)matchingRow.Cells["isMatchedCol"].Value))
          {
            cell.Style.ForeColor = Color.Black;
            cell.Style.SelectionForeColor = Color.Black;
          }
          else
          {
            cell.Style.ForeColor = Color.Red;
            cell.Style.SelectionForeColor = Color.Red;
          }
        }

        _autoSelectionChanging = false;
      }
    }

    public void DisableMatchMode()
    {
      this.matchModeButton.Checked = false;
      var currentCell = messagesGrid.CurrentCell;
      if (currentCell != null)
      {
        messagesGrid.CurrentCell = null;
        messagesGrid.CurrentCell = currentCell;
      }
    }

    public void ClearSelectedMessage()
    {
      messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Empty;
    }

    public void EndLoadData()
    {
      dt.EndLoadData();
    }

    public MPICommand SelectedCommand
    {
      get
      {
        if (messagesGrid.SelectedRows.Count > 0)
        {
          DataRowView dataRow = messagesGrid.SelectedRows[0].DataBoundItem as DataRowView;
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
      if (messagesGrid.SelectedCells.Count == 0)
      {
        commandDetailsTextBox.Clear();
      }
      else
      {
        commandDetailsTextBox.Text = SelectedCommand.Tooltip;
      }
    }

    private void AssignDataRow(DataRow dataRow, IMessageCommand messageCommand)
    {
      dataRow.BeginEdit();
      if (messageCommand.MessageId.HasValue)
        dataRow["idColumn"] = messageCommand.MessageId;

      dataRow["sizeColumn"] = messageCommand.Count;
      dataRow["typeColumn"] = messageCommand.Name;
      if (messageCommand.Dest.HasValue)
        dataRow["destColumn"] = messageCommand.Dest;

      if (messageCommand.Src != null)
        dataRow["srcColumn"] = messageCommand.Src.Value;

      dataRow["objectColumn"] = messageCommand;
      dataRow["tagColumn"] = messageCommand.Tag.Value;
      dataRow["isMatchedCol"] = messageCommand.MatchingMessageCommand != null;
      dataRow.EndEdit();
    }

    private void matchModeButton_Click(object sender, EventArgs e)
    {
      if (matchModeButton.Checked)
      {
        NodePanelHost.MatchModeEnabled(this, this.NodeId);
        IMessageCommand selectedMessage = GetSelectedMessageCommand();

        if (selectedMessage != null && selectedMessage.MatchingMessageCommand != null)
        {
          NodePanelHost.MessageMatchRequest(this, selectedMessage.MatchingMessageCommand);
        }
      }
      else
      {
        IMessageCommand selectedMessage = GetSelectedMessageCommand();

        if (selectedMessage != null && selectedMessage.MatchingMessageCommand != null)
        {
          NodePanelHost.MatchSelectionCleared(this, selectedMessage.MatchingMessageCommand);
        }
      }

      messagesGrid.RowsDefaultCellStyle.SelectionBackColor = matchModeButton.Checked ? Color.Yellow : Color.Empty;
      messagesGrid.RowsDefaultCellStyle.SelectionForeColor = matchModeButton.Checked ? Color.Black : DefaultForeColor;

      RefreshCommandDetails();
      NodePanelHost.MPIMessageSelected(this, SelectedCommand);
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      messagesGrid.Refresh();
    }

    public SessionMode SessionMode
    {
      get;
      set;
    }

    private void messagesGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      DataGridViewRow dataGridViewRow = ((DataGridView)sender).Rows[e.RowIndex];
      DataRowView dataViewRow = dataGridViewRow.DataBoundItem as DataRowView;

      if (dataViewRow != null)
      {
        if (((bool)dataViewRow.Row["isMatchedCol"] && dataGridViewRow.Cells[0].Style.ForeColor == Color.Red) ||
            (!(bool)dataViewRow.Row["isMatchedCol"] && (dataGridViewRow.Cells[0].Style.ForeColor == Color.Black || dataGridViewRow.Cells[0].Style.ForeColor == Color.Empty || dataGridViewRow.Cells[0].Style.ForeColor == DefaultForeColor)))
        {
          foreach (DataGridViewCell cell in dataGridViewRow.Cells)
          {
            cell.Style = new DataGridViewCellStyle();

            if (((bool)dataViewRow.Row["isMatchedCol"]))
            {
              cell.Style.ForeColor = Color.Black;
              cell.Style.SelectionForeColor = Color.Black;
            }
            else
            {
              cell.Style.ForeColor = Color.Red;
              cell.Style.SelectionForeColor = Color.Red;
            }
          }
        }
      }
    }

    private void messagesGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (matchModeButton.Checked)
      {
        messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
        IMessageCommand selectedMessage = GetSelectedMessageCommand();

        if (selectedMessage != null && selectedMessage.MatchingMessageCommand != null)
        {
          NodePanelHost.MessageMatchRequest(this, selectedMessage.MatchingMessageCommand);
        }
      }
      else
      {
        if (_autoSelectionChanging)
        {
          messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
        }
        else
        {
          if (messagesGrid.RowsDefaultCellStyle.SelectionBackColor == Color.Yellow)
          {
            IMessageCommand selectedMessage = GetSelectedMessageCommand();

            if (selectedMessage != null && selectedMessage.MatchingMessageCommand != null)
            {
              NodePanelHost.MatchSelectionCleared(this, selectedMessage.MatchingMessageCommand);
            }
          }
          messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Empty;
        }
      }
      
      RefreshCommandDetails();

      if (!_autoSelectionChanging)
      {
        NodePanelHost.MPIMessageSelected(this, SelectedCommand);
      }
    }

    private void messagesGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (matchModeButton.Checked)
      {
        messagesGrid.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
        IMessageCommand selectedMessage = GetSelectedMessageCommand();

        if (selectedMessage != null && selectedMessage.MatchingMessageCommand != null)
        {
          NodePanelHost.MessageMatchRequest(this, selectedMessage.MatchingMessageCommand);
        }
      }
    }

    private void messagesGrid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
    {
      if (e.RowIndex > -1)
      {
        DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
        DataRowView dataRow = row.DataBoundItem as DataRowView;

        var command = dataRow["objectColumn"] as MPICommand;

        e.ToolTipText = command == null ? string.Empty : command.Tooltip;
      }
      else
      {
        e.ToolTipText = string.Empty;
      }
    }

    private void getBufferToolStripMenuItem_Click(object sender, EventArgs e)
    {
      List<IMessageCommand> messages = new List<IMessageCommand>();
      foreach (DataGridViewRow row in messagesGrid.SelectedRows)
      {
        DataRowView dataRow = row.DataBoundItem as DataRowView;
        messages.Add(dataRow["objectColumn"] as IMessageCommand);
      }
      NodePanelHost.MessageBufferRequest(this, messages);
    }

    private void commandDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataRowView dataRow = messagesGrid.SelectedRows[0].DataBoundItem as DataRowView;
      NodePanelHost.MPINavigateRequest(this, dataRow["objectColumn"] as MPICommand);
    }

    private void hideButton_Click(object sender, EventArgs e)
    {
      messageDetailsSplitter.Panel2Collapsed = true;
      messageDetailsFooter.Visible = true;
    }

    private void detailsButton_Click(object sender, EventArgs e)
    {
      messageDetailsFooter.Visible = false;
      messageDetailsSplitter.Panel2Collapsed = false;
    }

    private IMessageCommand GetSelectedMessageCommand()
    {
      IMessageCommand selectedMessage = null;

      if (messagesGrid.SelectedRows.Count > 0)
      {
        DataRowView dataRowView = messagesGrid.SelectedRows[0].DataBoundItem as DataRowView;
        if (dataRowView != null)
        {
          selectedMessage = dataRowView.Row["objectColumn"] as IMessageCommand;
        }
      }

      return selectedMessage;
    }

    private void messageContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (messagesGrid.SelectedRows.Count == 0)
      {
        e.Cancel = true;
      }
      else
      {
        getBufferToolStripMenuItem.Enabled = SessionMode == Views.SessionMode.Record || SessionMode == Views.SessionMode.Replay;
        commandToolStripMenuItem.Enabled = messagesGrid.SelectedRows.Count == 1;
      }
    }

    private void SetFilter()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(hideMatchedButton.Checked ? "isMatchedCol = False" : string.Empty);

      List<ToolStripMenuItem> checkedMenuItems = filterDropDown.DropDownItems.OfType<ToolStripMenuItem>().Where(x => x.Checked).ToList();

      if (checkedMenuItems.Count > 0)
      {
        if (hideMatchedButton.Checked)
        {
          sb.Append(" AND (");
        }

        sb.Append(String.Format("typeColumn = '{0}'", checkedMenuItems.First().Text));

        foreach (var checkedMenuItem in checkedMenuItems.Skip(1))
        {
          sb.Append(String.Format(" OR typeColumn = '{0}'", checkedMenuItem.Text));
        }

        if (hideMatchedButton.Checked)
        {
          sb.Append(")");
        }
      }
      else
      {
        //We don't want anything, just make something up
        sb = new StringBuilder("typeColumn = 'XXXX'");
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
      foreach(var menuItem in filterDropDown.DropDownItems.OfType<ToolStripMenuItem>())
      {
        menuItem.Checked = false;
      }

      SetFilter();
    }

    private void messagesGrid_MouseDown(object sender, MouseEventArgs e)
    {
      RefreshCommandDetails();
      NodePanelHost.MPIMessageSelected(this, SelectedCommand);
    }
  }
}
