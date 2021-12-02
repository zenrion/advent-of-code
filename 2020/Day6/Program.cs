using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] declarations = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day6\input.txt");

      List<FlightGroup> groups = CreateGroupsFromDeclarations(declarations);
      CalculateAnswersFromDeclarations(groups);

      int sumOfAnswers = GetTotalAnswersCountFromGroups(groups);
      int sumOfQuestionsWhereAllAnswered = GetTotalQuestionCountFromGroupsWhereAllAnswered(groups);

      Console.WriteLine("For each group, count the number of questions to which anyone answered \"yes\".");
      Console.WriteLine($"What is the sum of those counts?\n{sumOfAnswers}");

      Console.WriteLine();

      Console.WriteLine("For each group, count the number of questions to which everyone answered \"yes\".");
      Console.Write($"What is the sum of those counts?\n{sumOfQuestionsWhereAllAnswered}");
    }
    
    private static int GetTotalQuestionCountFromGroupsWhereAllAnswered(List<FlightGroup> groups)
    {
      CountQuestionsWhereAllAnswered(groups);
      return groups.Sum(group => group.QuestionsAnsweredCount);
    }

    private static int GetTotalAnswersCountFromGroups(List<FlightGroup> groups)
    {
      return groups.Sum(group => group.Answers);
    }

    private static void CountQuestionsWhereAllAnswered(List<FlightGroup> groups)
    {
      foreach (FlightGroup group in groups)
      {
        HashSet<char> questions = BuildSetOfQuestions(group);
        foreach (char question in questions)
        {
          bool everyoneSaidYesToQuestion = group.Declarations.All(declaration => declaration.Contains(question));
          if (everyoneSaidYesToQuestion)
          {
            ++group.QuestionsAnsweredCount;
          }
        }
      }
    }

    private static HashSet<char> BuildSetOfQuestions(FlightGroup group)
    {
      var questions = new HashSet<char>();

      foreach (string declaration in group.Declarations)
      {
        for (int i = 0; i < declaration.Length; ++i)
        {
          if (!questions.Contains(declaration[i]))
          {
            questions.Add(declaration[i]);
          }
        }
      }

      return questions;
    }

    private static void CalculateAnswersFromDeclarations(List<FlightGroup> groups)
    {
      foreach (FlightGroup group in groups)
      {
        var uniqueAnswers = new HashSet<char>();
        foreach (string declaration in group.Declarations)
        {
          for (int i = 0; i < declaration.Length; ++i)
          {
            if (!uniqueAnswers.Contains(declaration[i]))
            {
              uniqueAnswers.Add(declaration[i]);
              ++group.Answers;
            }
          }
        }
      }
    }

    private static List<FlightGroup> CreateGroupsFromDeclarations(string[] declarations)
    {
      List<FlightGroup> groups = new List<FlightGroup>();

      FlightGroup group = new FlightGroup();
      for (int i = 0; i < declarations.Length; ++i)
      {
        if (string.IsNullOrWhiteSpace(declarations[i]))
        {
          groups.Add(group);
          group = new FlightGroup();
        }
        else
        {
          group.Declarations.Add(declarations[i]);
        }
      }

      if (group.Declarations.Count > 0)
      {
        groups.Add(group);
      }

      return groups;
    }
  }
}
