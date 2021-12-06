using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day5
{
    public class HydrothermalVenture
    {
        public static void FindOverlapingLines()
        {
            var rawData = ReadFile();
            var lines = ProcessData(rawData);
            var grid = BuildGrid(lines);

            var count = grid.Count(p => p.Value > 1);
            Console.WriteLine($"There are {count} overlapping lines");
        }

        public static void FindOverlapingLinesWithDiagonals()
        {
            var rawData = ReadFile();

            var lines = ProcessData(rawData, true);
            var grid = BuildGrid(lines);

            var count = grid.Count(p => p.Value > 1);
            Console.WriteLine($"There are {count} overlapping lines");
        }

        private static Dictionary<(int, int), int> BuildGrid(List<Line> lines)
        {
            var gridPoint = new Dictionary<(int, int), int>();
            foreach (var line in lines)
            {
                foreach (var point in line.Points)
                {
                    if (gridPoint.ContainsKey((point.X, point.Y)))
                        gridPoint[(point.X, point.Y)] = gridPoint[(point.X, point.Y)] + 1;
                    else
                        gridPoint[(point.X, point.Y)] = 1;
                }
            }

            return gridPoint;
        }

        private static List<Line> ProcessData(List<string> rawData, bool considerDiagonal = false)
        {
            var lines = new List<Line>();
            foreach (var raw in rawData)
            {
                var points = raw.Replace(" -> ", ",").Split(",");
                var line = new Line(considerDiagonal)
                {
                    Start = new Point(int.Parse(points[0]), int.Parse(points[1])),
                    End = new Point(int.Parse(points[2]), int.Parse(points[3]))
                };

                if (!considerDiagonal)
                {
                    if (line.IsVertical || line.IsHorizontal)
                        lines.Add(line);
                }
                else
                    lines.Add(line);
            }

            return lines;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_HYDROTHERMAL_VENTURE).ToList();
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public List<Point> Points { get { return CalculatePoints(); } }
        public bool IsVertical { get { return Start.X == End.X; } }
        public bool IsHorizontal { get { return Start.Y == End.Y; } }
        public bool ConsiderDiagonal { get; set; }

        public Line(bool considerDiagonal)
        {
            ConsiderDiagonal = considerDiagonal;
        }

        private List<Point> CalculatePoints()
        {
            var points = new List<Point>();
            if(Start == null || End == null)
                return Points;

            if (IsVertical)
                points = GetVerticalPoints(Start, End);
            else if (IsHorizontal)
                points = GetHorizontalPoints(Start, End);
            else if(ConsiderDiagonal)
                points = GetDiagonalPoints(Start, End);

            return points;
        }

        private static List<Point> GetVerticalPoints(Point start, Point end)
        {
            List<Point> points = new List<Point>();

            if (end.Y < start.Y)
            {
                var temp = new Point(start.X, start.Y);
                start = new Point(end.X, end.Y);
                end = new Point(temp.X, temp.Y);
            }
            points.Add(start);

            for (int i = start.Y + 1; i < end.Y; i++)
            {
                if (i != end.Y)
                    points.Add(new Point(start.X, i));
            }
            points.Add(end);

            return points;
        }

        private static List<Point> GetHorizontalPoints(Point start, Point end)
        {
            List<Point> points = new List<Point>();

            if (end.X < start.X)
            {
                var temp = new Point(start.X, start.Y);
                start = new Point(end.X, end.Y);
                end = new Point(temp.X, temp.Y);
            }
            points.Add(start);

            for (int i = start.X + 1; i < end.X; i++)
            {
                if (i != end.X)
                    points.Add(new Point(i, start.Y));
            }
            points.Add(end);

            return points;
        }

        private static List<Point> GetDiagonalPoints(Point start, Point end)
        {
            List<Point> points = new List<Point>();

            if (end.X < start.X)
            {
                var temp = new Point(start.X, start.Y);
                start = new Point(end.X, end.Y);
                end = new Point(temp.X, temp.Y);
            }
            points.Add(start);
            if (start.X == start.Y && end.X == end.Y)
            {
                for (int i = start.X + 1; i < end.X; i++)
                {
                    if (i != end.X)
                        points.Add(new Point(i, i));
                }
            }
            else if (start.X < end.X && start.Y < end.Y)
            {
                var j = start.Y + 1;
                for (int i = start.X + 1; i < end.X; i++)
                {
                    if (i != end.X)
                        points.Add(new Point(i, j));
                    j++;
                }
            }
            else
            {
                var j = start.Y - 1;
                for (int i = start.X + 1; i < end.X; i++)
                {
                    if (i != end.X)
                        points.Add(new Point(i, j));
                    j--;
                }
            }
            points.Add(end);

            return points;
        }
    }
}