using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DistributedApplicationDebugger.Communication;
using DistributedApplicationDebugger.Communication.MPICommands;
using DistributedApplicationDebugger.Extensions;
using DistributedApplicationDebugger.Properties;

namespace DistributedApplicationDebugger.Views
{
  public enum SessionMode
  {
    Offline,
    Play,
    Record,
    Replay
  }

  public partial class NodePanelHost : UserControl
  {
    private Dictionary<int, NodePanel> _nodeDictionary = new Dictionary<int, NodePanel>();
    private Dictionary<Panel, Splitter> _splitterDictionary = new Dictionary<Panel, Splitter>();
    private int _nodeCount = 0;
    private List<Byte> _inputBuffer = new List<Byte>();
    private List<string> _processedMessages = new List<string>();
    private SessionMode _sessionMode = SessionMode.Play;
    private MPICommand _displayedCommand = null;

    private bool _replayMode = false;
    private bool _sessionRunning = false;
    private bool _sessionIdle = false;
    private bool _displayBufferGrid = false;

    private EventWaitHandle _incomingDataThreadAlert = new AutoResetEvent(false);
    private EventWaitHandle _processedMessagesThreadAlert = new AutoResetEvent(false);

    public event EventHandler<ValueEventArgs<string>> ExecutableFolderReceived;
    public event EventHandler<ValueEventArgs<string>> FileNotFound;
    public event EventHandler<ValueEventArgs<string>> HistoryListReceived;
    public event EventHandler<ValueEventArgs<int>> FinalizedProcessed;
    public event EventHandler<ValueEventArgs<string>> RecordSessionNameReceived;
    public event EventHandler<ValueEventArgs<string>> RecordSessionInfoReceived;
    public event EventHandler<GdbRequestEventArgs> GdbRequestReceived;

    public event EventHandler<ValueEventArgs<MPICommand>> MessageSelected;

    public event EventHandler<ValueEventArgs<IEnumerable<IMessageCommand>>> MessageBufferRequested;

    private Dictionary<int, List<MPICommand>> _incompleteCommands = new Dictionary<int, List<MPICommand>>();
    private int _messageIdCounter = 0;
    private Stopwatch _sessionTimer = new Stopwatch();

    private Dictionary<int, List<MPI_IRecv>> _waitingIRecvs = new Dictionary<int, List<MPI_IRecv>>();
    private Dictionary<int, List<IMessageCommand>> _unmatchedSendMessages = new Dictionary<int, List<IMessageCommand>>();
    private Dictionary<int, List<IMessageCommand>> _unmatchedReceiveMessages = new Dictionary<int, List<IMessageCommand>>();
    
    private static event EventHandler<ValueEventArgs<int>> MatchModeEnabledEvent;
    private static event EventHandler<ValueEventArgs<IMessageCommand>> MessageNavigateRequestEvent;
    private static event EventHandler<ValueEventArgs<IMessageCommand>> MessageMatchRequestEvent;
    private static event EventHandler<ValueEventArgs<IMessageCommand>> MatchSelectionClearedEvent;
    private static event EventHandler<ValueEventArgs<IEnumerable<IMessageCommand>>> BufferRequestEvent;
    private static event EventHandler<ValueEventArgs<MPICommand>> MPINavigateRequestEvent;
    private static event EventHandler<ValueEventArgs<MPICommand>> MessageSelectedEvent;
    private static event EventHandler<GdbRequestEventArgs> GdbRequestEvent;

    private IMessageCommand _lastRequestedMatchMessage = null;

