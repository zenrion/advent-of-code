using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day11
{
  class Program
  {
    static void Main(string[] args)
    {
      List<string> seatingLayout = File.ReadAllLines(Path.GetFullPath(@"..\..\..\input.txt"))
        .ToList();

      SolvePartOne(seatingLayout);
      SolvePartTwo(seatingLayout);
    }

    private static void SolvePartTwo(List<string> seatingLayout)
    {
      List<string> previousSeatingLayout = seatingLayout.ToList();

      bool isSeatLayoutUnchanged = false;
      while (!isSeatLayoutUnchanged)
      {
        List<string> parsedSeatingLayout = ParseVisibleSeatingLayout(previousSeatingLayout);

        isSeatLayoutUnchanged = parsedSeatingLayout.SequenceEqual(previousSeatingLayout);

        previousSeatingLayout = parsedSeatingLayout.ToList();
      }

      Console.WriteLine("How many seats end up occupied?");
      int sum = previousSeatingLayout.Sum(row => row.Count(seat => seat == '#'));
      Console.WriteLine(sum);
    }

    private static List<string> ParseVisibleSeatingLayout(List<string> seatingLayout)
    {
      List<string> newSeatingLayout = seatingLayout.ToList();

      for (int row = 0; row < seatingLayout.Count; ++row)
      {
        for (int col = 0; col < seatingLayout[row].Length; ++col)
        {
          if (seatingLayout[row][col] != '.')
          {
            int numberOfOccupiedVisibleSeats = CountOccupiedVisibleSeats(seatingLayout, row, col);
            if (numberOfOccupiedVisibleSeats == 0)
            {
              newSeatingLayout[row] = ChangeSeat(newSeatingLayout[row], col, '#');
            }
            else if (numberOfOccupiedVisibleSeats >= 5)
            {
              newSeatingLayout[row] = ChangeSeat(newSeatingLayout[row], col, 'L');
            }
          }
        }
      }

      return newSeatingLayout;
    }

    private static int CountOccupiedVisibleSeats(List<string> seatingLayout, int row, int col)
    {
      return CountDiagonalVisibleSeats(seatingLayout, row, col)
           + CountColumnVisibleSeats(seatingLayout, row, col) 
           + CountRowVisibleSeats(seatingLayout, row, col);
    }

    private static int CountRowVisibleSeats(List<string> seatingLayout, int row, int col)
    {
      int total = 0;

      int j = col - 1;
      while (j >= 0)
      {
        if (seatingLayout[row][j] != '.')
        {
          if (seatingLayout[row][j] == 'L')
          {
            break;
          }

          if (seatingLayout[row][j] == '#')
          {
            ++total;
            break;
          }
        }

        --j;
      }

      j = col + 1;
      while (j < seatingLayout[row].Length)
      {
        if (seatingLayout[row][j] != '.')
        {
          if (seatingLayout[row][j] == 'L')
          {
            break;
          }

          if (seatingLayout[row][j] == '#')
          {
            ++total;
            break;
          }
        }

        ++j;
      }

      return total;
    }

    private static int CountColumnVisibleSeats(List<string> seatingLayout, int row, int col)
    {
      int total = 0;

      int i = row - 1;
      while (i >= 0)
      {
        if (seatingLayout[i][col] != '.')
        {
          if (seatingLayout[i][col] == 'L')
          {
            break;
          }

          if (seatingLayout[i][col] == '#')
          {
            ++total;
            break;
          }
        }

        --i;
      }

      i = row + 1;
      while (i < seatingLayout.Count)
      {
        if (seatingLayout[i][col] != '.')
        {
          if (seatingLayout[i][col] == 'L')
          {
            break;
          }

          if (seatingLayout[i][col] == '#')
          {
            ++total;
            break;
          }
        }

        ++i;
      }

      return total;
    }

    private static int CountDiagonalVisibleSeats(List<string> seatingLayout, int row, int col)
    {
      int total = 0;

      // Search for top-left diagonal seats.
      int i = row - 1;
      int j = col - 1;
      while (i >= 0 && j >= 0)
      {
        if (seatingLayout[i][j] != '.')
        {
          if (seatingLayout[i][j] == 'L')
          {
            break;
          }

          if (seatingLayout[i][j] == '#')
          {
            ++total;
            break;
          }
        }

        --i;
        --j;
      }

      // Search bottom-right diagonal seats.
      i = row + 1;
      j = col + 1;
      while (i < seatingLayout.Count && j < seatingLayout[row].Length)
      {
        if (seatingLayout[i][j] != '.')
        {
          if (seatingLayout[i][j] == 'L')
          {
            break;
          }

          if (seatingLayout[i][j] == '#')
          {
            ++total;
            break;
          }
        }

        ++i;
        ++j;
      }

      // Search top-right diagonal seats.
      i = row - 1;
      j = col + 1;
      while (i >= 0 && j < seatingLayout[row].Length)
      {
        if (seatingLayout[i][j] != '.')
        {
          if (seatingLayout[i][j] == 'L')
          {
            break;
          }

          if (seatingLayout[i][j] == '#')
          {
            ++total;
            break;
          }
        }

        --i;
        ++j;
      }

      // Search bottom-left diagonal seats.
      i = row + 1;
      j = col - 1;
      while (i < seatingLayout.Count && j >= 0)
      {
        if (seatingLayout[i][j] != '.')
        {
          if (seatingLayout[i][j] == 'L')
          {
            break;
          }

          if (seatingLayout[i][j] == '#')
          {
            ++total;
            break;
          }
        }

        ++i;
        --j;
      }

      return total;
    }

    private static void SolvePartOne(List<string> seatingLayout)
    {
      List<string> previousSeatingLayout = seatingLayout.ToList();

      bool isSeatLayoutUnchanged = false;
      while (!isSeatLayoutUnchanged)
      {
        List<string> parsedSeatingLayout = ParseSeatingLayout(previousSeatingLayout);

        isSeatLayoutUnchanged = parsedSeatingLayout.SequenceEqual(previousSeatingLayout);

        previousSeatingLayout = parsedSeatingLayout.ToList();
      }

      Console.WriteLine("How many seats end up occupied?");
      int sum = previousSeatingLayout.Sum(row => row.Count(seat => seat == '#'));
      Console.WriteLine(sum);
    }

    private static List<string> ParseSeatingLayout(List<string> seatingLayout)
    {
      List<string> newSeatingLayout = seatingLayout.ToList();

      for (int row = 0; row < seatingLayout.Count; ++row)
      {
        List<string> rowsToCheck = seatingLayout
          .Where((seatRow, index) => index == row - 1
                          || index == row + 1)
          .ToList();

        for (int col = 0; col < seatingLayout[row].Length; ++col)
        {
          if (seatingLayout[row][col] != '.')
          {
            int numberOfOccupiedSeats = CountOccupiedAdjacentSeats(seatingLayout[row], rowsToCheck, col);
            if (numberOfOccupiedSeats == 0)
            {
              newSeatingLayout[row] = ChangeSeat(newSeatingLayout[row], col, '#');
            }
            else if (numberOfOccupiedSeats >= 4)
            {
              newSeatingLayout[row] = ChangeSeat(newSeatingLayout[row], col, 'L');
            }
          }
        }
      }

      return newSeatingLayout;
    }

    private static string ChangeSeat(string seatingRow, int position, char newSeat)
    {
      char[] newSeatingRow = seatingRow.ToCharArray();
      newSeatingRow[position] = newSeat;

      return new string(newSeatingRow);
    }

    private static int CountOccupiedAdjacentSeats(string currentRow, List<string> rowsToCheck, int col)
    {
      var characterHits = new Dictionary<char, int>()
      {
        { '#', 0 },
        { '.', 0 },
        { 'L', 0 }
      };

      if (col - 1 >= 0 && col + 1 < currentRow.Length) // prev, curr, next col can be checked
      {
        ++characterHits[currentRow[col - 1]];
        ++characterHits[currentRow[col + 1]];

        foreach (string adjacentRow in rowsToCheck)
        {
          ++characterHits[adjacentRow[col - 1]];
          ++characterHits[adjacentRow[col]];
          ++characterHits[adjacentRow[col + 1]];
        }
      }
      else if (col - 1 >= 0) // prev, curr col can be checked
      {
        ++characterHits[currentRow[col - 1]];

        foreach (string adjacentRow in rowsToCheck)
        {
          ++characterHits[adjacentRow[col - 1]];
          ++characterHits[adjacentRow[col]];
        }
      }
      else  // current, next col only
      {
        ++characterHits[currentRow[col + 1]];

        foreach (string adjacentRow in rowsToCheck)
        {
          ++characterHits[adjacentRow[col + 1]];
          ++characterHits[adjacentRow[col]];
        }
      }

      return characterHits['#'];
    }
  }
}
