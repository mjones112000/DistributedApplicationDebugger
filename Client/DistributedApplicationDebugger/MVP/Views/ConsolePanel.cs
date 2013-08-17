using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DistributedApplicationDebugger.Views
{
  public partial class ConsolePanel : UserControl
  {
    private bool _gdbEnabled = false;

    public ConsolePanel()
    {
      InitializeComponent();
    }

    public void AppendText(string message)
    {      
      consoleText.AppendText(message);
    }

    public void AppendGdbMessage(string gdbMessage)
    {
      this.gdbText.AppendText(gdbMessage);
    }

    public void Reset()
    {
      consoleText.Clear();
      gdbText.Clear();
    }

    public bool GdbEnabled
    {
      get { return _gdbEnabled; }
      set
      {
        _gdbEnabled = value;

        gdbOutMessagesButton.Enabled = _gdbEnabled;
        splitConsoleButton.Enabled = _gdbEnabled;
        if (_gdbEnabled)
        {
          gdbOutMessagesButton.Checked = true;
          consoleOutButton.Checked = false;
          splitConsoleButton.Checked = false;
          consoleSplitter.Panel1Collapsed = true;
          consoleSplitter.Panel2Collapsed = false;
        }
        else
        {
          consoleOutButton.Checked = true;
          gdbOutMessagesButton.Checked = false;
          splitConsoleButton.Checked = false;
          consoleSplitter.Panel1Collapsed = false;
          consoleSplitter.Panel2Collapsed = true;
        }
        
      }
    }


    private void consoleOutButton_Click(object sender, EventArgs e)
    {
      gdbOutMessagesButton.Checked = false;
      splitConsoleButton.Checked = false;
      consoleSplitter.Panel1Collapsed = false;
      consoleSplitter.Panel2Collapsed = true;
    }

    private void gdbOutMessagesButton_Click(object sender, EventArgs e)
    {
      consoleOutButton.Checked = false;
      splitConsoleButton.Checked = false;
      consoleSplitter.Panel2Collapsed = false;
      consoleSplitter.Panel1Collapsed = true;
    }

    private void splitConsoleButton_Click(object sender, EventArgs e)
    {
      consoleOutButton.Checked = false;
      gdbOutMessagesButton.Checked = false;
      consoleSplitter.Panel1Collapsed = false;
      consoleSplitter.Panel2Collapsed = false;
    }
  }
}