    public NodePanelHost()
    {
      InitializeComponent();
      hiddenPanelsStrip.Visible = false;
      MatchModeEnabledEvent += new EventHandler<ValueEventArgs<int>>(nodePanel_MatchModeEnabled);
      MessageNavigateRequestEvent += new EventHandler<ValueEventArgs<IMessageCommand>>(nodePanel_MessageNavigateRequest);
      MessageMatchRequestEvent += new EventHandler<ValueEventArgs<IMessageCommand>>(nodePanel_MessageMatchRequest);
      MatchSelectionClearedEvent += new EventHandler<ValueEventArgs<IMessageCommand>>(nodePanel_MatchSelectionCleared);
      BufferRequestEvent += new EventHandler<ValueEventArgs<IEnumerable<IMessageCommand>>>(nodePanel_BufferRequested);
      MPINavigateRequestEvent += new EventHandler<ValueEventArgs<MPICommand>>(nodePanel_MPINavigateRequested);
      MessageSelectedEvent += new EventHandler<ValueEventArgs<MPICommand>>(nodePanel_MPICommandSelected);
      GdbRequestEvent += new EventHandler<GdbRequestEventArgs>(nodePanel_GdbCommandRequested);

      SetBufferToolStripState(null);

      runningTimeToolStrip.Renderer = new MySR();
      bufferDetailsToolStrip1.Renderer = new MySR();
      bufferDetailsToolStrip2.Renderer = new MySR();

      IndexColumn.DataPropertyName = "Index";

      nodesPanel.HorizontalScroll.Value = 0;
    }

    public static void MatchModeEnabled(object sender, int nodeId)
    {
      MatchModeEnabledEvent(sender, new ValueEventArgs<int>(nodeId));
    }

    public static void MessageNavigateRequest(object sender, IMessageCommand messageCommand)
    {
      MessageNavigateRequestEvent(sender, new ValueEventArgs<IMessageCommand>(messageCommand));
    }

    public static void MessageMatchRequest(object sender, IMessageCommand messageCommand)
    {

      MessageMatchRequestEvent(sender, new ValueEventArgs<IMessageCommand>(messageCommand));
    }

    public static void MatchSelectionCleared(object sender, IMessageCommand messageCommand)
    {
      MatchSelectionClearedEvent(sender, new ValueEventArgs<IMessageCommand>(messageCommand));
    }

    public static void MessageBufferRequest(object sender, IEnumerable<IMessageCommand> messageCommands)
    {
      BufferRequestEvent(sender, new ValueEventArgs<IEnumerable<IMessageCommand>>(messageCommands));
    }

    public static void MPINavigateRequest(object sender, MPICommand messageCommand)
    {
      MPINavigateRequestEvent(sender, new ValueEventArgs<MPICommand>(messageCommand));
    }

    public static void MPIMessageSelected(object sender, MPICommand messageCommand)
    {
      MessageSelectedEvent(sender, new ValueEventArgs<MPICommand>(messageCommand));
    }

    public static void GDBCommandIssued(object sender,int nodeId, string gdbMessage)
    {
      GdbRequestEvent(sender, new GdbRequestEventArgs(nodeId, gdbMessage));
    }

    public int NodeCount
    {
      get { return _nodeCount; }
      set
      {
        _nodeCount = value;
        RefreshPanels();
      }
    }

    public bool ReplayMode
    {
      get { return _replayMode; }
      set
      {
        _replayMode = value;
        foreach (var nodePanel in _nodeDictionary.Values)
        {
          nodePanel.ReplayMode = value;
        }
      }
    }

    public bool SessionIdle
    {
      get { return _sessionIdle; }
      set
      {
        if (_sessionIdle != value)
        {
          _sessionIdle = value;
          if (!_sessionIdle)
          {
            new Thread(ProcessMessages).Start();
            new Thread(ProcessIncomingData).Start();
          }
          else
          {
            _incomingDataThreadAlert.Set();
            _processedMessagesThreadAlert.Set();
            runningTimeLabel.Visible = false;
            runningTimeValueLabel.Visible = false;
          }
        }
      }
    }

    public bool SessionRunning
    {
      get { return _sessionRunning; }
      set
      {
        _sessionRunning = value;

        foreach (var nodePanel in _nodeDictionary.Values)
        {
          nodePanel.SessionRunning = value;
        }

        if (_sessionRunning)
        {
          runningTimeLabel.Visible = true;
          runningTimeValueLabel.Visible = true;
          _sessionTimer.Start();
          new Thread(SessionTimeThread).Start();
        }
        else
        {
          _sessionTimer.Stop();
        }
      }
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
        foreach (var nodePanel in _nodeDictionary.Values)
        {
          nodePanel.SessionMode = _sessionMode;
        }

      }
    }

    private void SessionTimeThread()
    {
      while (_sessionTimer.IsRunning)
      {
        UpdateSessionTime();
        Thread.Sleep(100);
      }

      UpdateSessionTime();
    }

