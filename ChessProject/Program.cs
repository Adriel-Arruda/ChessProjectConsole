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
                board.PutPiece(new Tower(board, Color.Preta), new Position(1, 3));
                board.PutPiece(new King(board, Color.Preta), new Position(2, 4));

                board.PutPiece(new Tower(board, Color.Branca), new Position(7, 7));
                board.PutPiece(new Tower(board, Color.Branca), new Position(5, 1));
                board.PutPiece(new King(board, Color.Branca), new Position(2, 6));

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