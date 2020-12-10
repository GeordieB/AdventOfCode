using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day4
{
    public class PassportProcessing
    {
        public static void FindValidPassports()
        {
            List<string> rawData = ReadFile();
            List<Passport> passports = new List<Passport>();

            string valueString = string.Empty;
            foreach (string data in rawData)
            {
                if (string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(valueString))
                {
                    ParsePassport(valueString, passports);
                    valueString = string.Empty;
                }
                else
                    valueString += $" {data}";
            }
            ParsePassport(valueString, passports);

            Console.WriteLine($"Total number of passports: {passports.Count}");
            Console.WriteLine($"Total number of valid passports: {passports.Count(p => p.IsValid)}");
        }

        private static void ParsePassport(string data, List<Passport> list)
        {
            Passport passport = new Passport(data);
            list.Add(passport);
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_PASSPORT_PROCESSING).ToList();
        }
    }
}