    private void UpdateSessionTime()
    {
      if (InvokeRequired)
      {
        Invoke(new Action(UpdateSessionTime));
        return;
      }

      runningTimeValueLabel.Text = _sessionTimer.Elapsed.ToReadableString();
    }

    public IList<int> PlaybackNodes
    {
      get
      {
        return _nodeDictionary.Values.Where(x => x.IsPlaybackNode).Select(x => x.NodeId).ToList();
      }
    }

    public IList<int> GDBNodes
    {
      get
      {
        return _nodeDictionary.Values.Where(x => x.DebugMode).Select(x=>x.NodeId).ToList();
      }
    }

    public void Reset()
    {
      _sessionTimer.Reset();
      runningTimeValueLabel.Text = string.Empty;
      _inputBuffer.Clear();
      foreach (var nodePanel in _nodeDictionary.Values)
      {
        nodePanel.Reset();
      }
      _messageIdCounter = 0;

      _incompleteCommands.Clear();

      _waitingIRecvs.Clear();
      _unmatchedSendMessages.Clear();
      _unmatchedReceiveMessages.Clear();
    }

    public void IncomingData(object sender, ValueEventArgs<string> e)
    {
      lock (_inputBuffer)
      {
        _inputBuffer.AddRange(Encoding.ASCII.GetBytes(e.Value));
        _incomingDataThreadAlert.Set();
      }
    }

    public string SOHReplacement
    {
        get;
        set;
    }

    public string BARReplacement
    {
      get;
      set;
    }

    public string EOTReplacement
    {
      get;
      set;
    }

    public bool DisplayBufferGrid
    {
      get { return _displayBufferGrid; }
      set
      {
        _displayBufferGrid = value;
        nodesGridSplitter.Panel2Collapsed = !_displayBufferGrid;
      }
    }

    private void RefreshPanels()
    {
      nodesPanel.SuspendLayout();

      if (_nodeCount > _nodeDictionary.Count)
      {
        while (_nodeCount > _nodeDictionary.Count)
          AddNodePanel();
      }
      else
      {
        while (_nodeCount < _nodeDictionary.Count)
          RemoveNodePanel();
      }
      nodesPanel.ResumeLayout();
    }

    public void ResetHorizonatalScroll()
    {
      nodesPanel.HorizontalScroll.Value = 0;
      nodesPanel.AutoScroll = true;
      
    }

    private NodePanel GetNodePanel(int nodeId)
    {
      if (_nodeDictionary.ContainsKey(nodeId))
      {
        return _nodeDictionary[nodeId];
      }

      return null;
    }

    private void AddNodePanel()
    {
      Splitter splitter = new Splitter();
      splitter.Width = 3;
      nodesPanel.Controls.Add(splitter);

      int nodeId = _nodeDictionary.Count;

      NodePanel nodePanel = new NodePanel(nodeId);
      nodePanel.SessionMode = SessionMode;
      nodePanel.Collapse += new EventHandler<EventArgs>(nodePanel_Collapse);
      nodePanel.MessageMatched += new EventHandler<ValueEventArgs<IMessageCommand>>(nodePanel_MessageMatched);
      
      Panel panel = new Panel();
      panel.Size = new Size(nodePanel.Width, nodesPanel.Height);
      panel.Location = new Point(nodeId * (panel.Width + splitter.Width), 0);
      panel.TabIndex = 0;
      panel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
      panel.Dock = DockStyle.Left;
      panel.Controls.Add(nodePanel);
      panel.AllowDrop = true;

      nodesPanel.Controls.Add(panel);
      nodesPanel.Controls.SetChildIndex(panel, 0);
      nodesPanel.Controls.SetChildIndex(splitter, 1);

      _nodeDictionary.Add(nodeId, nodePanel);
      _splitterDictionary.Add(panel, splitter);

    }

