using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DistributedApplicationDebugger;
using DistributedApplicationDebugger.Communication;
using DistributedApplicationDebugger.Communication.MPICommands;
using DistributedApplicationDebugger.Extensions;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;

namespace SSHLib
{
  public class SSHManager
  {
    private SshShell _sshShell;
    private Pipe _dataIn;
    private bool _running = false;

    private const string CONSOLE_PROMPT = "$";
    private const string PASSWORD_PROMPT = "password:";
    private const string DIRECTORY_NOT_FOUND_PROMPT = "No such file or directory";
    private const string CONTINUE_CONNECTING_PROMPT = "Are you sure you want to continue connecting (yes/no)?";
    private const string IDENTIFICATION_CHANGED_PROMPT = "REMOTE HOST IDENTIFICATION HAS CHANGED";
    private const string VERIFICATION_FAILED_PROMPT = "verification failed";
    private const string PERMISSION_DENIED_PROMPT = "Permission Denied";
    private const string CONNECTION_CLOSED_PROMPT = "Closed";
    private const string NO_ROUTE_PROMPT = "No route to host";
    public const string DEFAULT_TRANFER_DIRECTORY = "DistributedDebugger";

    private IEnumerable<SSHConfiguration> _sshConfigurations = null;
    private DirectoryInfo _transferDirectory = null;
    private TcpClient tcpClient = null;
    Stream outputStream = null;
    TcpClient client = null;

    public event EventHandler<ValueEventArgs<string>> StatusUpdate;
    public event EventHandler<ValueEventArgs<bool>> ConnectionComplete;
    public event EventHandler<ValueEventArgs<string>> IncomingData;
    public event EventHandler<ValueEventArgs<string>> RawDataUpdate;

    public SSHManager(IEnumerable<SSHConfiguration> sshConfigurations, DirectoryInfo transferDirectory)
    {
      _sshConfigurations = sshConfigurations;
      _transferDirectory = transferDirectory;

      ConnectionComplete += new EventHandler<ValueEventArgs<bool>>(SSHManager_ConnectionComplete);
    }

    void SSHManager_ConnectionComplete(object sender, ValueEventArgs<bool> e)
    {
      if (!e.Value)
        Disconnect();
    }

