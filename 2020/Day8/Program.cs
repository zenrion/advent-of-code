using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day8
{
  class Program
  {
    public static int ACCUMULATOR { get; set; }

    static void Main(string[] args)
    {
      string[] instructionsInput = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day8\input.txt");
      Instruction[] instructions = ParseInstructionsFromFile(instructionsInput);

      HandleInstructions(instructions);
      Console.WriteLine($"What value is in the accumulator?\n{ACCUMULATOR}");

      Console.WriteLine();

      Console.WriteLine($"What is the value of the accumulator after the program terminates?");
      HandleInstructionsWithoutLimitingHits(instructions);
    }

    private static void HandleInstructionsWithoutLimitingHits(Instruction[] instructions)
    {
      int maxInstructionsToRun = 5000;

      for (int i = 0; i < instructions.Length; ++i)
      {
        Instruction[] copyOfInstructions = DeepCopyArray(instructions);

        if (copyOfInstructions[i].Operation == "jmp")
        {
          copyOfInstructions[i].Operation = "nop";
        }
        else if (copyOfInstructions[i].Operation == "nop")
        {
          copyOfInstructions[i].Operation = "jmp";
        }
        else
        {
          continue;
        }

        int j = 0;
        int tries = 0;
        ACCUMULATOR = 0;
        while (j >= 0 && j < copyOfInstructions.Length && tries < maxInstructionsToRun)
        {
          j = HandleInstruction(copyOfInstructions[j], j);

          if (j == copyOfInstructions.Length)
          {
            Console.WriteLine($"WE MADE IT: {ACCUMULATOR}");
            return;
          }

          ++tries;
        }
      }
    }

    private static Instruction[] DeepCopyArray(Instruction[] instructions)
    {
      Instruction[] newInstructions = new Instruction[instructions.Length];

      for (int i = 0; i < instructions.Length; ++i)
      {
        newInstructions[i] = new Instruction()
        {
          Operation = instructions[i].Operation,
          Argument = instructions[i].Argument,
          IsNegative = instructions[i].IsNegative
        };
      }

      return newInstructions;
    }

    private static void HandleInstructions(Instruction[] instructions)
    {
      ACCUMULATOR = 0;

      int[] hits = new int[instructions.Length];

      int i = 0;
      while (!hits.Any(element => element == 2))
      {
        i = HandleInstruction(instructions[i], i);
        ++hits[i];
      }
    }

    private static int HandleInstruction(Instruction instruction, int iterator)
    {
      if (instruction.Operation == "nop")
      {
        return ++iterator;
      }

      if (instruction.IsNegative)
      {
        if (instruction.Operation == "acc")
        {
          ACCUMULATOR -= instruction.Argument;
          return ++iterator;
        }

        if (instruction.Operation == "jmp")
        {
          iterator -= instruction.Argument;
          return iterator;
        }
      }
      else
      {
        if (instruction.Operation == "acc")
        {
          ACCUMULATOR += instruction.Argument;
          return ++iterator;
        }

        if (instruction.Operation == "jmp")
        {
          iterator += instruction.Argument;
          return iterator;
        }
      }

      return iterator;
    }

    private static Instruction[] ParseInstructionsFromFile(string[] instructionsInput)
    {
      Regex instructionRegex = new Regex(@"^(?<operation>[a-z]{3}) ((?<sign>[\+\-])(?<argument>\d+))$");

      return instructionsInput
        .Select(line => instructionRegex.Match(line))
        .Select(regexLine => new Instruction()
        {
          Operation = regexLine.Groups["operation"].Value,
          Argument = int.Parse(regexLine.Groups["argument"].Value),
          IsNegative = regexLine.Groups["sign"].Value == "-"
        })
        .ToArray();
    }
  }
}
