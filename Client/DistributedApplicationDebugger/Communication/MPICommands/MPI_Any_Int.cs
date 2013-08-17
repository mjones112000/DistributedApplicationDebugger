using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public abstract class MPI_Any_Int : IComparable<MPI_Any_Int>
  {
    public abstract bool IsSet{ get; }
    protected abstract string AmbiguousDescription { get; }
    public abstract override bool Equals(object obj);
    public override int GetHashCode()
    {
      return ActualValue;
    }


    protected abstract int AmbiguousValue 
    {
      get;
    }

    private int _initialValue = 0;

    protected MPI_Any_Int(int initialvalue)
    {
      _initialValue = initialvalue;
      ActualValue = initialvalue;
    }

    public int ActualValue
    {
      get;
      set;
    }

    public string Value
    {
      get
      {
        if (ActualValue == AmbiguousValue)
        {
          return "*";
        }
        else if (_initialValue == AmbiguousValue)
        {
          return String.Format("*{0}", ActualValue);
        }
        else
        {
          return ActualValue.ToString();
        }
      }
    }
    
    public override string ToString()
    {
      if (_initialValue == AmbiguousValue)
      {
        return AmbiguousDescription;
      }
      else
      {
        return ActualValue.ToString();
      }
    }

    public int CompareTo(MPI_Any_Int other)
    {
      return ActualValue.CompareTo(other.ActualValue);
    }

    public static bool operator ==(MPI_Any_Int a, MPI_Any_Int b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(MPI_Any_Int a, MPI_Any_Int b)
    {
        return !(a == b);
    }
  }

  public class MPI_TAG : MPI_Any_Int
  {
    public MPI_TAG(int initialValue) 
      : base(initialValue)
    {
      
    }

    public override bool IsSet
    {
      get
      {
        return ActualValue != MPICommand.MPI_ANY_TAG;
      }
    }

    public override bool Equals(object obj)
    {
      MPI_TAG otherTag = obj as MPI_TAG;
      if (otherTag != null)
      {
        return otherTag.ActualValue == ActualValue;
      }

      return false; 
    }

    protected override int AmbiguousValue
    {
        get { return MPICommand.MPI_ANY_TAG; }
    }

    protected override string AmbiguousDescription
    {
      get
      {
        return "MPI_ANY_TAG";
      }
    }
  }

  public class MPI_SOURCE : MPI_Any_Int
  {
    public MPI_SOURCE(int initialValue)
      : base(initialValue)
    {

    }

    public override bool IsSet
    {
      get
      {
        return ActualValue != MPICommand.MPI_ANY_SOURCE;
      }
    }

    protected override int AmbiguousValue
    {
        get { return MPICommand.MPI_ANY_SOURCE; }
    }

    protected override string AmbiguousDescription
    {
      get
      {
        return "MPI_ANY_SOURCE";
      }
    }

    public override bool Equals(object obj)
    {
      MPI_SOURCE otherSource = obj as MPI_SOURCE;
      if (otherSource != null)
      {
        return otherSource.ActualValue == ActualValue;
      }

      return false;
    }
  }
}
