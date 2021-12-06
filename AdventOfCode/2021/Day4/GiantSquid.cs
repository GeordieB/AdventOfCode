using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021.Day4
{
    public class GiantSquid
    {
        public static void PlayBingo()
        {
            var rawData = ReadFile();
            var game = BuildGame(rawData);

            game.Play();
            var winner = game.Boards.Where(p => p.Winner).FirstOrDefault();
            Console.WriteLine($"The winning board is Board #{winner.Index} with Final Score: {winner.FinalScore}");

            var lastWinner = game.CowardPlay();
            Console.WriteLine($"The last winning board is Board #{lastWinner.Index} with Final Score: {lastWinner.FinalScore}");
        }

        private static BingoGame BuildGame(List<string> rawData)
        {
            var game = new BingoGame();
            game.DrawnNumbers = rawData.FirstOrDefault().Split(",").ToList();

            game.Boards = new List<Board>();
            var boardNumber = -1;
            foreach (var data in rawData.Skip(1))
            {
                var board = new Board(boardNumber + 2);
                var row = new List<string>();
                var column = new List<string>();

                if (string.IsNullOrEmpty(data))
                {
                    game.Boards.Add(board);
                    boardNumber++;
                    continue;
                }

                board = game.Boards.ElementAt(boardNumber);

                var numbers = data.Split(" ").Where(p => !string.IsNullOrEmpty(p)).ToArray();
                row.AddRange(numbers);

                board.Rows.Add(row);
            }
            foreach (var board in game.Boards)
            {
                board.Columns = board.InitializeColumns();
            }
            return game;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_GIANT_SQUID).ToList();
        }
    }

    public class BingoGame
    {
        public List<string> DrawnNumbers { get; set; }
        public List<Board> Boards { get; set; }

        public void Play()
        {
            bool winnerFound = false;
            foreach (var drawnNumber in DrawnNumbers)
            {
                if (winnerFound)
                    break;

                foreach (var board in Boards)
                {
                    board.MarkBoard(drawnNumber);
                    if (board.Winner)
                    {
                        winnerFound = true;
                        board.FinalScore = board.CalculateScore(drawnNumber);
                        break;
                    }
                }
            }
        }

        public Board CowardPlay()
        {
            Board lastWinner = null;
            foreach (var drawnNumber in DrawnNumbers)
            {
                foreach (var board in Boards.Where(p => !p.Winner))
                {
                    board.MarkBoard(drawnNumber);
                    if (board.Winner)
                    {
                        lastWinner = board;
                        board.FinalScore = board.CalculateScore(drawnNumber);
                    }
                }
            }

            return lastWinner;
        }
    }

    public class Board
    {
        public int Index { get; set; }
        public List<List<string>> Rows { get; set; } = new List<List<string>>();
        public List<List<string>> Columns { get; set; }
        public int FinalScore { get; set; }

        public bool Winner { get; set; }

        public Board(int index)
        {
            Index = index;
        }

        public void MarkBoard(string number)
        {
            foreach (var row in Rows)
            {
                row.Remove(number);
                if (!row.Any())
                    Winner = true;
            }
            foreach (var column in Columns)
            {
                column.Remove(number);
                if (!column.Any())
                    Winner = true;
            }
        }

        public int CalculateScore(string winningNumber)
        {
            if (!Winner)
                return 0;

            var allNumbers = Columns.SelectMany(p => p.Select(x => x)).ToList();
            allNumbers.AddRange(Rows.SelectMany(p => p.Select(x => x)).ToList());
            allNumbers = allNumbers.Distinct().ToList();

            var sum = allNumbers.Select(p => int.Parse(p)).Sum();
            var winningNumAsInt = int.Parse(winningNumber);

            Console.WriteLine($"Sum: {sum}, Winning Number: {winningNumAsInt}");
            return sum * winningNumAsInt;
        }

        public List<List<string>> InitializeColumns()
        {
            var _columns = new List<List<string>>();
            if (!Rows.Any())
                return _columns;

            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = 0; j < Rows[i].Count; j++)
                {
                    var column = new List<string>();
                    if (i != 0)
                        column = _columns.ElementAt(j);
                    
                    column.Add(Rows[i][j]);

                    if (i == 0)
                        _columns.Add(column);
                }
            }

            return _columns;
        }
    }
}