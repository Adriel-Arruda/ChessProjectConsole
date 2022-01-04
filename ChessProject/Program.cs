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
                Board board = new Board(8, 8);

                board.PutPiece(new Tower(board, Color.Preta), new Position(0, 0));
                board.PutPiece(new King(board, Color.Preta), new Position(0, 0));
                board.PutPiece(new Tower(board, Color.Preta), new Position(1, 3));
                board.PutPiece(new King(board, Color.Preta), new Position(2, 4));

                Screen.BoardPrint(board);
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