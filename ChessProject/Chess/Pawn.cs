using System;
using BoardChess;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
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

            if (Color == Color.Branca)
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
            }
            return validMovements;
        }
    }
}
