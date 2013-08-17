using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DistributedApplicationDebugger.Views
{
  public partial class SettingsView : Form
  {
    public SettingsView(string sohReplacement, string barReplacement, string eotReplacement)
    {
      InitializeComponent();
      sohTextBox.Text = sohReplacement;
      barTextBox.Text = barReplacement;
      eotTextBox.Text = eotReplacement;
    }

    public string SOHReplacement
    {
      get;
      private set;
    }

    public string BARReplacement
    {
      get;
      private set;
    }

    public string EOTReplacement
    {
      get;
      private set;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      SOHReplacement = sohTextBox.Text;
      BARReplacement = barTextBox.Text;
      EOTReplacement = eotTextBox.Text;

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
