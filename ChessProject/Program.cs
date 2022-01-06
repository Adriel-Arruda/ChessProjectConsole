using BoardChess;
using Chess;
namespace ChessProject
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finish)
                {
                    try
                    {
                        Console.Clear();
                        Screen.MatchPrint(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        match.ValidPlayOriginPosition(origin);

                        bool[,] validPositions = match.Board.Piece(origin).ValidMovements();

                        Console.Clear();
                        Screen.BoardPrint(match.Board, validPositions);

                        Console.WriteLine();
                        Console.WriteLine("Shift: " + match.shift);
                        Console.WriteLine("Waiting play: " + match.currentPlayer);

                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadPosition().ToPosition();
                        match.ValidPlayDestinyPosition(origin, destiny);
                        
                        match.ExecutePlay(origin, destiny);
                    }
                    catch (BoardException error)
                    {
                        Console.Write(error.Message);
                        Console.ReadLine();
                    }
                }
                Console.ReadLine();
            }
            catch (BoardException error)
            {
                Console.WriteLine(error.Message);
            }
            Console.ReadLine();
        }
    }
}