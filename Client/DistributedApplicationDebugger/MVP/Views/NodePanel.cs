using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DistributedApplicationDebugger.Communication.MPICommands;
using DistributedApplicationDebugger.Communication;
using SSHLib;

namespace DistributedApplicationDebugger.Views
{
  public partial class NodePanel : UserControl
  {
    public event EventHandler<EventArgs> Collapse;
    public event EventHandler<ValueEventArgs<IMessageCommand>> MessageMatched;
    private bool _replayMode = false;
    private SessionMode _sessionMode = SessionMode.Play;

    public NodePanel(int nodeId)
    {
      InitializeComponent();

      NodeId = nodeId;
      this.groupBox1.Text = string.Format("Node {0}", NodeId);

      List<Type> nonDraggableTypes = 
        new List<Type>() {typeof(ToolStripButton), typeof(ToolStrip), typeof(RadioButton) };

      foreach (Control control in nodePanelSplitter.Panel1.Controls.OfType<Control>()
        .Where(x=>!nonDraggableTypes.Contains(x.GetType())))
      {
        control.DragDrop += new DragEventHandler(Control_DragDrop);
        control.DragOver += new DragEventHandler(Control_DragOver);
        control.MouseDown += new MouseEventHandler(Control_MouseDown);
      }

      this.AllowDrop = true;
      this.Dock = DockStyle.Fill;
      this.DragDrop += new DragEventHandler(nodePanel_DragDrop);
      this.DragOver += new DragEventHandler(nodePanel_DragOver);
      this.MouseDown += new MouseEventHandler(nodePanel_MouseDown);

      messagesPanel.NodeId = NodeId;
      SessionMode = Views.SessionMode.Offline;
      DebugMode = false;

      debugFooter.Visible = true;
      debugSplitter.Panel2Collapsed = true;
      
      debugToolStrip.Renderer = new MySR();
      debugFooter.Renderer = new MySR();
    }

    public void SetIdInformation(int processId, string computerName)
    {
      nodeInfoLabel.Text = String.Format("Host: {0}  Process Id: {1}", computerName, processId);
    }

    public void DisableMatchMode()
    {
      messagesPanel.DisableMatchMode();
    }

    public void ClearSelectedMessage()
    {
      messagesPanel.ClearSelectedMessage();
    }

    public void Reset()
    {
      nodeInfoLabel.Text = string.Empty;
      this.consolePanel.Reset();
      this.messagesPanel.Reset();
      this.mpiPanel.Reset();
    }

    public void AddConsoleMessage(string consoleMessage)
    {
      consolePanel.AppendText(consoleMessage);
    }

    public void AddGdbMessage(string gdbMessage)
    {
      consolePanel.AppendGdbMessage(gdbMessage);
    }

    public void AddCommands(List<MPICommand> newCommands)
    {
      mpiPanel.BeginLoadData();
      messagesPanel.BeginLoadData();

      foreach (MPICommand mpiCommand in newCommands)
      {
        mpiPanel.AddCommand(mpiCommand);

        IMessageCommand messageCommand = mpiCommand as IMessageCommand;

        if (messageCommand != null)
        {
          messagesPanel.AddMessage(messageCommand);

          if (messageCommand.MatchingMessageCommand != null &&
              messageCommand.MatchingMessageCommand.DisplayState == MessageDisplayState.DisplayedUnMatched)
          {
            MessageMatched(this, new ValueEventArgs<IMessageCommand>(messageCommand.MatchingMessageCommand));
          }
        }
      }
      mpiPanel.UpdateMessageCount();

      messagesPanel.EndLoadData();
      mpiPanel.EndLoadData();
    }

    public void UpdateCommands(List<MPICommand> updatedCommands)
    {
      mpiPanel.BeginLoadData();
      messagesPanel.BeginLoadData();

      foreach (MPICommand mpiCommand in updatedCommands)
      {
        mpiPanel.UpdateCommand(mpiCommand.CommandId);

        if (mpiCommand.MatchingMessageCommand != null)
        {
          IMessageCommand messageCommand = mpiCommand as IMessageCommand;
          if (messageCommand.DisplayState == MessageDisplayState.DisplayedUnMatched)
          {
            UpdateMessage(messageCommand);

            if (messageCommand.MatchingMessageCommand.DisplayState == MessageDisplayState.DisplayedUnMatched)
            {
              MessageMatched(this, new ValueEventArgs<IMessageCommand>(messageCommand.MatchingMessageCommand));
            }
          }
        }
      }

      messagesPanel.EndLoadData();
      mpiPanel.EndLoadData();
    }

    public void UpdateCommandBuffers(Dictionary<int, List<BufferValue>> bufferUpdates)
    {
      mpiPanel.BeginLoadData();
      foreach (var bufferUpdate in bufferUpdates)
      {
        mpiPanel.UpdateBuffer(bufferUpdate.Key,bufferUpdate.Value);
      }
      
      mpiPanel.EndLoadData();

      NodePanelHost.MPIMessageSelected(this, SelectedMpiCommand);
      messagesPanel.RefreshCommandDetails();
      mpiPanel.RefreshCommandDetails();
      
    }

