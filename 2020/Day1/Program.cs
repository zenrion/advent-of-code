using System;
using System.IO;

namespace Day1
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] fileContents = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day1\input.txt");

      int[] numberArray = ConvertToIntArray(fileContents);
      Array.Sort(numberArray);

      Console.WriteLine($"Product of 2 numbers: {ProductOfTwoNumbersInArray(numberArray)}");
      Console.WriteLine($"Product of 3 numbers: {ProductOfThreeNumbersInArray(numberArray)}");
    }

    private static int ProductOfTwoNumbersInArray(int[] numberArray)
    {
      int leftIter = 0;
      int rightIter = numberArray.Length - 1;
      while (leftIter < rightIter)
      {
        if (numberArray[leftIter] + numberArray[rightIter] == 2020)
        {
          return numberArray[leftIter] * numberArray[rightIter];
        }
        else if (numberArray[leftIter] + numberArray[rightIter] > 2020)
        {
          --rightIter;
        }
        else
        {
          ++leftIter;
        }
      }
      return -1;
    }

    private static int ProductOfThreeNumbersInArray(int[] numberArray)
    {
      int leftIter = 1;
      int rightIter = numberArray.Length - 1;
      while (leftIter < rightIter)
      {
        if (numberArray[0] + numberArray[leftIter] + numberArray[rightIter] == 2020)
        {
          return numberArray[0] * numberArray[leftIter] * numberArray[rightIter];
        }
        else if (numberArray[0] + numberArray[leftIter] + numberArray[rightIter] > 2020)
        {
          --rightIter;
        }
        else
        {
          ++leftIter;
        }
      }
      return -1;
    }

    static int[] ConvertToIntArray(string[] stringArray)
    {
      int[] numberArray = new int[stringArray.Length];
      for (int i = 0; i < stringArray.Length; ++i)
      {
        numberArray[i] = int.Parse(stringArray[i]);
      }

      return numberArray;
    }
  }
}
