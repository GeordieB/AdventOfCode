using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day1
{
    public class RocketEquation
    {
        public static void CalculateTotalFuel()
        {
            string[] values = FileUtils.ReadFile(Const.FILE_ROCKET_EQUATION);
            double totalFuel = 0;
            foreach (string rawMass in values)
            {
                if (string.IsNullOrEmpty(rawMass))
                    continue;

                double mass = Convert.ToDouble(rawMass);
                totalFuel += CalculateFuel(mass);
            }
            Console.WriteLine($"Total fuel required: {totalFuel}");
        }

        public static void CalculateFullTotalFuel()
        {
            List<string> values = FileUtils.ReadFile(Const.FILE_ROCKET_EQUATION).ToList();
            double totalFuel = 0;
            int index = 0;
            while (index != values.Count())
            {
                string rawMass = values.ElementAt(index);
                index++;

                if (string.IsNullOrEmpty(rawMass))
                    continue;

                double mass = Convert.ToDouble(rawMass);
                double fuel = CalculateFuel(mass);

                if (fuel > 0)
                {
                    values.Add(fuel.ToString());
                    totalFuel += fuel;
                }
            }
            Console.WriteLine($"Total fuel required: {totalFuel}");
        }

        public static double CalculateFuel(double mass)
        {
            return Math.Floor(mass / 3) - 2;
        }
    }
}