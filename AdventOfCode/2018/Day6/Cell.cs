namespace AdventOfCode.Day6
{
    public class Cell
    {
        public Point Point { get; set; }
        public Coordinate Owner { get; set; }
        public int Iteration { get; set; }
        public bool IsFilled { get { return Iteration != 0; } }
        public bool IsShared { get; set; }

        public Cell(Point point)
        {
            Point = point;
        }

        public void Fill(Coordinate owner, int iteration)
        {
            Owner = owner;
            Iteration = iteration;
            Owner.AddArea();
        }

        public void Shared()
        {
            IsShared = true;
            Owner.RemoveArea();
        }
    }
}