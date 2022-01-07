using System;
using BoardChess;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P ";
        }

        public bool ThereIsEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreeHouse(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] ValidMovements()
        {
            bool[,] validMovements = new bool[Board.Rows, Board.Colunms];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Colunm);
                if (Board.PositionIsValid(pos) && FreeHouse(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                pos.SetValues(Position.Row - 2, Position.Colunm);
                if (Board.PositionIsValid(pos) && FreeHouse(pos) && MoveAmount == 0)
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                pos.SetValues(Position.Row - 1, Position.Colunm - 1);
                if (Board.PositionIsValid(pos) && ThereIsEnemy(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }
                
                pos.SetValues(Position.Row - 1, Position.Colunm + 1);
                if (Board.PositionIsValid(pos) && ThereIsEnemy(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                //En Passant

                if(Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Colunm - 1);
                    if(Board.PositionIsValid(left) && ThereIsEnemy(left) && Board.Piece(left) == match.pieceEnPassant)
                    {
                        validMovements[left.Row - 1, left.Colunm] = true;
                    }

                    Position right = new Position(Position.Row, Position.Colunm + 1);
                    if (Board.PositionIsValid(right) && ThereIsEnemy(right) && Board.Piece(right) == match.pieceEnPassant)
                    {
                        validMovements[right.Row - 1, right.Colunm] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Row + 1, Position.Colunm);
                if (Board.PositionIsValid(pos) && FreeHouse(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                pos.SetValues(Position.Row + 2, Position.Colunm);
                if (Board.PositionIsValid(pos) && FreeHouse(pos) && MoveAmount == 0)
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                pos.SetValues(Position.Row + 1, Position.Colunm - 1);
                if (Board.PositionIsValid(pos) && ThereIsEnemy(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Colunm + 1);
                if (Board.PositionIsValid(pos) && ThereIsEnemy(pos))
                {
                    validMovements[pos.Row, pos.Colunm] = true;
                }

                //En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Colunm + 1);
                    if (Board.PositionIsValid(left) && ThereIsEnemy(left) && Board.Piece(left) == match.pieceEnPassant)
                    {
                        validMovements[left.Row + 1, left.Colunm] = true;
                    }

                    Position right = new Position(Position.Row, Position.Colunm - 1);
                    if (Board.PositionIsValid(right) && ThereIsEnemy(right) && Board.Piece(right) == match.pieceEnPassant)
                    {
                        validMovements[right.Row + 1, right.Colunm] = true;
                    }
                }
            }
            return validMovements;
        }
    }
}
