using AdventOfCode.Day6;
using System;

namespace AdventOfCode._2019.Day3
{
    public class WirePoint : Point
    {
        public static WirePoint Origin => new WirePoint();

        public WirePoint() : base(0, 0)
        {

        }

        public WirePoint(int x, int y) :
            base(x, y)
        {

        }

        public void Up() => Y++;
        public void Up(int numSpaces) => Y = +numSpaces;

        public void Down() => Y--;
        public void Down(int numSpaces) => Y = -numSpaces;

        public void Left() => X--;
        public void Left(int numSpaces) => X = -numSpaces;

        public void Right() => X++;
        public void Right(int numSpaces) => X = +numSpaces;

        public int ManhattanDistance() => Math.Abs(X - Origin.X) + Math.Abs(Y - Origin.Y);
    }
}