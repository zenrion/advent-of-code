using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] file = File.ReadAllLines(@"D:\Documents\GitHub\advent-of-code-2020\Day4\input.txt");
      List<Passport> passports = CreatePassportsFromFile(file);

      Console.WriteLine($"Number of valid passports: {CountValidPassports(passports)}");
    }

    private static int CountValidPassports(List<Passport> passports)
    {
      int totalValidPassports = 0;
      foreach (Passport passport in passports)
      {
        if (IsValidPassport(passport))
        {
          ++totalValidPassports;
        }
      }

      return totalValidPassports;
    }

    private static bool IsValidPassport(Passport passport)
    {
      bool arePropertiesValid = passport.GetType()
                                        .GetProperties()
                                        .All(property => {
                                          if (!string.Equals(property.Name, "CountryId", StringComparison.InvariantCultureIgnoreCase)
                                          && property.GetValue(passport) != null)
                                          {
                                            return true;
                                          }
                                          else if (string.Equals(property.Name, "CountryId", StringComparison.InvariantCultureIgnoreCase))
                                          {
                                            return true;
                                          }
                                          else
                                          {
                                            return false;
                                          }
                                        });

      if (!arePropertiesValid)
      {
        return false;
      }

      if (ArePassportYearsValid(passport)
       && IsHeightValid(passport.Height)
       && IsHairColorValid(passport.HairColor)
       && IsEyeColorValid(passport.EyeColor)
       && IsPassportIdValid(passport.PassportId))
      {
        return true;
      }

      return false;
    }

    private static bool ArePassportYearsValid(Passport passport)
    {
      if (IsBirthYearValid(passport.BirthYear)
       && IsIssueYearValid(passport.IssueYear)
       && IsExpirationYearValid(passport.ExpirationYear))
      {
        return true;
      }

      return false;
    }

    private static bool IsPassportIdValid(string passportId)
    {
      const string passportIdRegex = "^([0-9]{9})$";

      return Regex.IsMatch(passportId, passportIdRegex);
    }

    private static bool IsEyeColorValid(string eyeColor)
    {
      HashSet<string> validEyeColors = new HashSet<string>()
      { 
        "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
      };

      if (validEyeColors.Contains(eyeColor))
      {
        return true;
      }

      return false;
    }

    private static bool IsHairColorValid(string hairColor)
    {
      const string hairColorRegex = "^(#[0-9a-z]{6})$";
      return Regex.IsMatch(hairColor, hairColorRegex);
    }

    private static bool IsHeightValid(string height)
    {
      const string metricHeightRegex = "^(1[5-8][0-9]|19[0-3])$";
      const string imperialHeightRegex = "^([5-6][0-9]|7[0-6])$";

      if (height.EndsWith("cm"))
      {
        return Regex.IsMatch(height.Substring(0, height.Length - 2), metricHeightRegex);
      }
      else if (height.EndsWith("in"))
      {
        return Regex.IsMatch(height.Substring(0, height.Length - 2), imperialHeightRegex);
      }

      return false;
    }

    private static bool IsExpirationYearValid(string expirationYear)
    {
      const string expirationYearRegex = "^(202[0-9]|2030)$"; // A year between 2020-2030
      return Regex.IsMatch(expirationYear, expirationYearRegex);
    }

    private static bool IsIssueYearValid(string issueYear)
    {
      const string issueYearRegex = "^(201[0-9]|2020)$"; // a number between 2010-2020
      return Regex.IsMatch(issueYear, issueYearRegex);
    }

    private static bool IsBirthYearValid(string birthYear)
    {
      const string birthYearRegex = "^(19[2-9][0-9]|200[0-2])$"; // a number between 1920-2002
      return Regex.IsMatch(birthYear, birthYearRegex);
    }

    private static List<Passport> CreatePassportsFromFile(string[] file)
    {
      List<Passport> passports = new List<Passport>();
      for(int i = 0; i < file.Length; ++i)
      {
        List<string> passportData = new List<string>();
        while (i < file.Length && !string.IsNullOrWhiteSpace(file[i]))
        {
          passportData.Add(file[i]);
          ++i;
        }

        passports.Add(CreatePassport(passportData));
      }

      return passports;
    }

    private static Passport CreatePassport(List<string> passportData)
    {
      Passport passport = new Passport();
      foreach (string line in passportData)
      {
        string modifiedLine = line;
        while (!string.IsNullOrWhiteSpace(modifiedLine))
        {
          bool spaceExists = modifiedLine.IndexOf(" ") > 0;

          string data = string.Empty;
          if (spaceExists)
          {
            data = modifiedLine.Substring(0, modifiedLine.IndexOf(" "));
            modifiedLine = modifiedLine.Substring(modifiedLine.IndexOf(" ") + 1);
          }
          else
          {
            data = modifiedLine.Substring(0, modifiedLine.Length);
            modifiedLine = string.Empty;
          }

          passport = AddValueFromPassportData(passport, data);
        }
      }

      return passport;
    }

    private static Passport AddValueFromPassportData(Passport passport, string data)
    {
      string key = data.Substring(0, data.IndexOf(":"));
      string value = data.Substring(data.IndexOf(":") + 1);

      switch(key)
      {
        case "byr":
          passport.BirthYear = value;
          break;
        case "iyr":
          passport.IssueYear = value;
          break;
        case "eyr":
          passport.ExpirationYear = value;
          break;
        case "hgt":
          passport.Height = value;
          break;
        case "hcl":
          passport.HairColor = value;
          break;
        case "ecl":
          passport.EyeColor = value;
          break;
        case "pid":
          passport.PassportId = value;
          break;
        case "cid":
          passport.CountryId = value;
          break;
      }

      return passport;
    }
  }
}
