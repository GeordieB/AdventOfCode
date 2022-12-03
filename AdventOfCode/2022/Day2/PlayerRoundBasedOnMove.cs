using AdventOfCode._2021.Day7;

namespace AdventOfCode._2022.Day1;

public class PlayerRoundBasedOnMove : PlayerRound
{
    public PlayerRoundBasedOnMove(string opponentMove, string playerMove)
    {
        OpponentMove = opponentMove;
        PlayerMove = playerMove;
    }

    public int CalculateScore()
    {
        RoundResult result = RoundResult.Loss;

        //OpponentMove is Rock
        if(OpponentMove == "A")
        {
            if (PlayerMove == "X")
                result = RoundResult.Draw;
            if (PlayerMove == "Y")
                result = RoundResult.Win;
            if (PlayerMove == "Z")
                result = RoundResult.Loss;
        }

        //OpponentMove is Paper
        if (OpponentMove == "B")
        {
            if (PlayerMove == "X")
                result = RoundResult.Loss;
            if (PlayerMove == "Y")
                result = RoundResult.Draw;
            if (PlayerMove == "Z")
                result = RoundResult.Win;
        }

        //OpponentMove is Scissors
        if (OpponentMove == "C")
        {
            if (PlayerMove == "X")
                result = RoundResult.Win;
            if (PlayerMove == "Y")
                result = RoundResult.Loss;
            if (PlayerMove == "Z")
                result = RoundResult.Draw;
        }

        return GetShapeScore() + GetMoveScore(result);
    }
}
