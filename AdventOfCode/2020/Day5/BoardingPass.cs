using System;

namespace AdventOfCode._2020.Day5
{
    public class BoardingPass
    {
        private const int MAX_ROWS = 127;
        private const int MAX_COLUMNS = 7;

        public SeatDirection[] SeatDirections { get; set; }
        public string Data { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatID { get { return Row != 0 ? (Row * 8) + Column : 0; } }

        public BoardingPass()
        {
            SeatDirections = new SeatDirection[10];
        }

        public void Parse()
        {
            int minRow = 0;
            int maxRow = MAX_ROWS;
            int minColumn = 0;
            int maxColumn = MAX_COLUMNS;
            foreach (SeatDirection direction in SeatDirections)
            {
                switch (direction)
                {
                    case SeatDirection.Front:
                        maxRow = (int)Math.Floor(IntUtils.Avg(minRow, maxRow));
                        break;
                    case SeatDirection.Back:
                        minRow = (int)Math.Ceiling(IntUtils.Avg(minRow, maxRow)) + 1;
                        break;
                    case SeatDirection.Left:
                        maxColumn = (int)Math.Floor(IntUtils.Avg(minColumn, maxColumn));
                        break;
                    case SeatDirection.Right:
                        minColumn = (int)Math.Ceiling(IntUtils.Avg(minColumn, maxColumn)) + 1;
                        break;
                }
            }

            Row = minRow;
            Column = minColumn;
        }

        public override string ToString()
        {
            return $"{Data}: Row {Row}, Column {Column}, Seat ID {SeatID}";
        }
    }
}