    private void RemoveNodePanel()
    {
      int nodeId = _nodeDictionary.Count - 1;


      Panel parentPanel = _nodeDictionary[nodeId].Parent as Panel;
      NodePanel nodePanel = _nodeDictionary[nodeId];
      parentPanel.Controls.Remove(_nodeDictionary[nodeId]);
      nodePanel.Collapse -= new EventHandler<EventArgs>(nodePanel_Collapse);
      nodePanel.MessageMatched -= new EventHandler<ValueEventArgs<IMessageCommand>>(nodePanel_MessageMatched);

      if (parentPanel.Visible == false)
      {
        ToolStripButton hidePanelButton =
          hiddenPanelsStrip.Items.OfType<ToolStripButton>().FirstOrDefault(x => x.Tag == parentPanel);

        if (hidePanelButton != null)
        {
          hidePanelButton.Click -= new EventHandler(hidePanelButton_Click);
          hiddenPanelsStrip.Items.Remove(hidePanelButton);
        }
      }

      nodesPanel.Controls.Remove(_splitterDictionary[parentPanel]);
      _splitterDictionary.Remove(parentPanel);

      nodesPanel.Controls.Remove(parentPanel);
      _nodeDictionary.Remove(nodeId);
    }

    private void nodePanel_MatchModeEnabled(object sender, ValueEventArgs<int> e)
    {
      foreach (var nodePanel in _nodeDictionary.Values.Where(x => x.NodeId != e.Value))
      {
        nodePanel.DisableMatchMode();
      }
    }

    private void nodePanel_MessageNavigateRequest(object sender, ValueEventArgs<IMessageCommand> e)
    {
      GetNodePanel(e.Value.NodeId).MessageNavigate(e.Value);
    }

    private void nodePanel_MessageMatchRequest(object sender, ValueEventArgs<IMessageCommand> e)
    {
      if (_lastRequestedMatchMessage != null &&
         e.Value.NodeId != _lastRequestedMatchMessage.NodeId)
      {
        GetNodePanel(_lastRequestedMatchMessage.NodeId).ClearSelectedMessage();
      }

      GetNodePanel(e.Value.NodeId).MessageMatch(e.Value);
      _lastRequestedMatchMessage = e.Value;
    }

    private void nodePanel_MessageMatched(object sender, ValueEventArgs<IMessageCommand> e)
    {
      GetNodePanel(e.Value.NodeId).UpdateMessage(e.Value);
    }

    private void nodePanel_MatchSelectionCleared(object sender, ValueEventArgs<IMessageCommand> e)
    {
      GetNodePanel(e.Value.NodeId).ClearSelectedMessage();
      if (_lastRequestedMatchMessage != null && _lastRequestedMatchMessage.MatchingMessageCommand != null)
      {
        GetNodePanel(_lastRequestedMatchMessage.MatchingMessageCommand.NodeId).ClearSelectedMessage();  
      }
    }

    private void nodePanel_BufferRequested(object sender, ValueEventArgs<IEnumerable<IMessageCommand>> e)
    {
      MessageBufferRequested(sender, e);
    }

    private void nodePanel_MPINavigateRequested(object sender, ValueEventArgs<MPICommand> e)
    {
      GetNodePanel(e.Value.NodeId).MpiNavigate(e.Value);
    }

    private void nodePanel_MPICommandSelected(object sender, ValueEventArgs<MPICommand> e)
    {
      SetBufferToolStripState(e.Value);
    }

    private void nodePanel_GdbCommandRequested(object sender, GdbRequestEventArgs e)
    {
      GdbRequestReceived(sender, e);
    }

    private void SetBufferToolStripState(MPICommand command)
    {
      if (_displayedCommand != command || bufferDetailsGrid.Rows.Count == 0)
      {
        if (InvokeRequired)
        {
          Invoke(new Action<MPICommand>(SetBufferToolStripState), command);
          return;
        }

        _displayedCommand = command;
        if (command == null)
        {
          commandTypeLabel.Visible = false;
          commandTypeLabel.Text = string.Empty;

          commandIdLabel.Visible = false;
          commandIdLabel.Text = string.Empty;

          dataTypeLabel.Visible = false;
          dataTypeLabel.Text = string.Empty;

          bufferDetailsGrid.DataSource = null;
          HexColumn.Visible = false;
        }
        else
        {
          commandTypeLabel.Visible = true;
          commandTypeLabel.Text = command.Name;

          commandIdLabel.Visible = true;
          commandIdLabel.Text = string.Format("{0}.{1}", command.NodeId, command.CommandId);

          IMessageCommand messageCommand = command as IMessageCommand;

          if (messageCommand == null)
          {
            dataTypeLabel.Visible = false;
            dataTypeLabel.Text = string.Empty;

            bufferDetailsGrid.DataSource = null;
            HexColumn.Visible = false;
          }
          else
          {
            dataTypeLabel.Visible = true;
            dataTypeLabel.Text = messageCommand.DataType;

            bufferDetailsGrid.DataSource = messageCommand.Buf;
            HexColumn.Visible = false;
          }
        }
      }
    }