    public void Connect()
    {
      //First connect to the first machine
      SSHConfiguration initialConnection = _sshConfigurations.First();

      try
      {
        OnUpdateStatus(String.Format("Establishing SSH connection to {0} using user name {1}.", initialConnection.RemoteComputer, initialConnection.UserName));
        _sshShell = new SshShell(initialConnection.RemoteComputerAddress, initialConnection.UserName);
        _sshShell.Password = initialConnection.Password;
        _sshShell.Connect();
        _running = true;
      }
      catch (JSchException)
      {
        OnUpdateStatus(String.Format("SSH connection to {0}:{1} failed, check address, port, and credential data.", initialConnection.RemoteComputer, initialConnection.ConnectionPort));
        OnUpdateConnectionStatus(false);
        return;
      }
      catch (Exception ex)
      {
        OnUpdateStatus(String.Format("Exception while establishing SSH connection{0}{1}.", Environment.NewLine, ex.Message));
        OnUpdateConnectionStatus(false);
        return;
      }

      WaitForPrompt();
      OnUpdateStatus(String.Format("Connection to {0} established.{1}", initialConnection.RemoteComputer, Environment.NewLine));

      if (!CheckForRemoteDirectory(initialConnection,
        String.Format("{0}/{1}", initialConnection.TransferDirectory, _transferDirectory.Name).Trim('/')))
      {
        CopyFiles(initialConnection);
        if (_sshConfigurations.Last() == initialConnection)
          CompileCallCenter(initialConnection);
        else
          CompileBridge(initialConnection);
      }

      OnUpdateStatus(String.Format("File transfer to {0} complete", initialConnection.RemoteComputer));

      if (_sshConfigurations.Last() == initialConnection)
        LaunchCallCenter(initialConnection);
      else
        LaunchBridge(initialConnection, _sshConfigurations.ElementAt(1));

      OnUpdateStatus(String.Format("Application launch on {0} complete", initialConnection.RemoteComputer));

      //Keep a pointer to our current configuration so we can report where we left and where we went
      SSHConfiguration previousConfiguration = initialConnection;

      foreach (SSHConfiguration sshConfiguration in _sshConfigurations.Skip(1))
      {
        OnUpdateStatus(Environment.NewLine);

        if (!SSH(sshConfiguration))
        {
          OnUpdateConnectionStatus(false);
          return;
        }

        if (!CheckForRemoteDirectory(sshConfiguration, String.Format("{0}/{1}", sshConfiguration.TransferDirectory, _transferDirectory.Name).Trim('/')))
        {
          CreateRemoteTransferDirectory(sshConfiguration);
          ExitSSH(sshConfiguration);
          TransferFiles(previousConfiguration, sshConfiguration);

          if (!SSH(sshConfiguration))
          {
            OnUpdateConnectionStatus(false);
            break;
          }

          if (_sshConfigurations.Last() == sshConfiguration)
            CompileCallCenter(sshConfiguration);
          else
            CompileBridge(sshConfiguration);
        }

        if (_sshConfigurations.Last() == sshConfiguration)
        {
          if (!LaunchCallCenter(sshConfiguration))
          {
            OnUpdateConnectionStatus(false);
            break;
          }
        }
        else
          LaunchBridge(sshConfiguration, _sshConfigurations.SkipWhile(x => x != sshConfiguration).ElementAt(1));

        previousConfiguration = sshConfiguration;
      }


      //Make a connecton to the first node
      OnUpdateStatus(String.Format("Establishing TCP connection to {0}:{1}.", initialConnection.RemoteComputer, initialConnection.ConnectionPort));
      tcpClient = new TcpClient(initialConnection.RemoteComputerAddress, Int32.Parse(initialConnection.ConnectionPort));

      outputStream = tcpClient.GetStream();
      

      _dataIn = new Pipe(outputStream, null);
      _dataIn.IncomingMessage += (s, e) => OnUpdateIncomingData(e.Message);
      _dataIn.BeginStream();

      //Request the environment data list
      OnUpdateStatus(String.Format("Testing TCP connection."));
      try
      {
        SSHConfiguration callCenterConfiguration = _sshConfigurations.Last();
        string sessionDir = String.Format("{0}/DebuggerFiles/Sessions", callCenterConfiguration.TransferDirectory).Trim('/');

        Byte[] outBuffer = Encoding.ASCII.GetBytes(String.Format("{0}ENVIRONMENT{1}{2}{3}",
          Protocal.SOH, Protocal.PARTITION, sessionDir, Protocal.EOT));
        outputStream.Write(outBuffer, 0, outBuffer.Length);
      }
      catch(IOException)
      {
        OnUpdateStatus(String.Format("TCP connections failed, check configurations."));
        OnUpdateConnectionStatus(false);
        return;
      }
      OnUpdateStatus(String.Format("TCP connections succeeded."));

      OnUpdateStatus(String.Format("Environment Data Requested."));
    }

    public void Play(int numberOfNodes, string hostFile, string executableName, string parameters, IList<int> gdbNodes,
        string sohReplacement, string barReplacement, string eotReplacement)
    {
      MPICommand._deserializedCommands = 0;
      //Report that play request was received
      OnUpdateStatus(String.Format("Play request received.", Environment.NewLine));
      SSHConfiguration callCenterConnection = _sshConfigurations.Last();

      //Send the play request to the first node
      string playRequest = String.Format("{0}PLAY{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}{9}{10}",
            Protocal.SOH, Protocal.PARTITION, numberOfNodes, executableName.PadRight(1),
            hostFile.PadRight(1), parameters.PadRight(1), 
            sohReplacement, barReplacement, eotReplacement,
            gdbNodes.Delimit(",").PadRight(1), Protocal.EOT);

      OnUpdateStatus(String.Format("Sending Play Request {0} to {1}.", playRequest, callCenterConnection.RemoteComputer));
      Byte[] outBuffer = Encoding.ASCII.GetBytes(playRequest);
      outputStream.Write(outBuffer, 0, outBuffer.Length);
      OnUpdateStatus(String.Format("Play Request sent to {0}.{1}", callCenterConnection.RemoteComputer, Environment.NewLine));
    }

