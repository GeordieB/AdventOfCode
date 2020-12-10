using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day5
{
    public class BinaryBoarding
    {
        public static void DecodeBoardingPass()
        {
            List<string> rawPasses = ReadFile();
            List<BoardingPass> boardingPasses = new List<BoardingPass>();

            foreach (string rawPass in rawPasses)
            {
                BoardingPass boardingPass = GetBoardingPass(rawPass);
                boardingPasses.Add(boardingPass);

                Console.WriteLine($"Boarding Pass: {boardingPass}");
            }
            Console.WriteLine($"\n");
            Console.WriteLine($"Highest Seat ID: {boardingPasses.Max(p => p.SeatID)}");
        }

        public static void FindBoardingPass()
        {
            List<string> rawPasses = ReadFile();
            List<BoardingPass> boardingPasses = new List<BoardingPass>();

            foreach (string rawPass in rawPasses)
            {
                BoardingPass boardingPass = GetBoardingPass(rawPass);
                boardingPasses.Add(boardingPass);
            }

            int mySeatID = 0;
            for (int i = boardingPasses.Min(p => p.SeatID) + 1; i < boardingPasses.Max(p => p.SeatID) - 1; i++)
            {
                if (boardingPasses.Any(p => p.SeatID == i + 1) && boardingPasses.Any(p => p.SeatID == i - 1)
                    && boardingPasses.All(p => p.SeatID != i))
                {
                    mySeatID = i;
                    break;
                }
            }

            Console.WriteLine($"My SeatID: {mySeatID}");
        }

        private static BoardingPass GetBoardingPass(string rawPass)
        {
            BoardingPass boardingPass = new BoardingPass();
            boardingPass.Data = rawPass;

            int index = 0;
            foreach (char character in rawPass)
            {
                switch (character)
                {
                    case 'F':
                        boardingPass.SeatDirections[index] = SeatDirection.Front;
                        break;
                    case 'B':
                        boardingPass.SeatDirections[index] = SeatDirection.Back;
                        break;
                    case 'L':
                        boardingPass.SeatDirections[index] = SeatDirection.Left;
                        break;
                    case 'R':
                        boardingPass.SeatDirections[index] = SeatDirection.Right;
                        break;
                }
                index++;
            }
            boardingPass.Parse();

            return boardingPass;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_BINARY_BOARDING).ToList();
        }
    }
}