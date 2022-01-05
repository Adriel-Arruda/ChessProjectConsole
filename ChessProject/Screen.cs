using BoardChess;
using Chess;

namespace ChessProject
{
    class Screen
    {
        public static void BoardPrint(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Colunms; j++)
                {
                    Screen.PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();

            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void BoardPrint(Board board, bool[,] validPositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Colunms; j++)
                {
                    if(validPositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j)); 
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = originalBackground;
            Console.WriteLine("  a b c d e f g h");
            
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.Branca)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
            }
        }

        public static ChessPosition ReadPosition()
        {
            string pos = Console.ReadLine();
            char colunm = pos[0];
            int row = int.Parse($"{pos[1]}");

            return new ChessPosition(colunm, row);
        }
    }
}
