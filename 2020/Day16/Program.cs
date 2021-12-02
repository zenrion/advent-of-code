using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
  class Program
  {
    private static readonly Regex ValueRangeRegex = new Regex(@"(?<value_range>\d+-\d+)|(?<your_ticket>your ticket:)|(?<nearby_tickets>nearby tickets:)");

    static void Main(string[] args)
    {
      List<string> inputFile = File.ReadAllLines(@"..\..\..\input.txt")
        .ToList();

      SolvePartOne(inputFile);
    }

    private static void SolvePartOne(List<string> inputFile)
    {
      var rangesFromFile = new List<string>();
      var yourTicketFromFile = new List<string>();
      var nearbyTicketsFromFile = new List<string>();

      var fields = new List<string>();
      var validNearbyTickets = new List<string>();

      bool parseYourTickets = false;
      bool parseNearbyTickets = false;

      foreach (var line in inputFile)
      {
        if (ValueRangeRegex.Matches(line).Count() > 0)
        {
          if (ValueRangeRegex.Match(line).Groups["your_ticket"].Success)
          {
            parseYourTickets = true;
            parseNearbyTickets = false;
          }
          else if (ValueRangeRegex.Match(line).Groups["nearby_tickets"].Success)
          {
            parseYourTickets = false;
            parseNearbyTickets = true;
          }
          else
          {
            fields.Add(line.Substring(0, line.IndexOf(':')));

            rangesFromFile.AddRange(ValueRangeRegex.Matches(line)
                                                   .Select(match => match.Value));
          }
        }
        else if (parseYourTickets && !string.IsNullOrWhiteSpace(line))
        {
          yourTicketFromFile.Add(line);
        }
        else if (parseNearbyTickets && !string.IsNullOrWhiteSpace(line))
        {
          nearbyTicketsFromFile.Add(line);
        }
      }

      var validNumbers = new HashSet<int>();
      foreach (string valueRange in rangesFromFile)
      {
        int[] startEndIndices = valueRange.Split('-')
                                          .Select(number => int.Parse(number))
                                          .ToArray();

        for (int i = startEndIndices[0]; i <= startEndIndices[1]; ++i)
        {
          validNumbers.Add(i);
        }
      }

      var invalidNumbers = new List<int>();
      foreach (var nearbyTicket in nearbyTicketsFromFile)
      {
        List<int> ticketValues = nearbyTicket.Split(',')
                                             .Select(number => int.Parse(number))
                                             .ToList();

        bool validTicket = true;
        foreach (var ticketValue in ticketValues)
        {
          if (!validNumbers.Contains(ticketValue))
          {
            invalidNumbers.Add(ticketValue);

            if (validTicket)
            {
              validTicket = false;
            }
          }
        }

        if (validTicket)
        {
          validNearbyTickets.Add(nearbyTicket);
        }
      }

      int totalInvalidNumbers = invalidNumbers.Sum();

      Console.WriteLine(totalInvalidNumbers);
    }
  }
}
