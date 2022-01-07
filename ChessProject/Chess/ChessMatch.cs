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
        public Piece pieceEnPassant { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.White;
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
            //Castling Kingside
            if (piece is King && destiny.Colunm == origin.Colunm + 2)
            {
                Position originTower = new Position(origin.Row, origin.Colunm + 3);
                Position destinyTower = new Position(origin.Row, origin.Colunm + 1);
                Piece tower = Board.RemovePiece(originTower);
                Board.PutPiece(tower, destinyTower);
            }
            //Castling Queenside
            if (piece is King && destiny.Colunm == origin.Colunm - 2)
            {
                Position originTower = new Position(origin.Row, origin.Colunm - 4);
                Position destinyTower = new Position(origin.Row, origin.Colunm - 1);
                Piece tower = Board.RemovePiece(originTower);
                Board.PutPiece(tower, destinyTower);
            }

            //En passant
            if (piece is Pawn)
            {
                if (origin.Colunm != destiny.Colunm && capturedPiece == null)
                {
                    Position pawnPos;
                    if (piece.Color == Color.White)
                    {
                        pawnPos = new Position(destiny.Row + 1, destiny.Colunm);
                    }
                    else
                    {
                        pawnPos = new Position(destiny.Row - 1, destiny.Colunm);
                    }
                    capturedPiece = Board.RemovePiece(pawnPos);
                    captured.Add(capturedPiece);
                }
            }
           return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.MoveAmountDecrement();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            Board.PutPiece(piece, origin);

            //Castling Kingside
            if (piece is King && destiny.Colunm == origin.Colunm + 2)
            {
                Position originTower = new Position(origin.Row, origin.Colunm + 3);
                Position destinyTower = new Position(origin.Row, origin.Colunm + 1);
                Piece tower = Board.RemovePiece(destinyTower);
                tower.MoveAmountDecrement();
                Board.PutPiece(tower, origin);
            }

            //Castling Queenside
            if (piece is King && destiny.Colunm == origin.Colunm - 2)
            {
                Position originTower = new Position(origin.Row, origin.Colunm - 4);
                Position destinyTower = new Position(origin.Row, origin.Colunm - 1);
                Piece tower = Board.RemovePiece(destinyTower);
                Board.PutPiece(tower, originTower);
            }

            //En Passant

            if(piece is Pawn)
            {
                if(origin.Colunm != destiny.Colunm && capturedPiece == pieceEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position pawnPos;
                    if(piece.Color == Color.White)
                    {
                        pawnPos = new Position(3, destiny.Colunm);
                    }
                    else
                    {
                        pawnPos = new Position(4, destiny.Colunm);
                    }
                    Board.PutPiece(pawn, pawnPos);
                }
            }
        }

        public void ExecutePlay(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeMovement(origin, destiny);
            if (IsInCheck(currentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            Piece piece = Board.Piece(destiny);

            //Promotion
            if(piece is Pawn)
            {
                if((piece.Color == Color.White && destiny.Row == 0) ||(piece.Color == Color.Black && destiny.Row == 7))
                {
                    piece = Board.RemovePiece(destiny);
                    pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.PutPiece(queen, destiny);
                    pieces.Add(queen);
                }
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

            //En Passant
            if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                pieceEnPassant = piece;
            }
            else
            {
                pieceEnPassant = null;
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
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
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
            foreach (Piece piece in PiecesInGame(AdversaryColor(color)))
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
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.ValidMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Colunms; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MakeMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            if (!testCheck)
                            {
                                UndoMovement(origin, destiny, capturedPiece);
                                return false;
                            }
                            UndoMovement(origin, destiny, capturedPiece);
                        }
                    }
                }
            }
            return true;
        }
        private void PutPiecesMatch()
        {
            //
            PutNewPiece('a', 1, new Tower(Board, Color.White));
            PutNewPiece('b', 1, new Horse(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Horse(Board, Color.White));
            PutNewPiece('h', 1, new Tower(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PutNewPiece('a', 8, new Tower(Board, Color.Black));
            PutNewPiece('b', 8, new Horse(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Horse(Board, Color.Black));
            PutNewPiece('h', 8, new Tower(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

        private Color AdversaryColor(Color Color)
            => Color == Color.White ? Color.Black : Color.White;

        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
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