    public void Record(int numberOfNodes, string hostFile, string executableName, string parameters, IList<int> gdbNodes, string sessionName,
        string sohReplacement, string barReplacement, string eotReplacement)
    {
      MPICommand._deserializedCommands = 0;

      //Report that record request was received
      OnUpdateStatus(String.Format("Record request received.", Environment.NewLine));

      SSHConfiguration callCenterConnection = _sshConfigurations.Last();
      string sessionDir = String.Format("{0}/DebuggerFiles/Sessions", callCenterConnection.TransferDirectory).Trim('/');

      //Send the record request to the first node
      string request = String.Format("{0}RECORD{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}{9}{1}{10}{1}{11}{12}",
        Protocal.SOH, Protocal.PARTITION, numberOfNodes, executableName.PadRight(1), 
        hostFile.PadRight(1), parameters.PadRight(1),
        sohReplacement, barReplacement, eotReplacement,
        gdbNodes.Delimit(",").PadRight(1), 
        sessionDir.PadRight(1), sessionName.PadRight(1), Protocal.EOT);

      OnUpdateStatus(String.Format("Sending Record Request {0} to {1}.", request, callCenterConnection.RemoteComputer));
      Byte[] outBuffer = Encoding.ASCII.GetBytes(request);
      outputStream.Write(outBuffer, 0, outBuffer.Length);
      OnUpdateStatus(String.Format("Record Request sent to {0}.{1}", callCenterConnection.RemoteComputer, Environment.NewLine));
    }

    public void Replay(int numberOfNodes, string hostFile,
      string executableName, string parameters, IList<int> gdbNodes, string sessionName, string sessionTime, IList<int> playbackList,
      string sohReplacement, string barReplacement, string eotReplacement)
    {
      MPICommand._deserializedCommands = 0;
      //Report that replay request was received
      OnUpdateStatus(String.Format("Replay request received.", Environment.NewLine));

      SSHConfiguration callCenterConnection = _sshConfigurations.Last();
      string sessionDir = String.Format("{0}/DebuggerFiles/Sessions", callCenterConnection.TransferDirectory).Trim('/');
      StringBuilder commaDelimetedReplay = new StringBuilder();
      if (playbackList.Count > 0)
      {
        commaDelimetedReplay.Append(playbackList.First().ToString());
        foreach (int id in playbackList.Skip(1))
        {
          commaDelimetedReplay.Append(String.Format(",{0}", id.ToString()));
        }
      }

      //Send the record request to the first node
      string request = String.Format("{0}REPLAY{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}{9}{1}{10}{1}{11}{1}{12}{1}{13}{14}",
                          Protocal.SOH, Protocal.PARTITION, numberOfNodes, executableName.PadRight(1), 
                          hostFile.PadRight(1), parameters.PadRight(1),
                          sohReplacement, barReplacement, eotReplacement,
                          gdbNodes.Delimit(",").PadRight(1), 
                          sessionDir.PadRight(1), sessionName.PadRight(1),
                          sessionTime.PadRight(1), commaDelimetedReplay.ToString().PadRight(1), Protocal.EOT);

      OnUpdateStatus(String.Format("Sending Replay Request {0} to {1}.", request, callCenterConnection.RemoteComputer));
      Byte[] outBuffer = Encoding.ASCII.GetBytes(request);
      outputStream.Write(outBuffer, 0, outBuffer.Length);
      OnUpdateStatus(String.Format("Replay Request sent to {0}.{1}", callCenterConnection.RemoteComputer, Environment.NewLine));
    }

    public void GdbRequest(int nodeId, string message)
    {
      //Report that a gdb request has been received
      OnUpdateStatus(String.Format("Received GDB request for node {0}: {1}", nodeId, message));
      SSHConfiguration callCenterConnection = _sshConfigurations.Last();

      //Send the gdb request
      string gdbInputCommand = String.Format("{0}GDB INPUT{1}{2}{1}{3}{4}",
        Protocal.SOH, Protocal.PARTITION, nodeId, message, Protocal.EOT);

      OnUpdateStatus(String.Format("Sending GDB Request {0} to {1}.", gdbInputCommand, callCenterConnection.RemoteComputer));
      Byte[] outBuffer = Encoding.ASCII.GetBytes(gdbInputCommand);
      outputStream.Write(outBuffer, 0, outBuffer.Length);
      OnUpdateStatus(String.Format("GDB Request sent to {0}.{1}", callCenterConnection.RemoteComputer, Environment.NewLine));


    }