    private void nodePanel_Collapse(object sender, EventArgs e)
    {
      nodesPanel.SuspendLayout();
      NodePanel nodePanel = sender as NodePanel;
      Panel panel = nodePanel.Parent as Panel;

      ToolStripButton hidePanelButton = new ToolStripButton(nodePanel.PanelText, Resources.Clipboard);
      hidePanelButton.Tag = panel;
      hidePanelButton.Click += new EventHandler(hidePanelButton_Click);

      panel.Visible = false;
      _splitterDictionary[panel].Visible = false;

      hiddenPanelsStrip.Items.Add(hidePanelButton);
      hiddenPanelsStrip.Visible = true;
      nodesPanel.ResumeLayout();
    }

    private void hidePanelButton_Click(object sender, EventArgs e)
    {
      ToolStripButton hidePanelButton = sender as ToolStripButton;
      hidePanelButton.Click -= new EventHandler(hidePanelButton_Click);

      Panel panel = hidePanelButton.Tag as Panel;
      panel.Visible = true;
      _splitterDictionary[panel].Visible = true;

      hiddenPanelsStrip.Items.Remove(hidePanelButton);

      hiddenPanelsStrip.Visible = hiddenPanelsStrip.Items.Count > 0;
    }

    private void ProcessIncomingData()
    {
      List<string> messages = new List<string>();
      while (!SessionIdle)
      {
        lock (_inputBuffer)
        {
          messages = ParseInputBuffer(_inputBuffer);
        }

        if (messages.Any())
        {
          lock (_processedMessages)
          {
            _processedMessages.AddRange(messages.ToArray());
            messages.Clear();
          }

          _processedMessagesThreadAlert.Set();
        }

        _incomingDataThreadAlert.WaitOne();
      }
    }

