using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Settings
{
  public class BindingConverter<T>
  {
    private Func<T> _converter = null;

    public BindingConverter(Func<T> converter)
    {
      _converter = converter;
    }

    public T Value
    {
      get
      {
        return _converter();
      }
    }
  }
}
