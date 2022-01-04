using BoardChess;
using Chess;

namespace ChessProject
{
    public static class Program
    {
        public static void Main(string[] args) 
        {
            ChessPosition chessPosition = new ChessPosition('a', 1);
            Console.WriteLine(chessPosition);
            Console.WriteLine(chessPosition.ToPosition());
        
            Console.ReadLine();
        }
    }
}