    public void Disconnect()
    {
      _running = false;

      if (client != null)
      {
        //client.GetStream().Dispose();
        client.Close();
        client = null;
      }

      if (_sshShell != null)
      {
        _sshShell.Close();
      }

      if (_dataIn != null)
      {
        _dataIn.EndStream();
      }
    }

    public void MessageBufferRequested(IEnumerable<IMessageCommand> messageCommands, string fileLocation, 
      string sohReplacement, string barReplacement, string eotReplacement)
    {
      //SOH BUFFER REQUEST|NODE ID|COMMAND ID1, COMMAND ID2...|VALUES FILE|SOH STR|PARTITION STR|EOT STR|EOT
      SSHConfiguration callCenterConfiguration = _sshConfigurations.Last();
      string sessionDir = String.Format("{0}/DebuggerFiles/Sessions", callCenterConfiguration.TransferDirectory).Trim('/');
      int nodeId = messageCommands.First().NodeId;

      StringBuilder sb = new StringBuilder();
      var orderedMessagses = messageCommands.OrderBy(x => x.BufferCommandId).ToArray();
      sb.Append(String.Format("{0}.{1}", orderedMessagses.First().CommandId, orderedMessagses.First().BufferCommandId));

      foreach (var command in orderedMessagses.Skip(1))
      {
        sb.Append(String.Format(",{0}.{1}",command.CommandId, command.BufferCommandId));
      }

      Byte[] outBuffer = Encoding.ASCII.GetBytes(String.Format("{0}BUFFER REQUEST{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}",
        Protocal.SOH, Protocal.PARTITION, nodeId, sb.ToString(),
        fileLocation, sohReplacement, barReplacement, eotReplacement, Protocal.EOT));
      outputStream.Write(outBuffer, 0, outBuffer.Length);
    }

    public void SessionComplete()
    {
      Byte[] outBuffer = Encoding.ASCII.GetBytes(String.Format("{0}MPI COMPLETE{1}",
         Protocal.SOH, Protocal.EOT));
      outputStream.Write(outBuffer, 0, outBuffer.Length);
    }

    private void CopyFiles(SSHConfiguration destination)
    {
      OnUpdateStatus(String.Format("Creating a secure copy session with {0}.", destination.RemoteComputer));
      Scp secureCopy = new Scp(destination.RemoteComputerAddress, destination.UserName);
      secureCopy.Password = destination.Password;
      secureCopy.Connect();
      secureCopy.Recursive = true;
      OnUpdateStatus(String.Format("Secure copy session with {0} created.", destination.RemoteComputer));

      if (!String.IsNullOrEmpty(destination.TransferDirectory))
      {
        OnUpdateStatus(String.Format("Creating folder {0} on {1}.", destination.TransferDirectory, destination.RemoteComputer));
        StringBuilder incrementalFolderPath = new StringBuilder();
        foreach (string folderLevel in destination.TransferDirectory.Split('/'))
        {
          string folderpath = String.Format("{0}/{1}", incrementalFolderPath.ToString(), folderLevel).Trim('/');
          if (!CheckForRemoteDirectory(destination, folderpath, false))
            secureCopy.Mkdir(folderpath);

          incrementalFolderPath.Append(String.Format("/{0}", folderLevel));
        }
      }

      AutoResetEvent resetEvent = new AutoResetEvent(false);
      secureCopy.OnTransferEnd +=
        ((string src, string dst, int transferredBytes, int totalBytes, string message) => resetEvent.Set());

      string destinationPath = String.Format("{0}/{1}", destination.TransferDirectory, _transferDirectory.Name).Trim('/');
      OnUpdateStatus(String.Format("Copying debugging files to folder {0} on {1}.", destinationPath, destination.RemoteComputer));
      secureCopy.Put(_transferDirectory.FullName, destinationPath);
      resetEvent.WaitOne();
      secureCopy.Close();

      OnUpdateStatus(String.Format("Copying of debugging files to {0} completed.", destination.RemoteComputer));
    }

    private void Write(string message)
    {
      ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      Byte[] bytes = encoding.GetBytes(String.Format("{0}\n", message));
      _sshShell.OutputStream.Write(bytes, 0, bytes.Length);
    }

