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
                    Console.Clear();
                    Screen.BoardPrint(match.Board);
                    Console.WriteLine(); 

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();

                    bool[,] validPositions = match.Board.Piece(origin).ValidMovements();

                    Console.Clear();
                    Screen.BoardPrint(match.Board, validPositions);

                    Console.WriteLine();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadPosition().ToPosition();
                    match.MakeMovement(origin, destiny);
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