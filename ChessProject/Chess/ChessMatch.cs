using BoardChess;
using System.Collections.Generic;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public bool finish;
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool check { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
      

        public ChessMatch()
        {
            Board = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.Branca;
            finish = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            check = false;
            PutPiecesMatch();


        }

        public Piece MakeMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.MoveAmountIncrement();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(piece, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.MoveAmountDecrement();

            if(capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            Board.PutPiece(piece, origin);
        }

        public void ExecutePlay(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeMovement(origin, destiny);
            if (IsInCheck(currentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (IsInCheck(AdversaryColor(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if (TestCheckmate(AdversaryColor(currentPlayer)))
            {
                finish = true;
            }
            else
            {
                shift++;
                PlayerChange();
            }
        }
         

        public void ValidPlayOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in position!");
            }
            if (currentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The piece is not yours!");
            }
            if (!Board.Piece(pos).ThereValidMovement())
            {
                throw new BoardException("There are no valid moves!");
            }
        }
        public void ValidPlayDestinyPosition(Position origin, Position destiny)
        {
            if (Board.Piece(origin).PossibleMovement(destiny) == false)
            {
                throw new BoardException("Target position is invalid!");
            }
        }

        private void PlayerChange()

        {
            if (currentPlayer == Color.Branca)
            {
                currentPlayer = Color.Preta;
            }
            else
            {
                currentPlayer = Color.Branca;
            }
        }
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }


        public void PutNewPiece(char colunm, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(colunm, row).ToPosition());
            pieces.Add(piece);
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException("There is no king on the board!");
            }
            foreach(Piece piece in PiecesInGame(AdversaryColor(color)))
            {
                bool[,] mat = piece.ValidMovements();
                if (mat[king.Position.Row, king.Position.Colunm])
                {
                    return true;
                }
            }
            return false;
        }
        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach(Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.ValidMovements();
                for(int i = 0; i < Board.Rows; i++)
                {
                    for(int j = 0; j < Board.Colunms; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position; 
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MakeMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private void PutPiecesMatch()
        {
            PutNewPiece('a', 1, new Tower(Board, Color.Branca));
            PutNewPiece('b', 1, new Horse(Board, Color.Branca));
            PutNewPiece('c', 1, new Bishop(Board, Color.Branca));
            PutNewPiece('d', 1, new Queen(Board, Color.Branca));
            PutNewPiece('e', 1, new King(Board, Color.Branca));
            PutNewPiece('f', 1, new Bishop(Board, Color.Branca));
            PutNewPiece('g', 1, new Horse(Board, Color.Branca));
            PutNewPiece('h', 1, new Tower(Board, Color.Branca));
            PutNewPiece('a', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('b', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('c', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('d', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('e', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('f', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('g', 2, new Pawn(Board, Color.Branca));
            PutNewPiece('h', 2, new Pawn(Board, Color.Branca));

            PutNewPiece('a', 8, new Tower(Board, Color.Preta));
            PutNewPiece('b', 8, new Horse(Board, Color.Preta));
            PutNewPiece('c', 8, new Bishop(Board, Color.Preta));
            PutNewPiece('d', 8, new Queen(Board, Color.Preta));
            PutNewPiece('e', 8, new King(Board, Color.Preta));
            PutNewPiece('f', 8, new Bishop(Board, Color.Preta));
            PutNewPiece('g', 8, new Horse(Board, Color.Preta));
            PutNewPiece('h', 8, new Tower(Board, Color.Preta));
            PutNewPiece('a', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('b', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('c', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('d', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('e', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('f', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('g', 7, new Pawn(Board, Color.Preta));
            PutNewPiece('h', 7, new Pawn(Board, Color.Preta));
        }

        private Color AdversaryColor(Color Color)
            => Color == Color.Branca ? Color.Preta : Color.Branca;

        private Piece King(Color color)
        {
            foreach(Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
    }
}