    private bool CheckForRemoteDirectory(SSHConfiguration destination, string destinationPath, bool logging = true)
    {
      if (logging)
        OnUpdateStatus(String.Format("Checking if directory {0} exists already on {1}.", destinationPath, destination.RemoteComputer));

      Write(String.Format("ls {0}", destinationPath));
      string prompt = WaitForPrompt("ls");

      bool result = String.Compare(prompt, DIRECTORY_NOT_FOUND_PROMPT) != 0;

      if (logging)
      {
        if (result)
          OnUpdateStatus(String.Format("Directory {0} already exists on {1}.", destinationPath, destination.RemoteComputer));
        else
          OnUpdateStatus(String.Format("Directory {0} not found on {1}.", destinationPath, destination.RemoteComputer));
      }

      return result;
    }

    private bool CreateRemoteTransferDirectory(SSHConfiguration remoteConfiguration)
    {
      string transferDirectoryPath = String.Format("{0}/{1}", remoteConfiguration.TransferDirectory, _transferDirectory.Name).Trim('/');
      OnUpdateStatus(String.Format("Creating directory {0} on {1}.", transferDirectoryPath, remoteConfiguration.RemoteComputer));

      Write(String.Format("ssh {0}@{1} \"mkdir -p {2}\"", remoteConfiguration.UserName, remoteConfiguration.RemoteComputerAddress, transferDirectoryPath));

      string prompt = WaitForPrompt();

      if (prompt != CONSOLE_PROMPT)
      {
        while (prompt != PASSWORD_PROMPT)
        {
          if (prompt == CONTINUE_CONNECTING_PROMPT)
            Write("yes");

          if (prompt == IDENTIFICATION_CHANGED_PROMPT)
          {
            OnUpdateStatus(String.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}",
              "Exiting: Remote Host Identification changed, contact system administrator to correct known_hosts file.",
              Environment.NewLine,
              "Running the following at the servers may fix the problem is some cases:",
              "vi ~/.ssh/known_hosts", "dd",":wq"));

            OnUpdateConnectionStatus(false);
            return false;
          }

          if (prompt == VERIFICATION_FAILED_PROMPT)
          {
            OnUpdateStatus("Exiting: Credentials\\Verification Failed");
            OnUpdateConnectionStatus(false);
            return false;
          }

          prompt = WaitForPrompt();
        }
        OnUpdateStatus(String.Format("Password prompt from {0} detected, sending password.", remoteConfiguration.RemoteComputer));

        //Supply the password
        Write(remoteConfiguration.Password);
        WaitForPrompt();
      }

