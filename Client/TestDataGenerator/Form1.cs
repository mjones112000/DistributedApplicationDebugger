using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestDataGenerator
{
  public partial class Form1 : Form
  {

    public const char SOH = '\x01';
    public const char EOT = '\x04';
    public const char PARTITION = '|';
    public const string CONSOLE_HEADER = "CONSOLE";
    public const string FILE_NOT_FOUND_HEADER = "FILE NOT FOUND";
    public const string HISTORY_LIST_HEADER = "History List";

    public Form1()
    {
      InitializeComponent();
    }

    //MPIINIT:  SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
    //Send:     SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
    //ISend:    SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
    //IRECV:    SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
    //BARRIER:  SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
    //FINALIZE: SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT

    //RANK:     SOH|POST|NODEID|COMMANDID|RETURNVALUE|RANK|EOT
    //SIZE:     SOH|POST|NODEID|COMMANDID|RETURNVALUE|SIZE|EOT
    //RECV:     SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT
    //WAIT:     SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT
    //PROBE:    SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT
    //IPROBE:   SOH|POST|NODEID|COMMANDID|RETURNVALUE|FLAG|Status.Source, Status.Tag, Status.Error|EOT


    private void generateButton_Click(object sender, EventArgs e)
    {
      using (StreamWriter writer = new StreamWriter(String.Format("{0}\\TestData.txt", Application.StartupPath)))
      {
        writer.WriteLine(WrapCommand("|PRE|0|0|500|MPI_INIT||"));
        writer.WriteLine(WrapCommand("|PRE|0|1|501|MPI_RANK|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|2|502|MPI_SIZE|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|3|503|MPI_SEND|15|MPI_INT|1|10|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|4|504|MPI_ISEND|15|MPI_INT|1|10|MPI_COMM_WORLD|50001|"));
        writer.WriteLine(WrapCommand("|PRE|0|5|505|MPI_RECV|15|MPI_INT|1|20|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|6|506|MPI_IRECV|15|MPI_INT|1|20|MP_COMM_WORLD|50002|"));
        writer.WriteLine(WrapCommand("|PRE|0|7|507|MPI_WAIT|50003|"));
        writer.WriteLine(WrapCommand("|PRE|0|8|508|MPI_BARRIER|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|9|509|MPI_PROBE|1|20|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|10|510|MPI_IPROBE|1|20|MPI_COMM_WORLD|"));
        writer.WriteLine(WrapCommand("|PRE|0|11|511|MPI_FINALIZE||"));
        writer.WriteLine(WrapCommand("|CONSOLE|0|Node 0 Starting Up|"));
        writer.WriteLine(WrapCommand("|CONSOLE|0|\nNode 0 finishing now|"));
        writer.WriteLine(WrapCommand("|POST|0|0|1||"));
        writer.WriteLine(WrapCommand("|POST|0|4|1||"));
        //writer.WriteLine(WrapCommand(""));
        //writer.WriteLine(WrapCommand(""));
        writer.Close();
      }
    }

    private string WrapCommand(string command)
    {
      return String.Format("{0}{1}{2}", SOH, command, EOT);
    }
  }
}
