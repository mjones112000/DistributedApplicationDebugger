using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DistributedApplicationDebugger
{
  public class RedrawToolStrip : ToolStrip
  {
    private Image _image = null;
    private string _toolTip = string.Empty;
    private BindingList<ToolStripMenuItem> _toolStripButtons = null;

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      foreach (ToolStripItem item in Items)
      {
        item.Enabled = Enabled;
      }

      this.Refresh();
    }

    public Image ButtonImage
    {
      get
      {
        return _image;
      }
      set
      {
        _image = value;
        foreach (ToolStripItem item in Items)
        {
          item.Image = _image;
        }
      }
    }

    public string ToolTip
    {
      get
      {
        return _toolTip;
      }
      set
      {
        _toolTip = value;
        foreach (ToolStripItem item in Items)
        {
          item.ToolTipText = _toolTip;
        }
      }
    }


    public BindingList<ToolStripMenuItem> DropDownList
    {
      get
      {
        return _toolStripButtons;
      }
      set
      {
        _toolStripButtons = value;
        if (value != null)
        {
          string s = "SHHH";
        }
        if (Items != null)
        {
          foreach (object item in Items)
          {
            ToolStripSplitButton toolStripSplitButton = item as ToolStripSplitButton;
            if (toolStripSplitButton != null)
            {
              toolStripSplitButton.DropDownItems.Clear();
              toolStripSplitButton.DropDownItems.AddRange(_toolStripButtons.ToArray<ToolStripMenuItem>());
            }
          }
        }
      }
    }

    //public IList<ToolStripButton> DropDownList
    //{
    //  get
    //  {
    //    return _toolStripButtons;
    //  }
    //  set
    //  {
    //    _toolStripButtons = value;
    //    if (Items != null)
    //    {
    //      foreach (object item in Items)
    //      {
    //        ToolStripSplitButton toolStripSplitButton = item as ToolStripSplitButton;
    //        if (toolStripSplitButton != null)
    //        {
    //          toolStripSplitButton.DropDownItems.Clear();
    //          toolStripSplitButton.DropDownItems.AddRange(_toolStripButtons.ToArray<ToolStripButton>());
    //        }
    //      }
    //    }
    //  }
    //}
  }
}
