namespace AdventOfCode.Day6
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object other)
        {
            if (other is Point)
            {
                Point toCompare = other as Point;
                return X == toCompare.X && Y == toCompare.Y;
            }

            return false;
        }
    }
}