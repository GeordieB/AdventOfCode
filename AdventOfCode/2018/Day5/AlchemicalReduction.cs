using System;

namespace AdventOfCode.Day5
{
    public class AlchemicalReduction
    {
        public static void Reduce()
        {
            string polymer = FileUtils.ReadFile(Const.FILE_POLYMER)[0];
            string reducedPolymer = Reduce(polymer);

            Console.WriteLine($"Resulting polymer is: {reducedPolymer} containing {reducedPolymer.Length} units");
        }

        public static string Reduce(string polymer)
        {
            bool allPairsFound = false;
            string reduced = polymer;
            int startIndex = 0;

            while (!allPairsFound)
            {
                char[] polymerAsChars = reduced.ToCharArray();
                bool firstReactionFound = false;
                string temp = reduced.Substring(0, startIndex);

                for (int i = startIndex; i < polymerAsChars.Length - 1; i++)
                {
                    char first = polymerAsChars[i];
                    char second = polymerAsChars[i + 1];

                    bool reaction = first.ToString().ToLower().Equals(second.ToString().ToLower()) && !first.Equals(second);
                    if (reaction)
                    {
                        if (!firstReactionFound)
                        {
                            startIndex = i - 1 < 0 ? 0 : i - 1;
                            firstReactionFound = true;
                        }
                        i++;
                    }
                    else
                    {
                        temp += first;
                        if (i + 1 == polymerAsChars.Length - 1)
                        {
                            temp += second;
                        }
                    }
                }

                allPairsFound = reduced == temp;
                reduced = temp;
            }

            return reduced;
        }

        /// <summary>
        /// Prone to Stack Overflow
        /// </summary>
        /// <param name="polymer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string ReduceRecusion(string polymer, int startIndex = 0)
        {
            char[] polymerAsChars = polymer.ToCharArray();

            startIndex = startIndex < 0 ? 0 : startIndex;
            for (int i = startIndex; i < polymerAsChars.Length - 1; i++)
            {
                char first = polymerAsChars[i];
                char second = polymerAsChars[i + 1];

                if (first.ToString().ToLower().Equals(second.ToString().ToLower()) && !first.Equals(second))
                {
                    return ReduceRecusion(polymer.Remove(i, 2), i - 1);
                }
            }

            return polymer;
        }
    }
}