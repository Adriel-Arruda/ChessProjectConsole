using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardChess;

namespace Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B ";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] ValidMovements()
        {
            bool[,] validMovements = new bool[Board.Rows, Board.Colunms];

            Position pos = new Position(0, 0);
            //Bishop movements
            //NE
            pos.SetValues(Position.Row - 1, Position.Colunm + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Colunm + 1);
            }

            //SE
            pos.SetValues(Position.Row + 1, Position.Colunm + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Colunm + 1);
            }

            //SW
            pos.SetValues(Position.Row + 1, Position.Colunm - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Colunm - 1);
            }

            //NW
            pos.SetValues(Position.Row -1, Position.Colunm - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Colunm -1);
            }
            return validMovements;
        }
    }
}
