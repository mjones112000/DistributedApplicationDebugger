using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DistributedApplicationDebugger
{
  public class RedrawToolStripSplitButton : ToolStripSplitButton
  {
    private bool _justClicked = false;

    protected override void OnClick(EventArgs e)
    {

      //if (!DropDownButtonPressed)
      if(!Pressed)
      {
        //raise the click event
        base.OnClick(e);

        //set a flag to remeber that we just clicked it
        _justClicked = true;

        //Make the system think that the mouse just left so it redraws it
        base.OnMouseLeave(e);

        //revert the flag for futre drawing
        _justClicked = false;
      }
    }

    public override bool Enabled
    {
      get
      {
        //Say we are enabled if it was just clicked so that it draws correctly
        if (_justClicked)
        {
          return true;
        }
        else
          //Just return the true setting
          return base.Enabled;
      }
      set
      {
        base.Enabled = value;
      }
    }
  }
}
