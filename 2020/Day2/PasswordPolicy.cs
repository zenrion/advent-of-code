using System;
using System.Collections.Generic;
using System.Text;

namespace Day2
{
  public class PasswordPolicy
  {
    public int MinOccurence { get; set; }
    public int MaxOccurence { get; set; }
    public char Letter { get; set; }
    public string Password { get; set; }

    public PasswordPolicy() { }

    public PasswordPolicy(int minOccurence, int maxOccurence, char letter, string password)
    {
      MinOccurence = minOccurence;
      MaxOccurence = maxOccurence;
      Letter = letter;
      Password = password;
    }
  }
}
