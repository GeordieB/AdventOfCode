using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class ImproveReduction
    {
        public static void Reduce()
        {
            string polymer = FileUtils.ReadFile(Const.FILE_POLYMER)[0];
            IEnumerable<string> array = polymer.ToCharArray().Select(p => p.ToString().ToLower()).Distinct();

            int shortestReduction = polymer.Length;
            string problematicUnit = "";
            foreach (string unit in array)
            {
                string temp = polymer.ReplaceIgnoreCase(unit, "");
                string reducedPolymer = AlchemicalReduction.Reduce(temp);

                if (reducedPolymer.Length < shortestReduction)
                {
                    shortestReduction = reducedPolymer.Length;
                    problematicUnit = unit;
                }
            }

            Console.WriteLine($"Problematic polymer unit is: {problematicUnit} reduing the polymer to {shortestReduction} units");
        }
    }
}