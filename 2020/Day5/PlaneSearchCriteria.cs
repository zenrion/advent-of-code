using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
  public class PlaneSearchCriteria
  {
    public readonly int Min;
    public readonly int Max;
    public readonly char MinIdentifier;
    public readonly char MaxIdentifier;

    public PlaneSearchCriteria() { }
    public PlaneSearchCriteria(int min, int max, char minId, char maxId)
    {
      Min = min;
      Max = max;
      MinIdentifier = minId;
      MaxIdentifier = maxId;
    }
  }
}
