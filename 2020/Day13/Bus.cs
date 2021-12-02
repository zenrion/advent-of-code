using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
  class Bus
  {
    public int Id { get; set; }
    public int DepartureOffset { get; set; }
    public ulong LastDepartureTime { get; set; }

    public Bus() { }

    public Bus(string busIdString, int offsetTime)
    {
      Id = int.Parse(busIdString);
      DepartureOffset = offsetTime;
    }
  }
}
