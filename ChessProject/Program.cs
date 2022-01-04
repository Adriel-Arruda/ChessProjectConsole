using BoardChess;
namespace ChessProject
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Screen.BoardPrint(board);
        }
    }
}