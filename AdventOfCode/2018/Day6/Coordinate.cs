namespace AdventOfCode.Day6
{
    public class Coordinate
    {
        private int _id;
        public int Id { get { return _id; } }
        private Point _point;
        public Point Point { get { return _point; } }
        private int _area;
        public int Area { get { return _area; } }
        public bool IsInfinite { get; set; }

        public Coordinate(int id, Point point)
        {
            _id = id;
            _point = point;
            _area = 0;
        }

        public void AddArea()
        {
            _area++;
        }

        public void RemoveArea()
        {
            _area--;
        }

        public string ToString()
        {
            return $"Coordinate #{_id}: {_point.X}, {_point.Y} has an Area of {_area}";
        }
    }
}