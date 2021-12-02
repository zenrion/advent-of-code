using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
  class Program
  {
    static void Main(string[] args)
    {
      List<string> busScheduleFile = File.ReadAllLines(@"..\..\..\input.txt")
        .ToList();

      SolvePartOne(busScheduleFile);
      SolvePartTwo(busScheduleFile);
    }

    private static void SolvePartTwo(List<string> busScheduleFile)
    {
      List<Bus> busses = ParseBusSchedule(busScheduleFile);
    }

    private static List<Bus> ParseBusSchedule(List<string> busScheduleFile)
    {
      List<Bus> busList = new List<Bus>();

      List<string> busIdsAndNonConstraints = busScheduleFile[1]
        .Split(",")
        .ToList();

      int departureTimestamp = 0;
      foreach(string busIdOrNonConstraint in busIdsAndNonConstraints)
      {
        if (busIdOrNonConstraint != "x")
        {
          busList.Add(new Bus(busIdOrNonConstraint, departureTimestamp));
        }

        ++departureTimestamp;
      }

      return busList;
    }

    private static int CalculateBusIdAndWaitTime(KeyValuePair<int, int> earliestBusIdAndArrivalTime, int timestamp)
    {
      return (earliestBusIdAndArrivalTime.Value - timestamp) * earliestBusIdAndArrivalTime.Key;
    }

    private static int CalculateBusTimeFromOriginToTimestamp(int busId, int timestamp)
    {
      int timeOfNextBus = 0;
      do
      {
        timeOfNextBus += busId;
      }
      while (timeOfNextBus < timestamp);

      return timeOfNextBus;
    }

    private static Dictionary<int, int> CreateBusTimeMapping(List<int> busIds, int timestamp)
    {
      Dictionary<int, int> busTimeMap = new Dictionary<int, int>();
      foreach (int busId in busIds)
      {
        busTimeMap.Add(busId, CalculateBusTimeFromOriginToTimestamp(busId, timestamp));
      }

      return busTimeMap;
    }

    private static void SolvePartOne(List<string> busScheduleFile)
    {
      List<int> busIds = busScheduleFile[1]
        .Split(',')
        .Where(id => id != "x")
        .Select(id => int.Parse(id))
        .ToList();

      int timestamp = int.Parse(busScheduleFile[0]);

      Dictionary<int, int> busTimeMap = CreateBusTimeMapping(busIds, timestamp);
      KeyValuePair<int, int> earliestBusIdAndArrivalTime = busTimeMap
        .OrderBy(pair => pair.Value)
        .First();

      Console.WriteLine("What is the ID of the earliest bus you can take to the airport multiplied by the number of minutes you'll need to wait for that bus?");
      Console.WriteLine(CalculateBusIdAndWaitTime(earliestBusIdAndArrivalTime, timestamp));
    }
  }
}
