using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
  class Program
  {
    static void Main(string[] args)
    {
      List<string> navInstructions = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day12\input.txt")
        .ToList();

      SolvePartOne(navInstructions);
      SolvePartTwo(navInstructions);
    }

    private static void SolvePartTwo(List<string> navInstructions)
    {
      Position waypoint = new Position(10, 1);
      Position shipPosition = new Position(0, 0);

      foreach (var instruction in navInstructions)
      {
        char action = instruction[0];
        int value = int.Parse(instruction.Substring(1));
        switch (action)
        {
          case 'L':
          case 'R':
            waypoint = ChangeWaypointDirection(waypoint, action, value);
            break;
          case 'F':
            shipPosition = MoveShipToWaypoint(shipPosition, waypoint, value);
            break;
          default:
            waypoint = MoveWaypoint(waypoint, action, value);
            break;
        }
      }

      int manhattanDistance = Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
      Console.WriteLine(manhattanDistance);
    }

    private static Position MoveWaypoint(Position waypoint, int cardinal, int distance)
    {
      switch(cardinal)
      {
        case 'N':
          waypoint.Y += distance;
          break;
        case 'E':
          waypoint.X += distance;
          break;
        case 'S':
          waypoint.Y -= distance;
          break;
        default:
          waypoint.X -= distance;
          break;
      }

      return waypoint;
    }

    private static Position ChangeWaypointDirection(Position waypoint, char rotation, int degrees)
    {
      int turns = CountRotations(degrees);
      while (turns > 0)
      {
        if (rotation == 'L')
        {
          waypoint = RotateWaypointCounterClockwise(waypoint);
        }
        else
        {
          waypoint = RotateWaypointClockwise(waypoint);
        }

        --turns;
      }

      return waypoint;
    }

    private static Position RotateWaypointClockwise(Position waypoint)
    {
      int temp = waypoint.X;

      waypoint.X = waypoint.Y;
      waypoint.Y = -temp;

      return waypoint;
    }

    private static Position RotateWaypointCounterClockwise(Position waypoint)
    {
      int temp = waypoint.X;

      waypoint.X = -waypoint.Y;
      waypoint.Y = temp;

      return waypoint;
    }

    private static Position MoveShipToWaypoint(Position shipPosition, Position waypoint, int distance)
    {
      shipPosition.X += waypoint.X * distance;
      shipPosition.Y += waypoint.Y * distance;
      return shipPosition;
    }

    private static void SolvePartOne(List<string> navInstructions)
    {
      char currentDirection = 'E';
      Position shipPosition = new Position(0, 0);
      foreach (var instruction in navInstructions)
      {
        char action = instruction[0];
        int value = int.Parse(instruction.Substring(1));
        switch (action)
        {
          case 'L':
          case 'R':
            currentDirection = ChangeDirection(currentDirection, action, value);
            break;
          case 'F':
            shipPosition = MoveShip(shipPosition, currentDirection, value);
            break;
          default:
            shipPosition = MoveShip(shipPosition, action, value);
            break;
        }
      }

      int manhattanDistance = Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
      Console.WriteLine(manhattanDistance);
    }

    private static Position MoveShip(Position shipPosition, char currentDirection, int distanceToMove)
    {
      switch (currentDirection)
      {
        case 'N':
          shipPosition.Y += distanceToMove;
          return shipPosition;
        case 'E':
          shipPosition.X += distanceToMove;
          return shipPosition;
        case 'S':
          shipPosition.Y -= distanceToMove;
          return shipPosition;
        case 'W':
          shipPosition.X -= distanceToMove;
          return shipPosition;
        default:
          return shipPosition;
      }
    }

    private static char ChangeDirection(char currentDirection, char rotation, int degrees)
    {
      int turns = CountRotations(degrees);
      while (turns > 0)
      {
        if (rotation == 'L')
        {
          currentDirection = RotateShipCounterClockwise(currentDirection);
        }
        else
        {
          currentDirection = RotateShipClockwise(currentDirection);
        }
        --turns;
      }

      return currentDirection;
    }

    private static char RotateShipCounterClockwise(char currentDirection)
    {
      switch (currentDirection)
      {
        case 'N':
          return 'W';
        case 'W':
          return 'S';
        case 'S':
          return 'E';
        default:
          return 'N';
      }
    }

    private static char RotateShipClockwise(char currentDirection)
    {
      switch (currentDirection)
      {
        case 'N':
          return 'E';
        case 'E':
          return 'S';
        case 'S':
          return 'W';
        default:
          return 'N';
      }
    }

    private static int CountRotations(int degrees)
    {
      return degrees / 90;
    }
  }
}
