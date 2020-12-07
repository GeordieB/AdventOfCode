using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day3
{
    public class CrossedWires
    {
        private const string UP = "U";
        private const string DOWN = "D";
        private const string RIGHT = "R";
        private const string LEFT = "L";

        private static string[] _wires;
        private static WirePoint _currentPoint;
        private static int _currentNumSteps;
        private static Dictionary<WirePoint, List<WirePointMarker>> _grid = new Dictionary<WirePoint, List<WirePointMarker>>();

        public static void FindIntersectingWires()
        {
            _wires = FileUtils.ReadFile(Const.FILE_CROSSED_WIRES);

            Run();

            int fewestSteps;
            WirePoint closest = FindClosestCrossedPoint(out fewestSteps);
            Console.WriteLine($"Closest Manhattan Distance: {closest.ManhattanDistance()}");
            Console.WriteLine($"Fewest Steps to Intersection: {fewestSteps}");
        }

        private static void Run()
        {
            for (int i = 0; i < _wires.Length; i++)
            {
                var wire = _wires[i];
                if (string.IsNullOrEmpty(wire))
                    continue;

                Console.WriteLine($"Wire: {wire}");

                string[] instructions = wire.Split(',');
                _currentPoint = WirePoint.Origin;
                _currentNumSteps = 0;

                foreach (string instruction in instructions)
                {
                    RunInstruction(instruction, i);
                }
            }
        }

        private static void RunInstruction(string instruction, int wireIndex)
        {
            string direction = instruction.Substring(0, 1);
            int numSpaces = instruction.Substring(1).AsInt();

            Console.WriteLine($"Running Instruction: {instruction}");
            for (int i = 0; i < numSpaces; i++)
            {
                _currentNumSteps++;
                switch (direction)
                {
                    case UP:
                        _currentPoint.Up();
                        AddPointToGrid(wireIndex);
                        break;
                    case DOWN:
                        _currentPoint.Down();
                        AddPointToGrid(wireIndex);
                        break;
                    case RIGHT:
                        _currentPoint.Right();
                        AddPointToGrid(wireIndex);
                        break;
                    case LEFT:
                        _currentPoint.Left();
                        AddPointToGrid(wireIndex);
                        break;
                }
            }
        }

        private static void AddPointToGrid(int wireIndex)
        {
            int numSteps = _currentNumSteps;
            if (_grid.Any(p => p.Key.Equals(_currentPoint)))
            {
                var cell = _grid.FirstOrDefault(p => p.Key.Equals(_currentPoint));
                if(!cell.Value.Any(p => p.WireIndex == wireIndex))
                    cell.Value.Add(new WirePointMarker(wireIndex, numSteps));
            }
            else
            {
                _grid.Add(new WirePoint(_currentPoint.X, _currentPoint.Y),
                    new List<WirePointMarker>() { new WirePointMarker(wireIndex, numSteps) });
            }
        }

        private static WirePoint FindClosestCrossedPoint(out int fewestSteps)
        {
            var crossedPaths = _grid.Where(p => p.Value.Count() > 1);
            var closestManhattanDistance = crossedPaths.Select(p => p.Key).FirstOrDefault();

            Console.WriteLine($"Found {crossedPaths.Count()} intersections");
            if (crossedPaths.Count() > 1)
            {
                foreach (WirePoint wirePoint in crossedPaths.Select(p => p.Key))
                {
                    if (wirePoint.ManhattanDistance() < closestManhattanDistance.ManhattanDistance())
                        closestManhattanDistance = wirePoint;
                }
            }

            var steps = crossedPaths.Select(p => p.Value.Sum(x => x.Steps)).ToList();
            fewestSteps = steps.Min();

            return closestManhattanDistance;
        }
    }
}