    public void UpdateMessage(IMessageCommand message)
    {
      messagesPanel.UpdateMessage(message);
    }

    public void MessageNavigate(IMessageCommand message)
    {
      nodeTabs.SelectedTab = messagesTab;
      messagesPanel.MessageNavigate(message);
    }

    public void MessageMatch(IMessageCommand message)
    {
      nodeTabs.SelectedTab = messagesTab;
      messagesPanel.MessageMatch(message);
    }

    public void MpiNavigate(MPICommand command)
    {
      nodeTabs.SelectedTab = mpiTab;
      this.mpiPanel.SelectCommand(command);
    }

    public SessionMode SessionMode
    {
      get
      {
        return _sessionMode;
      }
      set
      {
        _sessionMode = value;
        messagesPanel.SessionMode = _sessionMode;
        mpiPanel.SessionMode = _sessionMode;
      }
    }

    public bool ReplayMode
    {
      get
      {
        return _replayMode;
      }
      set
      {
        _replayMode = value;
        modeLabel.Visible = value;
        rdoNormal.Visible = value;
        rdoPlayback.Visible = value;

        if (value)
        {
          modeLabel.Enabled = value;
          rdoNormal.Enabled = value;
          rdoPlayback.Enabled = value;
        }
      }
    }

    public bool SessionRunning
    {
      set
      {
        enableGdbButton.Enabled = !value;
        disableGdbButton.Enabled = !value;
        stepButton.Enabled = value;
        nextButton.Enabled = value;
        continueButton.Enabled = value;
        commandLineLabel.Enabled = value;
        commandLineTextBox.Enabled = value;
        rdoNormal.Enabled = !value;
        rdoPlayback.Enabled = !value;
      }
    }

    public bool IsPlaybackNode
    {
      get
      {
        return rdoPlayback.Checked;
      }
    }

    public int NodeId
    {
      get;
      private set;
    }

    public string PanelText
    {
      get
      {
        return groupBox1.Text;
      }
    }

    public bool DebugMode
    {
      get;
      set;
    }

    private void Control_DragDrop(object sender, DragEventArgs e)
    {
      OnDragDrop(e);
    }

    private void Control_DragOver(object sender, DragEventArgs e)
    {
      OnDragOver(e);
    }

    private void Control_MouseDown(object sender, MouseEventArgs e)
    {
      OnMouseDown(e);
    }

    private void collapsePanelButton_Click(object sender, EventArgs e)
    {
      if (Collapse != null)
      {
        Collapse(this, new EventArgs());
      }
    }

    private void nodePanel_MouseDown(object sender, MouseEventArgs e)
    {
      (sender as NodePanel).DoDragDrop(sender, DragDropEffects.Move);
    }

    private void nodePanel_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Move;
    }

    private void nodePanel_DragDrop(object sender, DragEventArgs e)
    {
      NodePanel incomingNodePanel = e.Data.GetData(e.Data.GetFormats()[0]) as NodePanel;

      if (incomingNodePanel != null)
      {
        NodePanel currentNodePanel = sender as NodePanel;
        if (currentNodePanel != null)
        {
          //switch parents
          Panel incomingParent = incomingNodePanel.Parent as Panel;
          Panel currentParent = currentNodePanel.Parent as Panel;

          incomingParent.Controls.Remove(incomingNodePanel);
          currentParent.Controls.Remove(currentNodePanel);

          incomingParent.Controls.Add(currentNodePanel);
          currentParent.Controls.Add(incomingNodePanel);
        }
      }
    }

    private void nodeTabs_SelectedIndexChanged(object sender, EventArgs e)
    {
      NodePanelHost.MPIMessageSelected(this, SelectedMpiCommand);
    }

    private MPICommand SelectedMpiCommand
    {
      get
      {
        if (nodeTabs.SelectedTab == messagesTab)
        {
          return messagesPanel.SelectedCommand;
        }
        if (nodeTabs.SelectedTab == mpiTab)
        {
          return mpiPanel.SelectedCommand;
        }

        return null;
      }
    }

    private void enableGdbButton_Click(object sender, EventArgs e)
    {
      debugFooter.Visible = false;
      debugSplitter.Panel2Collapsed = false;
      DebugMode = true;
      consolePanel.GdbEnabled = DebugMode;
    }

    private void disableGdbButton_Click(object sender, EventArgs e)
    {
      debugSplitter.Panel2Collapsed = true;
      debugFooter.Visible = true;
      DebugMode = false;
      consolePanel.GdbEnabled = DebugMode;
    }

    private void stepButton_Click(object sender, EventArgs e)
    {
      NodePanelHost.GDBCommandIssued(this, NodeId, "s\n");
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      NodePanelHost.GDBCommandIssued(this, NodeId, "n\n");
    }

    private void continueButton_Click(object sender, EventArgs e)
    {
      NodePanelHost.GDBCommandIssued(this, NodeId, "c\n");
    }

    private void commandLineTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        NodePanelHost.GDBCommandIssued(this, NodeId, string.Format("{0}\n", commandLineTextBox.Text));
        commandLineTextBox.Clear();
        e.SuppressKeyPress = true;
        e.Handled = true;
      }
    }
  }
}