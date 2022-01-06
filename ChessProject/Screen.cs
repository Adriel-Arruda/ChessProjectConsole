using BoardChess;
using Chess;
using System.Collections.Generic;

namespace ChessProject
{
    class Screen
    {
        public static void MatchPrint(ChessMatch match)
        {
            BoardPrint(match.Board);
            Console.WriteLine();
            CapturedPiecesPrint(match);
            Console.WriteLine();
            Console.WriteLine("Shift: " + match.shift);
            if (!match.finish)
            {
                Console.WriteLine("Aguardando jogada: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine();
                    Console.WriteLine("----------");
                    Console.WriteLine("| CHECK! |");
                    Console.WriteLine("----------");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + match.currentPlayer);
            }
        }

        

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
                    if (validPositions[i, j])
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
                Console.BackgroundColor = originalBackground;
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = ConsoleColor.White;
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

        private static void CapturedPiecesPrint(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            PrintPieces(match.CapturedPieces(Color.Branca));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintPieces(match.CapturedPieces(Color.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void PrintPieces(HashSet<Piece> conjunct)
        {
            Console.Write("[");
            foreach (Piece piece in conjunct)
            {
                Console.Write(piece + ", ");
            }
            Console.Write("]");
        }
    }
}
