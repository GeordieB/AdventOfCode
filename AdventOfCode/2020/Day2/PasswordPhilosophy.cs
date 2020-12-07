using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day2
{
    public class PasswordPhilosophy
    {
        public static void FindSledValidPasswords()
        {
            List<string> ruleSets = ReadFile();
            List<SledRuleSet> parsedRuleSets = ParseSledRuleSets(ruleSets);

            Console.WriteLine($"Total valid passwords: {parsedRuleSets.Count(p => p.IsValid)}");
        }
        public static void FindTobogganValidPasswords()
        {
            List<string> ruleSets = ReadFile();
            List<TobogganRuleSet> parsedRuleSets = ParseTobogganRuleSets(ruleSets);

            Console.WriteLine($"Total valid passwords: {parsedRuleSets.Count(p => p.IsValid)}");
        }

        private static List<SledRuleSet> ParseSledRuleSets(List<string> ruleSets)
        {
            List<SledRuleSet> sledRuleSets = new List<SledRuleSet>();
            foreach (string ruleSet in ruleSets)
            {
                if (string.IsNullOrEmpty(ruleSet))
                    continue;

                string[] data = ruleSet.Split(':');
                string[] rule = data[0].Split('-');

                SledRuleSet sledRuleSet = new SledRuleSet();
                sledRuleSet.Min = rule[0].AsInt();
                sledRuleSet.Max = rule[1].Substring(0, rule[1].Length - 1).Trim().AsInt();
                sledRuleSet.Letter = char.Parse(rule[1].Substring(rule[1].Length - 1).Trim());
                sledRuleSet.Password = data[1].Trim().ToLower();

                sledRuleSets.Add(sledRuleSet);
            }

            return sledRuleSets;
        }

        private static List<TobogganRuleSet> ParseTobogganRuleSets(List<string> ruleSets)
        {
            List<TobogganRuleSet> tobogganRuleSets = new List<TobogganRuleSet>();
            foreach (string ruleSet in ruleSets)
            {
                if (string.IsNullOrEmpty(ruleSet))
                    continue;

                string[] data = ruleSet.Split(':');
                string[] rule = data[0].Split('-');

                TobogganRuleSet tobogganRuleSet = new TobogganRuleSet();
                tobogganRuleSet.FirstIndex = rule[0].AsInt();
                tobogganRuleSet.SecondIndex = rule[1].Substring(0, rule[1].Length - 1).Trim().AsInt();
                tobogganRuleSet.Letter = char.Parse(rule[1].Substring(rule[1].Length - 1).Trim());
                tobogganRuleSet.Password = data[1].Trim().ToLower();

                tobogganRuleSets.Add(tobogganRuleSet);
            }

            return tobogganRuleSets;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_PASSWORD_PHILOSOPHY).ToList();
        }
    }
}