      return true;
    }

    private void TransferFiles(SSHConfiguration source, SSHConfiguration destination)
    {
      if (source == null)
      {
        return;
      }
      string transferDirectoryPath = String.Format("{0}/{1}", source.TransferDirectory, _transferDirectory.Name).Trim('/');
      OnUpdateStatus(String.Format("Transfering {0} to {1}.", transferDirectoryPath, destination.RemoteComputer));

      string formatCommand = String.Format("scp -r {0} {1}@{2}:{3}", transferDirectoryPath,
        destination.UserName, destination.RemoteComputerAddress, destination.TransferDirectory);

      Write(formatCommand);

      string prompt = WaitForPrompt();

      if (prompt == CONTINUE_CONNECTING_PROMPT)
        Write("yes");

      OnUpdateStatus(String.Format("Password prompt from {0} detected, sending password.", destination.RemoteComputer));
      //Supply the password
      Write(destination.Password);
      WaitForPrompt();


      OnUpdateStatus(String.Format("File transfer to {0} complete.", destination.RemoteComputer));
    }

    private bool SSH(SSHConfiguration destination)
    {
      OnUpdateStatus(String.Format("SSH'ing into {0}, using user name {1}.", destination.RemoteComputer, destination.UserName));

      //Do the first part to log in
      Write(String.Format("ssh {0}@{1}", destination.UserName, destination.RemoteComputerAddress));

      string prompt = WaitForPrompt();

      while (prompt != PASSWORD_PROMPT)
      {
        if (prompt == CONTINUE_CONNECTING_PROMPT)
          Write("yes");

        if (prompt == IDENTIFICATION_CHANGED_PROMPT)
        {
          OnUpdateStatus("Remote Host Identification changed, contact system administrator to correct known_hosts file.");
          return false;
        }

        if (prompt == VERIFICATION_FAILED_PROMPT)
        {
          OnUpdateStatus(String.Format("Credentials verification for {0} failed.", destination.RemoteComputer));
          return false;
        }

        if (prompt == NO_ROUTE_PROMPT)
        {
          OnUpdateStatus(String.Format("Connection path could not be made to {0}:{1}, check address and port.", 
              destination.RemoteComputer, destination.ConnectionPort));
          return false;
        }

        prompt = WaitForPrompt();
      }

      OnUpdateStatus(String.Format("Password prompt from {0} detected, sending password.", destination.RemoteComputer));

      //Supply the password
      Write(destination.Password);
      string passwordResult = WaitForPrompt();
      if (passwordResult == PERMISSION_DENIED_PROMPT)
      {
        OnUpdateStatus(String.Format("Password for {0} denied.", destination.RemoteComputer));
        return false;
      }
      else
      {
        OnUpdateStatus(String.Format("SSH to {0} complete.", destination.RemoteComputer));
      }

      return true;
    }

    private void ExitSSH(SSHConfiguration sshConfiguration)
    {
      OnUpdateStatus(String.Format("Exiting {0}", sshConfiguration.RemoteComputer));
      Write("exit");

      OnUpdateStatus(String.Format("Waiting for closed connection confirmation."));

      string promptResult = WaitForPrompt();
      while (promptResult != CONNECTION_CLOSED_PROMPT)
      {
        promptResult = WaitForPrompt();
      }

      OnUpdateStatus(String.Format("SSH closed confirmed."));
    }

    private void CompileBridge(SSHConfiguration currentWorkstation)
    {
      //Launch the make file to compile the bridge and the call center    
      string makeCommand = String.Format("make -C {0}/Bridge",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'));
      Write(makeCommand);
      WaitForPrompt();

      string chmodCommand = String.Format("chmod 777 {0}/Bridge*",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'));

      OnUpdateStatus(String.Format("Granting permissions {0} on {1}", chmodCommand, currentWorkstation.RemoteComputer));
      Write(chmodCommand);
      if (WaitForPrompt() == PASSWORD_PROMPT)
      {
        OnUpdateStatus(String.Format("Supplying password for chmod"));
        Write(currentWorkstation.Password);
        WaitForPrompt();
      }
    }

    private void LaunchBridge(SSHConfiguration currentWorkstation, SSHConfiguration nextWorkstation)
    {
      string appDestination = String.Format("./{0}/Bridge/tcpBridge -b {1} {2} {3}",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'),
        currentWorkstation.ConnectionPort, nextWorkstation.RemoteComputerAddress, nextWorkstation.ConnectionPort);

      string launchCommand = String.Format("{0} &", appDestination);
      OnUpdateStatus(String.Format("Launching {0} on {1}", launchCommand, currentWorkstation.RemoteComputer));

      Write(launchCommand);
      WaitForPrompt();
    }

    private void CompileCallCenter(SSHConfiguration currentWorkstation)
    {
      //Launch the make file to compile the bridge and the call center    
      string makeCommand = String.Format("make -C {0}/CallCenter",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'));
      Write(makeCommand);
      WaitForPrompt();

      string chmodCommand = String.Format("chmod 777 {0}/CallCenter*",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'));

      OnUpdateStatus(String.Format("Granting permissions {0} on {1}", chmodCommand, currentWorkstation.RemoteComputer));
      Write(chmodCommand);
      if (WaitForPrompt() == PASSWORD_PROMPT)
      {
        OnUpdateStatus(String.Format("Supplying password for chmod"));
        Write(currentWorkstation.Password);
        WaitForPrompt();
      }
    }

    private bool LaunchCallCenter(SSHConfiguration currentWorkstation)
    {
      string appDestination = String.Format("./{0}/CallCenter/callCenter {1}",
        String.Format("{0}/{1}", currentWorkstation.TransferDirectory, _transferDirectory.Name).Trim('/'),
        currentWorkstation.ConnectionPort);

      string launchCommand = String.Format("{0} &", appDestination);

      OnUpdateStatus(String.Format("Launching {0} on {1}", launchCommand, currentWorkstation.RemoteComputer));

      Write(launchCommand);
      string promptResult = WaitForPrompt();

      if (promptResult == DIRECTORY_NOT_FOUND_PROMPT)
      {
          OnUpdateStatus(String.Format(
          "Directory /{0}/CallCenter not found on {1}.  Exiting.",
          currentWorkstation.TransferDirectory, currentWorkstation.RemoteComputer));

          return false;
      }

      return true;
    }

    private string WaitForPrompt(string commandName = null)
    {
      byte[] buff = new byte[2048];
      StringBuilder inputBuffer = new StringBuilder();

      while (_running)
      {
        //Read incoming message
        int n = _sshShell.InputStream.Read(buff, 0, buff.Length);
        if (n > -1)
        {
          //Convert the read message to string
          inputBuffer.Append(Encoding.Default.GetString(buff, 0, n));
          string inputBuffStr = inputBuffer.ToString();

          OnUpdateRawData(inputBuffStr);

          if (inputBuffStr.ConstainsCaseInsensitive(CONSOLE_PROMPT))
          {
            if (inputBuffStr.ConstainsCaseInsensitive(IDENTIFICATION_CHANGED_PROMPT))
            {
              return IDENTIFICATION_CHANGED_PROMPT;
            }

            if (inputBuffStr.ConstainsCaseInsensitive(VERIFICATION_FAILED_PROMPT))
              return VERIFICATION_FAILED_PROMPT;

            if (inputBuffStr.ConstainsCaseInsensitive(DIRECTORY_NOT_FOUND_PROMPT))
              return DIRECTORY_NOT_FOUND_PROMPT;

            else if (inputBuffStr.ConstainsCaseInsensitive(PASSWORD_PROMPT))
            {
              return PASSWORD_PROMPT;
            }
            else if (inputBuffStr.ConstainsCaseInsensitive(CONNECTION_CLOSED_PROMPT))
            {
              return CONNECTION_CLOSED_PROMPT;
            }
            else if (inputBuffStr.ConstainsCaseInsensitive(NO_ROUTE_PROMPT))
            {
              return NO_ROUTE_PROMPT;
            }

            //just return it as a console prompt
            return CONSOLE_PROMPT;
          }
          else if (inputBuffStr.ConstainsCaseInsensitive(PERMISSION_DENIED_PROMPT))
          {
            return PERMISSION_DENIED_PROMPT;
          }
          else if (inputBuffStr.ConstainsCaseInsensitive(CONTINUE_CONNECTING_PROMPT))
          {
            return CONTINUE_CONNECTING_PROMPT;
          }
          else if (inputBuffStr.ConstainsCaseInsensitive(PASSWORD_PROMPT))
          {
            return PASSWORD_PROMPT;
          }
          else if (inputBuffStr.ConstainsCaseInsensitive(IDENTIFICATION_CHANGED_PROMPT))
          {
            return IDENTIFICATION_CHANGED_PROMPT;
          }
        }
        else
        {
          Thread.Sleep(1000);
        }
      }

      return null;
    }

    private string LocalPathDelimiter
    {
      get
      {
        if (Environment.CurrentDirectory.Contains("\\"))
          return "\\";
        else
          return "/";
      }
    }

    protected void OnUpdateRawData(string status)
    {
      var rawDataUpdate = RawDataUpdate;

      if (rawDataUpdate != null)
      {
        rawDataUpdate(this, new ValueEventArgs<string>(status));
      }
    }

    private void OnUpdateStatus(string status)
    {
      var statusUpdate = StatusUpdate;

      if (statusUpdate != null)
      {
        statusUpdate(this, new ValueEventArgs<string>(status));
      }
    }

    private void OnUpdateConnectionStatus(bool connected)
    {
      var connectionStatus = ConnectionComplete;

      if (connectionStatus != null)
      {
        connectionStatus(this, new ValueEventArgs<bool>(connected));
      }
    }

    private void OnUpdateIncomingData(string incomingData)
    {
      incomingData = incomingData.Replace("\n", Environment.NewLine);

      var incomingDataEvent = IncomingData;

      if (incomingDataEvent != null)
      {
        incomingDataEvent(this, new ValueEventArgs<string>(incomingData));
      }
    }
  }
}


