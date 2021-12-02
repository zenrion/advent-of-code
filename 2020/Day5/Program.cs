using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] binarySpacePartitions = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day5\input.txt");

      List<int> seatIds = GetSeatIdsFromPartition(binarySpacePartitions);
      Console.WriteLine($"Highest SeatID: {seatIds.OrderByDescending(seatId => seatId).FirstOrDefault()}");

      int mySeatId = GetMySeatIdFromSeatIds(seatIds);
      Console.WriteLine($"My SeatID is: {mySeatId}");
    }

    private static int GetMySeatIdFromSeatIds(List<int> seatIds)
    {
      seatIds = CreateSortedSeatIdList(seatIds);

      return seatIds.Where((currentSeatId, i) => seatIds[i + 1] - currentSeatId > 1)
                    .Select(currentSeatId => currentSeatId + 1)
                    .FirstOrDefault();
    }

    private static List<int> CreateSortedSeatIdList(List<int> seatIds)
    {
      return seatIds.OrderBy(seatId => seatId)
                    .ToList();
    }

    private static List<int> GetSeatIdsFromPartition(string[] binarySpacePartitions)
    {
      List<int> seatIds = new List<int>();

      foreach (string partition in binarySpacePartitions)
      {
        string planeRows = GetPlaneRowsFromPartition(partition);
        string planeColumns = GetPlaneColumsFromPartition(partition);

        PlaneSearchCriteria rowCriteria = new PlaneSearchCriteria(0, 127, 'F', 'B');
        PlaneSearchCriteria columnCritria = new PlaneSearchCriteria(0, 7, 'L', 'R');

        int planeRow = FindRegion(planeRows, rowCriteria);
        int planeColumn = FindRegion(planeColumns, columnCritria);

        seatIds.Add(CalculateSeatId(planeRow, planeColumn));
      }

      return seatIds;
    }

    private static int CalculateSeatId(int planeRow, int planeColumn)
    {
      return planeRow * 8 + planeColumn;
    }

    private static string GetPlaneColumsFromPartition(string partition)
    {
      return new string(partition.Where(partitionSymbol => Equals(partitionSymbol, 'L') || Equals(partitionSymbol, 'R'))
                                 .ToArray());
    }

    private static string GetPlaneRowsFromPartition(string partition)
    {
      return new string(partition.Where(partitionSymbol => Equals(partitionSymbol, 'F') || Equals(partitionSymbol, 'B'))
                                 .ToArray());
    }

    private static int FindRegion(string partition, PlaneSearchCriteria planeSearchCriteria)
    {
      int min = planeSearchCriteria.Min;
      int max = planeSearchCriteria.Max;

      for (int i = 0; i < partition.Length; ++i)
      {
        int difference = max - min;

        if (Equals(partition[i], planeSearchCriteria.MinIdentifier) && difference == 1)
        {
          return min;
        }

        if (Equals(partition[i], planeSearchCriteria.MaxIdentifier) && difference == 1)
        {
          return max;
        }

        if (Equals(partition[i], planeSearchCriteria.MinIdentifier))
        {
          max = (int)Math.Floor((max + min) / 2m);
        }
        else if (Equals(partition[i], planeSearchCriteria.MaxIdentifier))
        {
          min = (int)Math.Ceiling((max + min) / 2m);
        }
      }

      return -1;
    }
  }
}
