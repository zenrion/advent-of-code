using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
  class Program
  {
    static void Main(string[] args)
    {
      List<int> adapterVoltages = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day10\input.txt")
        .Select(line => int.Parse(line))
        .ToList();

      adapterVoltages.Add(0); // Account for the charging outlet.
      adapterVoltages.Sort();
      adapterVoltages.Add(adapterVoltages.Last() + 3); // Account for the build-in adapter.

      SolvePartOne(adapterVoltages);
      SolvePartTwo(adapterVoltages);
    }

    private static void SolvePartTwo(List<int> adapterVoltages)
    {
      Console.WriteLine("What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?");

      Dictionary<int, ulong> cache = new Dictionary<int, ulong>();
      ulong totalNumberOfDistinctCombinations = CalculateTotalWays(cache, adapterVoltages, 0);

      Console.WriteLine(totalNumberOfDistinctCombinations);
    }

    private static ulong CalculateTotalWays(Dictionary<int, ulong> cache, List<int> adapterVoltages, int index)
    {
      if (index == adapterVoltages.Count - 1)
      {
        return 1;
      }

      if (cache.ContainsKey(index))
      {
        return cache[index];
      }

      ulong totalWays = 0;
      for (int j = index + 1; j < adapterVoltages.Count; ++j)
      {
        if (adapterVoltages[j] - adapterVoltages[index] <= 3)
        {
          totalWays += CalculateTotalWays(cache, adapterVoltages, j);
        }
      }

      cache.Add(index, totalWays);
      return totalWays;
    }

    private static void SolvePartOne(List<int> adapterVoltages)
    {
      int highestAdaptervoltage = adapterVoltages.Last();

      Console.WriteLine("What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?");

      int outletVolts = 0;
      int oneVoltDifferences = 0;
      int threeVoltDifferences = 0;
      while (outletVolts < highestAdaptervoltage)
      {
        int adapterVolts = adapterVoltages
          .Where(jolts => jolts > outletVolts && jolts <= outletVolts + 3)
          .OrderBy(jolts => jolts)
          .ToList()
          .FirstOrDefault();

        int difference = adapterVolts - outletVolts;

        if (difference == 1)
        {
          ++oneVoltDifferences;
        }
        else if (difference == 3)
        {
          ++threeVoltDifferences;
        }

        outletVolts = adapterVolts;
      }

      Console.WriteLine(oneVoltDifferences * threeVoltDifferences);
    }
  }
}
