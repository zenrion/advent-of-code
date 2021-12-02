using System;
using System.IO;
using System.Linq;

namespace Day2
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] fileContents = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day2\input.txt");
      PasswordPolicy[] passwordPolicies = ParseFileContents(fileContents);
      Console.WriteLine($"Number of valid passwords for part 1: {GetCountOfValidPasswordsPartOne(passwordPolicies)}");
      Console.WriteLine($"Number of valid passwords for part 2: {GetCountOfValidPasswordsPartTwo(passwordPolicies)}");
    }

    private static int GetCountOfValidPasswordsPartOne(PasswordPolicy[] passwordPolicies)
    {
      int count = 0;
      foreach (PasswordPolicy policy in passwordPolicies)
      {
        if (IsValidPasswordPolicyPartOne(policy))
        {
          ++count;
        }
      }

      return count;
    }

    private static int GetCountOfValidPasswordsPartTwo(PasswordPolicy[] passwordPolicies)
    {
      int count = 0;
      foreach (PasswordPolicy policy in passwordPolicies)
      {
        if (IsValidPasswordPolicyPartTwo(policy))
        {
          ++count;
        }
      }

      return count;
    }

    private static bool IsValidPasswordPolicyPartTwo(PasswordPolicy passwordPolicy)
    {
      if (passwordPolicy.MinOccurence - 1 < 0
       || passwordPolicy.MaxOccurence - 1 > passwordPolicy.Password.Length)
      {
        return false;
      }

      if (passwordPolicy.Password[passwordPolicy.MinOccurence - 1] == passwordPolicy.Letter
       && passwordPolicy.Password[passwordPolicy.MaxOccurence - 1] == passwordPolicy.Letter)
      {
        return false;
      }

      if (passwordPolicy.Password[passwordPolicy.MinOccurence - 1] == passwordPolicy.Letter
       || passwordPolicy.Password[passwordPolicy.MaxOccurence - 1] == passwordPolicy.Letter)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private static bool IsValidPasswordPolicyPartOne(PasswordPolicy passwordPolicy)
    {
      // Get total number of occurences of the letter in the password
      int totalCountOfLetter = passwordPolicy.Password
                                             .Where(letterInPassword => letterInPassword == passwordPolicy.Letter)
                                             .Count();

      if (totalCountOfLetter >= passwordPolicy.MinOccurence
       && totalCountOfLetter <= passwordPolicy.MaxOccurence)
      {
        return true;
      }

      return false;
    }

    private static PasswordPolicy[] ParseFileContents(string[] fileContents)
    {
      PasswordPolicy[] passwordPolicies = new PasswordPolicy[fileContents.Length];
      for (int i = 0; i < fileContents.Length; ++i)
      {
        passwordPolicies[i] = CreatePasswordPolicyFromFileLine(fileContents[i]);
      }

      return passwordPolicies;
    }

    private static PasswordPolicy CreatePasswordPolicyFromFileLine(string fileLine)
    {
      int minOccurence = int.Parse(fileLine.Substring(0, fileLine.IndexOf("-")));
      fileLine = fileLine.Substring(fileLine.IndexOf("-") + 1);

      int maxOccurence = int.Parse(fileLine.Substring(0, fileLine.IndexOf(" ")));
      fileLine = fileLine.Substring(fileLine.IndexOf(" ") + 1);

      char letter = fileLine.Substring(0, fileLine.IndexOf(":"))[0];
      fileLine = fileLine.Substring(fileLine.IndexOf(":") + 1);

      string password = fileLine.Trim();

      return new PasswordPolicy(minOccurence, maxOccurence, letter, password);
    }
  }
}
