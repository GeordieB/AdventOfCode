using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day3
{
    public class TobogganTrajectory
    {
        private static Map Map;

        public static void FindTrees_Part1()
        {
            List<string> rawData = ReadFile();
            Map = new Map(rawData);

            Console.WriteLine($"Total trees encountered: {TraverseTerrain(3, 1)}");
        }
        public static void FindTrees_Part2()
        {
            List<string> rawData = ReadFile();
            Map = new Map(rawData);

            int right1Down1 = TraverseTerrain(1, 1);
            int right3Down1 = TraverseTerrain(3, 1);
            int right5Down1 = TraverseTerrain(5, 1);
            int right7Down1 = TraverseTerrain(7, 1);
            int right1Down2 = TraverseTerrain(1, 2);

            int result = right1Down1 * right3Down1 * right5Down1 * right7Down1 * right1Down2;
            Console.WriteLine($"Total trees encountered multiplied: {result}");
        }

        private static int TraverseTerrain(int right, int down)
        {
            int column = 0;
            int row = 0;
            int count = 0;
            while (row < Map.Terrains.Length)
            {
                row += down;
                column += right;

                if (row < Map.Terrains.Length)
                {
                    if (column >= Map.Terrains[row].Length)
                        column = column - Map.Terrains[row].Length;

                    if (Map.Terrains[row][column] == Terrain.Tree)
                        count++;
                }
            }

            return count;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_TOBOGGAN_TRAJECTORY).ToList();
        }
    }
}