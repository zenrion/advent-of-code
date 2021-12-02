using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/* 
 * HUGE CREDIT TO:
 * Mitchell van Manen
 * https://github.com/mavanmanen
 * 
 * Heavily inspired from:
 * https://github.com/mavanmanen/AdventOfCode/blob/master/AdventOfCode.Y2020/Day07.cs
 * 
 * My lack of experience in C# (Specifically LINQ) was preventing me from completing the puzzle.
 * Seeing his code finally allowed me to wrap my head around the idea that I was trying to implement.
 */

namespace Day7
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] luggageProcessingRules = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day7\input.txt");
      BagRules bagRules = ParseLuggageProcessingRulesIntoBags(luggageProcessingRules);

      int numOfBagsThatCanContainShinyBag = CountBagsThatCanContainTargetBag(bagRules, "shiny gold");
      Console.Write($"How many bag colors can eventually contain at least one shiny gold bag?\n {numOfBagsThatCanContainShinyBag}");

      Console.WriteLine();

      int numOfRequiredBagsInsideShinyGoldBag = CountBagsInsideShinyBag(bagRules, "shiny gold");
      Console.WriteLine($"How many individual bags are required inside your single shiny gold bag?\n{numOfRequiredBagsInsideShinyGoldBag}");
    }

    private static int CountBagsInsideShinyBag(BagRules bagRules, string targetColor)
    {
      return bagRules.Map[targetColor].Sum(bag => bag.Count + (bag.Count * CountBagsInsideShinyBag(bagRules, bag.Description)));
    }

    private static int CountBagsThatCanContainTargetBag(BagRules bagRules, string targetColor)
    {
      return bagRules.Map.Keys.Count(key => CanContainBag(bagRules, key, targetColor));
    }

    private static bool CanContainBag(BagRules bagRules, string bagDescription, string targetColor)
    {
      return bagRules.Map[bagDescription].Any(bag => bag.Description == targetColor
                                                  || CanContainBag(bagRules, bag.Description, targetColor));
    }

    private static BagRules ParseLuggageProcessingRulesIntoBags(string[] luggageProcessingRules)
    {
      Regex parentBagRegex = new Regex(@"^(?<parent_bag>[a-z ]+) bags contain (?<bags>(((\d+[a-z ]+)|no other) bags?(, )?)*)\.$");

      var bagRulesDictionary = luggageProcessingRules.Select(luggageProcessingRule => parentBagRegex.Match(luggageProcessingRule))
                                                     .ToDictionary(
                                                       k => k.Groups["parent_bag"].Value,
                                                       v => ParseBag(v));

      return new BagRules() { Map = bagRulesDictionary };
    }

    private static List<Bag> ParseBag(Match parentRegexLine)
    {
      Regex childBagRegex = new Regex(@"^(?<count>\d+) (?<description>[a-z ]+) bags?$");

      string childBag = parentRegexLine.Groups["bags"].Value.Trim();

      if (string.Equals(childBag, "no other bags"))
      {
        return new List<Bag>();
      }
      else
      {
        return childBag.Split(',')
                .Select(line => childBagRegex.Match(line.Trim()))
                .Select(regexMatch => new Bag()
                {
                  Description = regexMatch.Groups["description"].Value,
                  Count = int.Parse(regexMatch.Groups["count"].Value)
                })
                .ToList();
      }
    }
  }
}
