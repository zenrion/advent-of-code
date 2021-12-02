using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
  public class Instruction
  {
    public string Operation { get; set; }
    public int Argument { get; set; }
    public bool IsNegative { get; set; }
  }
}
