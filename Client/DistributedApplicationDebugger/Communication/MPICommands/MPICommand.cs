using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public abstract class MPICommand
  {
    public const int MPI_ANY_TAG = -1;
    public const int MPI_ANY_SOURCE = -2;
    public const int MPI_SUCCESS = 0;

    public static int _deserializedCommands = 0;

    private static object commandCounterSync = new object();
    private static int _commandCounter = 0;
    private static Random _randomNumGen = new Random();

    protected MPICommand(int nodeId)
    {
      NodeId = nodeId;

      lock (commandCounterSync)
      {
        CommandId = _commandCounter;
        _commandCounter++;
      }

      LineNum = _randomNumGen.Next(1, 1000);
      ReturnValue = 0;
    }

    protected MPICommand(int originatorId, int commandId, int lineNum)
    {
      CommandId = commandId;
      NodeId = originatorId;
      LineNum = lineNum;
    }

    public byte[] Serialize()
    { 
      List<byte> command = new List<byte>();

      command.AddRange(Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}",
        Protocal.PARTITION, Protocal.SOH, NodeId, CommandId, LineNum, Name)));
      command.AddRange(SerializeCommand());
      command.Add((byte)Protocal.PARTITION);
      command.Add((byte)Protocal.EOT);
      return command.ToArray();
    }

    public static MPICommand Deserialize(string commandString)
    {
      _deserializedCommands++;
      List<string> values = new List<string>(commandString.Split(Protocal.PARTITION));
      MPICommand deserializedCommand = null; ;

      //the first 4 elements are just general elements, the last one is the EOT
      int sourceId = Int32.Parse(values[1]);
      int commandId = Int32.Parse(values[2]);
      int lineNum = Int32.Parse(values[3]);
      string commandName = values[4];

      string[] parameterList = values.Skip(5).ToArray();
      switch (commandName)
      {
        case MPI_Barrier.CommandName:
          deserializedCommand = new MPI_Barrier(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Comm_Rank.CommandName:
          deserializedCommand = new MPI_Comm_Rank(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Comm_Size.CommandName:
          deserializedCommand = new MPI_Comm_Size(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Finalize.CommandName:
          deserializedCommand = new MPI_Finalize(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Init.CommandName:
          deserializedCommand = new MPI_Init(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_IProbe.CommandName:
          deserializedCommand = new MPI_IProbe(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_IRecv.CommandName:
          deserializedCommand = new MPI_IRecv(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_ISend.CommandName:
          deserializedCommand = new MPI_ISend(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Probe.CommandName:
          deserializedCommand = new MPI_Probe(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Recv.CommandName:
          deserializedCommand = new MPI_Recv(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Send.CommandName:
          deserializedCommand = new MPI_Send(sourceId, commandId, lineNum, parameterList);
          break;
        case MPI_Wait.CommandName:
          deserializedCommand = new MPI_Wait(sourceId, commandId, lineNum, parameterList);
          break;
      }

      return deserializedCommand;
    }

    public int CommandId
    {
      get;
      private set;
    }

    public int NodeId
    {
      get;
      private set;
    }

    public int LineNum
    {
      get;
      private set;
    }

    public int? ReturnValue
    {
      get;
      protected set;
    }

    public int? ExpectedReturnValue
    {
        get;
        protected set;
    }

    public string Tooltip
    {
      get
      {
        return String.Format("OriginatorId: {1}{0}CommandId: {2}{0}LineNum: {3}{0}Command: {4}{0}{5}",
          Environment.NewLine, NodeId, CommandId, LineNum, Name, Description);
      }
    }

    public abstract string Name
    {
      get;
    }

    public virtual bool IsValid
    {
        get { return !ExpectedReturnValue.HasValue; }
    }

    public virtual IMessageCommand MatchingMessageCommand
    {
      get { return null; }
      set
      {

      }
    }

    public void ParseReturnValues(string[] returnValues)
    {
        this.ReturnValue = Int32.Parse(returnValues[0]);

        int expectedReturnValue;
        if (Int32.TryParse(returnValues[1], out expectedReturnValue))
            this.ExpectedReturnValue = expectedReturnValue;

        Complete(returnValues.Skip(2).ToArray());
    }

    protected virtual void Complete(string[] completeValues)
    {
        //Base implementation is that there is nothing to complete
        //Concrete objects with extra fields can override this
    }

    protected abstract byte[] SerializeCommand();

    protected abstract string Description
    {
      get;
    }

    protected static List<BufferValue> ParseBufferValues(string[] bufferValues)
    {
        List<BufferValue> buffer = null;

        //Check if any invalid buffer values where sent back
        if (bufferValues != null && bufferValues.Length > 0 && 
           (bufferValues[0] == "E" || bufferValues[0] == "U"))
        {
            List<BufferValue> bufferList = new List<BufferValue>();
            int indexCounter = 0;
            //Check if any buffer values had to be encoded
            if (bufferValues[0] == "E")
            {
                //the three keys are embedded in the message
                string sohReplacement = bufferValues[1];
                string barReplacement = bufferValues[2];
                string eotReplacement = bufferValues[3];

                //Values were encoded so decode here
                bufferValues.Skip(4).ToList().ForEach(x => bufferList.Add(
                    new BufferValue(indexCounter++,
                        x.Replace(sohReplacement, Protocal.SOH.ToString())
                            .Replace(barReplacement, Protocal.PARTITION.ToString())
                            .Replace(eotReplacement, Protocal.EOT.ToString()))));
            }
            else
            {
                //Nothing was encoded so just add them as they are.
                bufferValues.Skip(4).ToList().ForEach(x => bufferList.Add(
                    new BufferValue(indexCounter++, x)));
            }

            buffer = bufferList;
        }

        return buffer;
    }
  }
}
