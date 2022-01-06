using BoardChess;
using System.Collections.Generic;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board{ get; private set; }
        public bool finish;
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
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
            PutPiecesMatch();

        }

        public void MakeMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.MoveAmountIncrement();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if(capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public void Play(Position origin, Position destiny)
        {
            MakeMovement(origin, destiny);
            shift++;
            PlayerChange();

        }

        public void ValidPlayOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in position!");
            }
            if(currentPlayer != Board.Piece(pos).Color)
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
            if (Board.Piece(origin).CanMoveTo(destiny) == false){
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
            foreach(Piece p in captured)
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
        private void PutPiecesMatch()
        {
            PutNewPiece('a', 1, new Tower(Board, Color.Branca));
            PutNewPiece('h', 1, new Tower(Board, Color.Branca));
            PutNewPiece('d', 1, new King(Board, Color.Branca));

            PutNewPiece('a', 8, new Tower(Board, Color.Preta));
            PutNewPiece('h', 8, new Tower(Board, Color.Preta));
            PutNewPiece('d', 8, new King(Board, Color.Preta));

        }
    }
}
