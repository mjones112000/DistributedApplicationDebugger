using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication
{
  public class Protocal
  {
    public const char SOH = '\x01';
    public const char EOT = '\x04';
    public const char PARTITION = '|';
    public const string CONSOLE_HEADER = "CONSOLE";
    public const string GDB_HEADER = "GDB";
    public const string NODE_ID_HEADER = "NODE ID";
    public const string FILE_NOT_FOUND_HEADER = "FILE NOT FOUND";
    public const string ENVIRONMENT_DATA_HEADER = "ENVIRONMENT DATA";
    public const string RECORD_SESSION_HEADER = "RECORD SESSION";
    public const string BUFFER_VALUE_HEADER = "BUFFER VALUE";
    public const string PRE_COMMAND_HEADER = "PRE";
    public const string POST_COMMAND_HEADER = "POST";
  }
}
