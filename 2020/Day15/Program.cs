using System;
using System.Collections.Generic;

namespace Day15
{
  class Program
  {
    static void Main(string[] args)
    {
      int[] startingNumbers = new[] { 2, 1, 10, 11, 0, 6 };

      SolvePartOne(startingNumbers, 2020);
      SolvePartOne(startingNumbers, 30000000);
    }

    private static void SolvePartOne(int[] startingNumbers, int turnLimit)
    {
      var mostRecentNumberOccurences = new Dictionary<int, int>();
      var secondMostRecentNumberOccurences = new Dictionary<int, int>();

      int currentNumber = 0;
      int previousNumber = 0;
      for (int i = 1; i <= turnLimit; ++i)
      {
        if (i <= startingNumbers.Length)
        {
          mostRecentNumberOccurences.Add(startingNumbers[i - 1], i);
          currentNumber = startingNumbers[i - 1];
        }
        else
        {
          if (secondMostRecentNumberOccurences.ContainsKey(previousNumber))
          {
            currentNumber = mostRecentNumberOccurences[previousNumber] - secondMostRecentNumberOccurences[previousNumber];
          }
          else
          {
            currentNumber = 0;
          }

          if (mostRecentNumberOccurences.ContainsKey(currentNumber))
          {
            secondMostRecentNumberOccurences[currentNumber] = mostRecentNumberOccurences[currentNumber];
          }

          mostRecentNumberOccurences[currentNumber] = i;
        }

        previousNumber = currentNumber;
      }

      Console.WriteLine(previousNumber);
    }
  }
}
