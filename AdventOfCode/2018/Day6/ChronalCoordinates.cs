using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day6
{
    public class ChronalCoordinates
    {
        public static void FindLargestArea()
        {
            string[] rawCoordinates = FileUtils.ReadFile(Const.FILE_COORDINATES);
            List<Coordinate> coordinates = Build(rawCoordinates, out Dictionary<Point, Cell> grid);

            Point max = new Point(coordinates.Max(p => p.Point.X), coordinates.Max(p => p.Point.Y));
            Point min = new Point(coordinates.Min(p => p.Point.X), coordinates.Min(p => p.Point.Y));

            bool keepGoing = true;
            int iteration = 1;

            while (keepGoing)
            {
                keepGoing = false;
                foreach (Coordinate coordinate in coordinates)
                {
                    bool filled = false;
                    for (int x = -iteration; x <= iteration; x++)
                    {
                        for (int y = -iteration + Math.Abs(x); y <= iteration - Math.Abs(x); y++)
                        {
                            filled = ProcessCell(x, y, iteration, coordinate, min, max, grid);
                            if (!keepGoing && filled)
                            {
                                keepGoing = true;
                            }
                        }
                    }
                }
                iteration++;
            }
            FindInfinite(coordinates, min, max, grid);
            Console.WriteLine(coordinates.Where(p => !p.IsInfinite).Max(p => p.Area).ToString());
        }

        public static List<Coordinate> Build(string[] rawCoordinates, out Dictionary<Point, Cell> grid)
        {
            int index = 0;
            List<Coordinate> coordinates = new List<Coordinate>();
            grid = new Dictionary<Point, Cell>();

            foreach (string rawCoordinate in rawCoordinates)
            {
                string[] points = rawCoordinate.Split(',');
                if (points.Any() && points.Length == 2)
                {
                    Point point = new Point(points[0].AsInt(), points[1].Trim().AsInt());
                    Coordinate coordinate = new Coordinate(index, point);

                    //Cell cell = new Cell(point);
                    //cell.Fill(coordinate, 0);

                    //grid.Add(point, cell);
                    coordinates.Add(coordinate);

                    index++;
                }
            }

            return coordinates;
        }

        public static bool ProcessCell(int x, int y, int iteration, Coordinate owner,
            Point minBound, Point maxBound, Dictionary<Point, Cell> grid)
        {
            Point point = new Point(owner.Point.X + x, owner.Point.Y + y);
            Cell cell = null;
            if (grid.Any(p => p.Key.X == point.X && p.Key.Y == point.Y))
            {
                cell = grid.FirstOrDefault(p => p.Key.X == point.X && p.Key.Y == point.Y).Value;
                grid.Remove(point);
            }
            else
            {
                cell = new Cell(point);
            }
            grid.Add(point, cell);

            if (point.X < minBound.X || point.Y < minBound.Y ||
                point.X > maxBound.X || point.Y > maxBound.Y)
            {
                return false;
            }

            if (!cell.IsFilled)
            {
                cell.Fill(owner, iteration);
                return true;
            }
            else if (cell.Iteration == iteration)
            {
                cell.Shared();
                return true;
            }

            return false;
        }

        public static void FindInfinite(List<Coordinate> coordinates,
            Point minBound, Point maxBound, Dictionary<Point, Cell> grid)
        {
            for (int x = minBound.X; x <= maxBound.X; x++)
            {
                MarkAsInfinite(x, minBound.Y, coordinates, grid);
                MarkAsInfinite(x, maxBound.Y, coordinates, grid);
            }
            for (int y = minBound.Y; y <= maxBound.Y; y++)
            {
                MarkAsInfinite(minBound.X, y, coordinates, grid);
                MarkAsInfinite(maxBound.X, y, coordinates, grid);
            }
        }
        public static void MarkAsInfinite(int x, int y, List<Coordinate> coordinates,
            Dictionary<Point, Cell> grid)
        {
            Cell cell = grid.FirstOrDefault(p => p.Key.X == x && p.Key.Y == y).Value;
            if (cell != null)
            {
                Coordinate coordinate = coordinates.FirstOrDefault(p => p.Id == cell.Owner.Id);
                coordinate.IsInfinite = true;
            }
        }
    }
}