    private void ProcessMessages()
    {
      List<string> messages = new List<string>();
      while (!SessionIdle)
      {
        messages.Clear();
        lock (_processedMessages)
        {
          messages = _processedMessages.ToList();
          _processedMessages.Clear();
        }

        if (messages.Any())
        {
          List<NodeUpdateData> nodeUpdates = new List<NodeUpdateData>();
          int finalizedCount = 0;
          int counter = 0;

          foreach (string message in messages)
          {
            if (counter > 500)
            {
              Invoke(new Action<List<NodeUpdateData>>(UpdateNodePanels), nodeUpdates);
              nodeUpdates.Clear();
              counter = 0;
            }
            string[] messageSplit = message.Split(Protocal.PARTITION);

            if (messageSplit.Length < 2)
              continue;

            switch (messageSplit[0])
            {
              //This is the name of the computer and process actually running the node
              //EX: Node 2, Cortex.cs.unlv.edu, Process 1122
              case Protocal.NODE_ID_HEADER:
                {
                  //Append the text of this message to the update data for this node
                  int id = Int32.Parse(messageSplit[1]);
                  var panel = GetNodePanel(id);
                  Invoke(new Action<int, string>(panel.SetIdInformation), Int32.Parse(messageSplit[2]), messageSplit[3]);
                }
                break;
              case Protocal.BUFFER_VALUE_HEADER:
                {
                  int nodeId = Int32.Parse(messageSplit[1]);
                  int commandId = Int32.Parse(messageSplit[2]);

                  //command buffer id is the 4th one, not necessary to use
                  List<BufferValue> bufferList = new List<BufferValue>();
                  int indexCounter = 0;
                  //Check if any buffer values had to be encoded
                  if (messageSplit[3] == "E")
                  {
                      //Values were encoded so decode here
                      messageSplit.Skip(4).ToList().ForEach(x => bufferList.Add(
                          new BufferValue(indexCounter++,
                              x.Replace(SOHReplacement, Protocal.SOH.ToString())
                               .Replace(BARReplacement, Protocal.PARTITION.ToString())
                               .Replace(EOTReplacement, Protocal.EOT.ToString()))));
                  }
                  else
                  {
                      //Nothing was encoded so just add them as they are.
                      messageSplit.Skip(4).ToList().ForEach(x => bufferList.Add(
                          new BufferValue(indexCounter++, x)));
                  }

                  NodeUpdateData updateData = GetNodeUpdateData(nodeUpdates, nodeId);
                  updateData.BufferUpdates.Add(commandId, bufferList);
                }
                break;

              case Protocal.CONSOLE_HEADER:
                {
                    //Append the text of this message to the update data for this node
                    int id = Int32.Parse(messageSplit[1]);
                    NodeUpdateData updateData = GetNodeUpdateData(nodeUpdates, id);                 

                    //E indicates that it was encoded
                    if (messageSplit[2] == "E")
                    {
                        messageSplit[3] = messageSplit[3]
                                            .Replace(SOHReplacement, Protocal.SOH.ToString())
                                            .Replace(BARReplacement, Protocal.PARTITION.ToString())
                                            .Replace(EOTReplacement, Protocal.EOT.ToString());
                    }

                    updateData.StdOutConsole.Append(messageSplit[3]);
                }
                break;
              case Protocal.GDB_HEADER:
                {
                  //Append the text of this message to the update data for this node
                  int id = Int32.Parse(messageSplit[1]);
                  NodeUpdateData updateData = GetNodeUpdateData(nodeUpdates, id);

                  //E indicates that it was encoded
                  if (messageSplit[2] == "E")
                  {
                      messageSplit[3] = messageSplit[3]
                                          .Replace(SOHReplacement, Protocal.SOH.ToString())
                                          .Replace(BARReplacement, Protocal.PARTITION.ToString())
                                          .Replace(EOTReplacement, Protocal.EOT.ToString());
                  }

                  updateData.GdbConsole.Append(messageSplit[3]);
                }
                break;
              case Protocal.PRE_COMMAND_HEADER:
                {
                  MPICommand mpiPreCommand = MPICommand.Deserialize(message);

                  NodeUpdateData updateData = GetNodeUpdateData(nodeUpdates, mpiPreCommand.NodeId);
                  updateData.NewMpiCommands.Add(mpiPreCommand);
                  AddIncompleteCommand(mpiPreCommand);
                  
                  IMessageCommand messageCommand = mpiPreCommand as IMessageCommand;
                  if (messageCommand != null)
                  {
                    MatchMessage(messageCommand);

                    MPI_IRecv irecvCommand = messageCommand as MPI_IRecv;
                    if (irecvCommand != null)
                    {
                      AddWaitingIRecv(irecvCommand);
                    }
                  }
                }
                break;

              case Protocal.POST_COMMAND_HEADER:
                {
                  int nodeId = Int32.Parse(messageSplit[1]);
                  int commandId = Int32.Parse(messageSplit[2]);

                  //See if we have an incomplete node for this commandId
                  MPICommand mpiPostCommand = _incompleteCommands[nodeId].FirstOrDefault(x => x.CommandId == commandId);
                  if (mpiPostCommand != null)
                  {
                    //Request the mpi command to process its complete information
                    mpiPostCommand.ParseReturnValues(messageSplit.Skip(3).ToArray());

                    //Remove it from our list since its no longer incomplete
                    _incompleteCommands[nodeId].Remove(mpiPostCommand);

                    //get the update data for this node
                    NodeUpdateData updateData = GetNodeUpdateData(nodeUpdates, mpiPostCommand.NodeId);

                    //If we don't have this one in the 'new' list add it to the 'updated' list for this update
                    if (!updateData.NewMpiCommands.Any(x => x.CommandId == mpiPostCommand.CommandId))
                    {
                      updateData.UpdatedMpiCommands.Add(mpiPostCommand);
                    }

                    //Check if this is a message command
                    IMessageCommand messageCommand = mpiPostCommand as IMessageCommand;
                    if (messageCommand != null)
                    {
                      //Check if it needs to be matched up
                      if (mpiPostCommand.MatchingMessageCommand == null)
                      {
                        MatchMessage(messageCommand);
                      }
                    }
                    else
                    {
                      //Check if its a wait command
                      MPI_Wait waitCommand = mpiPostCommand as MPI_Wait;

                      if (waitCommand != null && _waitingIRecvs.ContainsKey(waitCommand.NodeId))
                      {
                        //Match the wait command
                        MPI_IRecv recvCommand = _waitingIRecvs[waitCommand.NodeId].FirstOrDefault(x => x.Request == waitCommand.Request);
                        if (recvCommand != null)
                        {
                          recvCommand.UpdateStatus(waitCommand);
                          _waitingIRecvs[waitCommand.NodeId].Remove(recvCommand);

                          //Now that this irecv command has been completed with its wait command match it up
                          MatchMessage(recvCommand);

                          //Add the receive to the list of updated commands to update if it is not already in a processing list
                          if (!updateData.NewMpiCommands.Any(x => x == recvCommand) && !updateData.UpdatedMpiCommands.Any(x => x == recvCommand))
                          {
                            updateData.UpdatedMpiCommands.Add(recvCommand);
                          }
                        }
                      }
                      else if (mpiPostCommand is MPI_Finalize)
                      {
                        //Its a finalize command, increase our count
                        finalizedCount++;
                      }
                    }
                  }
                }
                break;
              case Protocal.FILE_NOT_FOUND_HEADER:
                {
                  //Raise the 'File Not Found' event
                  FileNotFound(this, new ValueEventArgs<string>(messageSplit[1]));
                }
                break;

              case Protocal.ENVIRONMENT_DATA_HEADER:
                {
                  //Raise the 'Executable Folder Received' event
                  Invoke(new Action(() => ExecutableFolderReceived(this, new ValueEventArgs<string>(messageSplit[1]))));

                  //Raise the 'History List Received' event
                  HistoryListReceived(this, new ValueEventArgs<string>(messageSplit[2]));
                }
                break;
              case Protocal.RECORD_SESSION_HEADER:
                {
                  RecordSessionNameReceived(this, new ValueEventArgs<string>(messageSplit[1]));
                  RecordSessionInfoReceived(this, new ValueEventArgs<string>(messageSplit[2]));
                }
                break;
            }
            counter++;
          }

          Invoke(new Action<List<NodeUpdateData>>(UpdateNodePanels), nodeUpdates);

          if (finalizedCount > 0 && FinalizedProcessed != null)
            FinalizedProcessed(this, new ValueEventArgs<int>(finalizedCount));
        }

        _processedMessagesThreadAlert.WaitOne();
      }
    }

