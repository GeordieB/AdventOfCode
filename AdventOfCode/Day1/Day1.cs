using System;

namespace AdventOfCode
{
    public class Day1
    {
        /// <summary>
        /// Takes comma seperated list of frequency changes, such as +1, -2, +3, -1
        /// Calculates current frequency
        /// </summary>
        /// <param name="args"></param>
        public static void CalculateFrequency(string[] args)
        {
            int frequency = 0;
            foreach (string change in args)
            {
                frequency = CalculateFrequency(change, frequency);
            }
        }

        /// <summary>
        /// Takes a frequency change and adds it to the current frequency
        /// Change must be a sign (+ or -) followed by a number
        /// </summary>
        /// <param name="change"></param>
        /// <param name="frequency"></param>
        public static int CalculateFrequency(string change, int frequency)
        {
            string sign = change.Substring(0, 1);
            string temp = change.Substring(1).Replace(",", "");
            if (int.TryParse(temp, out int result))
            {
                int oldFrequency = frequency;
                frequency += sign == Const.PLUS ? result : -result;
                Console.WriteLine($"Current frequency {oldFrequency}, change of {sign}{temp}; resulting frequency {frequency},");
            }

            return frequency;
        }
    }
}