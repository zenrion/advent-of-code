using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
  class Program
  {
    private static readonly Regex BitmaskMemoryLocationAndValueRegex = new Regex(@"^(mask = (?<bitmask>[01X]{36}))|(mem\[(?<memory_index>\d+)\] = (?<memory_value>\d+))$");

    static void Main(string[] args)
    {
      List<string> file = File.ReadAllLines(@"..\..\..\input.txt")
        .ToList();

      SolvePartOne(file);

      Console.WriteLine();

      SolvePartTwo(file);
    }

    private static void SolvePartOne(List<string> file)
    {
      var matches = file
         .Select((line) => BitmaskMemoryLocationAndValueRegex.Match(line))
         .ToList();

      var memory = new Dictionary<int, ulong>();
      var bitmask = string.Empty;
      foreach (var match in matches)
      {
        if (match.Groups["bitmask"].Success)
        {
          bitmask = match.Groups["bitmask"].Value;
        }
        else
        {
          var memoryIndex = int.Parse(match.Groups["memory_index"].Value);
          int memoryValue = int.Parse(match.Groups["memory_value"].Value);

          string memoryValueInBinaryForm = ConvertIntToBinary(memoryValue);
          memoryValueInBinaryForm = ApplyValueBitmask(memoryValueInBinaryForm, bitmask);

          memory[memoryIndex] = Convert.ToUInt64(memoryValueInBinaryForm, 2);
        }
      }

      Console.WriteLine("What is the sum of all values left in memory after it completes?");

      ulong total = memory.Values
        .Aggregate((currentTotal, nextItem) => currentTotal + nextItem);

      Console.WriteLine(total);
    }

    private static void SolvePartTwo(List<string> file)
    {
      var matches = file
        .Select(line => BitmaskMemoryLocationAndValueRegex.Match(line))
        .ToList();

      var memory = new Dictionary<ulong, ulong>();
      var bitmask = string.Empty;
      foreach (var match in matches)
      {
        if (match.Groups["bitmask"].Success)
        {
          bitmask = match.Groups["bitmask"].Value;
        }
        else
        {
          int memoryIndex = int.Parse(match.Groups["memory_index"].Value);
          ulong value = ulong.Parse(match.Groups["memory_value"].Value);

          string memoryIndexInBinaryForm = ConvertIntToBinary(memoryIndex);
          memoryIndexInBinaryForm = ApplyMemoryIndexBitmask(memoryIndexInBinaryForm, bitmask);

          IEnumerable<string> memoryIndices = CreateMemoryIndexPermutations(memoryIndexInBinaryForm);
          foreach (string index in memoryIndices)
          {
            memory[Convert.ToUInt64(index, 2)] = value;
          }
        }
      }

      Console.WriteLine("What is the sum of all values left in memory after it completes?");

      var total = memory.Values
       .Aggregate((currentTotal, nextItem) => currentTotal + nextItem);

      Console.WriteLine(total);
    }

    private static string ApplyValueBitmask(string memoryValueInBinaryForm, string bitmask)
    {
      char[] memoryValueWithBitmaskApplied = memoryValueInBinaryForm
        .Select((bit, i) => bitmask[i] == '0' || bitmask[i] == '1'
                          ? bitmask[i]
                          : bit)
        .ToArray();

      return new string(memoryValueWithBitmaskApplied);
    }

    private static string ApplyMemoryIndexBitmask(string memoryLocation, string bitmask)
    {
      char[] memoryLocationWithBitmaskApplied = memoryLocation
        .Select((bit, i) => bitmask[i] == 'X' || bitmask[i] == '1'
                          ? bitmask[i]
                          : bit)
        .ToArray();

      return new string(memoryLocationWithBitmaskApplied);
    }

    private static string ConvertIntToBinary(int value)
    {
      return Convert.ToString(value, 2)
        .PadLeft(36, '0');
    }

    private static IEnumerable<string> CreateMemoryIndexPermutations(string address)
    {
      var permutations = new List<string>();

      permutations = CreateMemoryIndexPermutation(permutations, address);

      return permutations;
    }

    private static List<string> CreateMemoryIndexPermutation(List<string> permutations, string address)
    {
      int indexOfBitToReplace = address.IndexOf('X');

      if (indexOfBitToReplace == -1)
      {
        permutations.Add(address);
        return permutations;
      }

      var replacements = new List<string>()
      {
        ReplaceBit(address, indexOfBitToReplace, "0"),
        ReplaceBit(address, indexOfBitToReplace, "1")
      };

      foreach (var replacement in replacements)
      {
        permutations = CreateMemoryIndexPermutation(permutations, replacement);
      }

      return permutations;
    }

    private static string ReplaceBit(string address, int index, string newBit)
    {
      return address
        .Remove(index, 1)
        .Insert(index, newBit);
    }
  }
}