    private void MatchMessage(IMessageCommand unmatchedMessage)
    {
      IMessageCommand companionMessage = null;
      Dictionary<int, List<IMessageCommand>> hostMessages = null;
      Dictionary<int, List<IMessageCommand>> companionMessages = null;

      if (unmatchedMessage.Dest.HasValue)
      {
        //Its a send command
        hostMessages = _unmatchedSendMessages;
        companionMessages = _unmatchedReceiveMessages;
      }
      else
      {
        //Its a receive command
        hostMessages = _unmatchedReceiveMessages;
        companionMessages = _unmatchedSendMessages;
      }

      //Make sure we have a receive list for this destination
      if (companionMessages.Keys.Contains(unmatchedMessage.MessageLinkId))
      {
        //See if there is an unmatched Receive with the same source and tag
        //companionMessage = companionMessages[unmatchedMessage.MessageLinkId].FirstOrDefault(
        //  x => x.MessageLinkId == unmatchedMessage.MessageLinkId && x.Tag == unmatchedMessage.Tag);
        companionMessage = companionMessages[unmatchedMessage.MessageLinkId].FirstOrDefault(
          x => x.IsMatch(unmatchedMessage));
      }

      if (companionMessage != null)
      {
        //We found the matching recieve, set both to have the same message id
        unmatchedMessage.MessageId = _messageIdCounter;
        companionMessage.MessageId = _messageIdCounter;
        _messageIdCounter++;

        //Allow both to know the node which matches
        unmatchedMessage.MatchingMessageCommand = companionMessage;
        companionMessage.MatchingMessageCommand = unmatchedMessage;

        //remove the unmatched receive from the list so we don't look at it again
        companionMessages[unmatchedMessage.MessageLinkId].Remove(companionMessage);
        
        //Also remove this message if it was pending in the host list
        if (hostMessages.ContainsKey(unmatchedMessage.NodeId))
        {
          hostMessages[unmatchedMessage.NodeId].Remove(unmatchedMessage);
        }
      }
      else
      {
        //No matching message

        //Make sure there is a send list for this node
        if (!hostMessages.Keys.Contains(unmatchedMessage.NodeId))
        {
          hostMessages.Add(unmatchedMessage.NodeId, new List<IMessageCommand>());
        }

        if (!hostMessages[unmatchedMessage.NodeId].Contains(unmatchedMessage))
        {
          //Add this unmatched message to the list 
          hostMessages[unmatchedMessage.NodeId].Add(unmatchedMessage);
        }
      }
    }

