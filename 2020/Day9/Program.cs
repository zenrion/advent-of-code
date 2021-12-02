using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
  class Program
  {
    const int MaxPreambleSize = 25;

    static void Main(string[] args)
    {
      string[] data = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day9\input.txt");

      ulong[] convertedData = data
        .Select(element => ulong.Parse(element))
        .ToArray();

      ulong invalidNumber = SolvePartOne(convertedData);

      Console.WriteLine();

      SolvePartTwo(convertedData, invalidNumber);
    }

    private static void SolvePartTwo(ulong[] convertedData, ulong targetNumber)
    {
      Console.WriteLine("What is the encryption weakness in your XMAS-encrypted list of numbers?");

      for (int i = MaxPreambleSize; i < convertedData.Length; ++i)
      {
        ulong[] preamble = CreatePreamble(convertedData, i - MaxPreambleSize, i - 1);

        List<ulong> contiguousList = FindContigiousSetOfNumbers(preamble, targetNumber);
        if (contiguousList.Count() > 0)
        {
          ulong weakness = GetLowestNumberFromInput(contiguousList) + GetHighestNumberFromInput(contiguousList);
          Console.WriteLine(weakness);
          return;
        }
      }

      Console.WriteLine("NO CONTIGIUOUS NUMBERS FOUND");
    }

    private static ulong GetLowestNumberFromInput(List<ulong> input)
    {
      return input
        .OrderBy(number => number)
        .First();
    }

    private static ulong GetHighestNumberFromInput(List<ulong> input)
    {
      return input
        .OrderByDescending(number => number)
        .First();
    }

    private static List<ulong> FindContigiousSetOfNumbers(ulong[] arrayToCheck, ulong targetNumber)
    {
      int indexPointer = 0;
      int nextIndexPointer = indexPointer + 1;
      int numberOfContiguousItems = 0;

      ulong sum = 0;
      List<ulong> contigiousNumbers = new List<ulong>();

      while (indexPointer < arrayToCheck.Length - 1 && nextIndexPointer < arrayToCheck.Length)
      {
        sum += arrayToCheck[nextIndexPointer];

        if (sum == targetNumber && contigiousNumbers.Count > 1)
        {
          contigiousNumbers.Add(arrayToCheck[nextIndexPointer]);
          return contigiousNumbers;
        }

        if (sum > targetNumber)
        {
          sum = 0;
          contigiousNumbers.Clear();
          indexPointer = numberOfContiguousItems - indexPointer;
          numberOfContiguousItems = 0;
        }

        contigiousNumbers.Add(arrayToCheck[nextIndexPointer]);

        ++indexPointer;
        ++nextIndexPointer;
        ++numberOfContiguousItems;
      }

      return new List<ulong> ();
    }

    private static ulong SolvePartOne(ulong[] convertedData)
    {
      for (int i = MaxPreambleSize; i < convertedData.Length; ++i)
      {
        ulong[] preamble = CreatePreamble(convertedData, i - MaxPreambleSize, i - 1);
        ulong numberToFind = convertedData[i];

        if (!ContainsPairThatSumsToTarget(preamble, numberToFind))
        {
          Console.WriteLine("What is the first number that does not have this property?");
          Console.WriteLine(numberToFind);
          return numberToFind;
        }
      }

      Console.WriteLine("What is the first number that does not have this property?");
      Console.WriteLine("NO MATCH FOUND");
      return 0;
    }

    private static bool ContainsPairThatSumsToTarget(ulong[] preamble, ulong targetNumber)
    {
      Array.Sort(preamble);

      int leftPointer = 0;
      int rightPointer = preamble.Length - 1;
      while (leftPointer < rightPointer)
      {
        if (preamble[leftPointer] + preamble[rightPointer] == targetNumber)
        {
          return true;
        }

        if (preamble[leftPointer] + preamble[rightPointer] > targetNumber)
        {
          --rightPointer;
        }
        else if (preamble[leftPointer] + preamble[rightPointer] < targetNumber)
        {
          ++leftPointer;
        }
      }

      return false;
    }

    private static ulong[] CreatePreamble(ulong[] convertedData, int startIndex, int endIndex)
    {
      var test = convertedData
        .Where((element, index) => index >= startIndex && index <= endIndex)
        .ToArray();

      return test;
    }
  }
}
