namespace DistributedApplicationDebugger.Views
{
  partial class SettingsView
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
      this.sohLabel = new System.Windows.Forms.Label();
      this.sohTextBox = new System.Windows.Forms.TextBox();
      this.barTextBox = new System.Windows.Forms.TextBox();
      this.barLabel = new System.Windows.Forms.Label();
      this.eotTextBox = new System.Windows.Forms.TextBox();
      this.eotLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // sohLabel
      // 
      this.sohLabel.Location = new System.Drawing.Point(15, 25);
      this.sohLabel.Name = "sohLabel";
      this.sohLabel.Size = new System.Drawing.Size(140, 18);
      this.sohLabel.TabIndex = 0;
      this.sohLabel.Text = "SOH \'x01\' Replacement:";
      this.sohLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // sohTextBox
      // 
      this.sohTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.sohTextBox.Location = new System.Drawing.Point(158, 23);
      this.sohTextBox.Name = "sohTextBox";
      this.sohTextBox.Size = new System.Drawing.Size(283, 20);
      this.sohTextBox.TabIndex = 1;
      // 
      // barTextBox
      // 
      this.barTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.barTextBox.Location = new System.Drawing.Point(158, 54);
      this.barTextBox.Name = "barTextBox";
      this.barTextBox.Size = new System.Drawing.Size(283, 20);
      this.barTextBox.TabIndex = 3;
      // 
      // barLabel
      // 
      this.barLabel.Location = new System.Drawing.Point(15, 56);
      this.barLabel.Name = "barLabel";
      this.barLabel.Size = new System.Drawing.Size(140, 18);
      this.barLabel.TabIndex = 2;
      this.barLabel.Text = "Bar \'|\' Replacement:";
      this.barLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // eotTextBox
      // 
      this.eotTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.eotTextBox.Location = new System.Drawing.Point(158, 85);
      this.eotTextBox.Name = "eotTextBox";
      this.eotTextBox.Size = new System.Drawing.Size(283, 20);
      this.eotTextBox.TabIndex = 5;
      // 
      // eotLabel
      // 
      this.eotLabel.Location = new System.Drawing.Point(15, 87);
      this.eotLabel.Name = "eotLabel";
      this.eotLabel.Size = new System.Drawing.Size(140, 18);
      this.eotLabel.TabIndex = 4;
      this.eotLabel.Text = "EOT \'x04\' Replacement:";
      this.eotLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(313, 132);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(61, 26);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(380, 132);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(61, 26);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // SettingsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(452, 172);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.eotTextBox);
      this.Controls.Add(this.eotLabel);
      this.Controls.Add(this.barTextBox);
      this.Controls.Add(this.barLabel);
      this.Controls.Add(this.sohTextBox);
      this.Controls.Add(this.sohLabel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(468, 210);
      this.Name = "SettingsView";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "SettingsView";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label sohLabel;
    private System.Windows.Forms.TextBox sohTextBox;
    private System.Windows.Forms.TextBox barTextBox;
    private System.Windows.Forms.Label barLabel;
    private System.Windows.Forms.TextBox eotTextBox;
    private System.Windows.Forms.Label eotLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
  }
}