    private void AddIncompleteCommand(MPICommand incompleteCommand)
    {
      if (!_incompleteCommands.Keys.Contains(incompleteCommand.NodeId))
        _incompleteCommands.Add(incompleteCommand.NodeId, new List<MPICommand>());

      _incompleteCommands[incompleteCommand.NodeId].Add(incompleteCommand);
    }

    private void AddWaitingIRecv(MPI_IRecv irecvCommand)
    {
      if (!_waitingIRecvs.Keys.Contains(irecvCommand.NodeId))
        _waitingIRecvs.Add(irecvCommand.NodeId, new List<MPI_IRecv>());

      _waitingIRecvs[irecvCommand.NodeId].Add(irecvCommand);
    }

    private void UpdateNodePanels(List<NodeUpdateData> updateData)
    {
      foreach (var update in updateData)
      {
        var panel = GetNodePanel(update.Id);
        if (update.StdOutConsole.Length > 0)
        {
          panel.AddConsoleMessage(update.StdOutConsole.ToString());
        }

        if (update.GdbConsole.Length > 0)
        {
          panel.AddGdbMessage(update.GdbConsole.ToString());
        }

        if (update.NewMpiCommands.Any())
        {
          panel.AddCommands(update.NewMpiCommands);
        }

        if (update.UpdatedMpiCommands.Any())
        {
          panel.UpdateCommands(update.UpdatedMpiCommands);
        }

        if (update.BufferUpdates.Any())
        {
          panel.UpdateCommandBuffers(update.BufferUpdates);
        }
      }
    }

    private static List<string> ParseInputBuffer(List<Byte> buffer)
    {
      int startIndex, endIndex;
      List<string> receivedMessages = new List<string>();

      //Get the index of the first SOH
      startIndex = buffer.IndexOf((byte)Protocal.SOH);

      if (startIndex > -1)
      {
        //Remove everything up until the SOH, anything before it is invalid
        buffer.RemoveRange(0, startIndex);
        //Get the index of the EOT
        endIndex = buffer.IndexOf((byte)Protocal.EOT);

        //Process message if an SOH and and EOT was found
        while (startIndex > -1 && endIndex > -1)
        {
          //Add the message to the list
          //receivedMessages.Add(Encoding.ASCII.GetString(buffer.ToArray(), 1, endIndex - 2));
          receivedMessages.Add(Encoding.ASCII.GetString(buffer.ToArray(), 1, endIndex - 1));
          //Remove everything up through the endIndex
          buffer.RemoveRange(0, endIndex);

          //Get the new SOH
          startIndex = buffer.IndexOf((byte)Protocal.SOH);

          if (startIndex > 0)
          {
            //Remove everything before the next SOH, it is useless
            buffer.RemoveRange(0, startIndex);
            //Get the next EOT
            endIndex = buffer.IndexOf((byte)Protocal.EOT);
          }
        }
      }

      return receivedMessages;
    }

    private static NodeUpdateData GetNodeUpdateData(List<NodeUpdateData> list, int nodeId)
    {
      NodeUpdateData updateData = list.FirstOrDefault(x => x.Id == nodeId);
      if (updateData == null)
      {
        updateData = new NodeUpdateData(nodeId);
        list.Add(updateData);
      }

      return updateData;
    }

    private void bufferDetailsGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {
      if (e.Column.DataPropertyName == "HexValue")
      {
        IMessageCommand messageCommand = _displayedCommand as IMessageCommand;
        e.Column.Visible = messageCommand != null && 
            (messageCommand.DataType == "MPI_CHAR" || messageCommand.DataType == "MPI_BYTE" || messageCommand.DataType == "MPI_UNSIGNED_CHAR");
      }

    }
  }

  public class MySR : ToolStripSystemRenderer
  {
    public MySR() { }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
      //base.OnRenderToolStripBorder(e);
    }
  }
}


