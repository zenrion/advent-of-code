using System;
using System.IO;

namespace Day3
{
  class Program
  {
    public static int WidthOfMap { get; set; } = 0;
    public static int HeightOfMap { get; set; } = 0;

    static void Main(string[] args)
    {
      string[] map = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day3\input.txt");

      WidthOfMap = map[0].Length;
      HeightOfMap = map.Length;

      Position pos = new Position(0, 0);

      ulong run = CalculateTreeHits(pos, map, 3, 1);
      Console.WriteLine($"Total from run: {run}");

      ulong secondRun = CalculateTreeHits(pos, map, 1, 1);
      ulong thirdRun = CalculateTreeHits(pos, map, 5, 1);
      ulong fourthRun = CalculateTreeHits(pos, map, 7, 1);
      ulong fifthRun = CalculateTreeHits(pos, map, 1, 2);

      ulong totalFromAllRuns = run * secondRun * thirdRun * fourthRun * fifthRun;
      Console.WriteLine($"Total from all runs: {totalFromAllRuns}");
    }

    private static ulong CalculateTreeHits(Position pos, string[] map, int xDelta, int yDelta)
    {
      ulong numOfTreesHit = 0;

      while (pos.y < HeightOfMap - 1)
      {
        pos.x += xDelta;
        pos.y += yDelta;

        int x = AdjustXPositionToFitMap(pos.x, WidthOfMap);

        if (map[pos.y][x] == '#')
        {
          ++numOfTreesHit;
        }
      }

      return numOfTreesHit;
    }

    private static int AdjustXPositionToFitMap(int x, int widthOfMap)
    {
      if (x < widthOfMap)
      {
        return x;
      }

      while (x >= widthOfMap)
      {
        x -= widthOfMap;
      }

      return x;
    }
  }

  struct Position
  {
    public int x;
    public int y;

    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
    }
  }
}
