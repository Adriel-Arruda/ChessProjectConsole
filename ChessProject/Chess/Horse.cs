
using BoardChess;

namespace Chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "H ";
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

            //King movements
            //Up-rigth
            pos.SetValues(Position.Row - 1, Position.Colunm - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Rigth-up
            pos.SetValues(Position.Row - 2, Position.Colunm - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Rigth-down
            pos.SetValues(Position.Row - 2 , Position.Colunm + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Down-rigth
            pos.SetValues(Position.Row - 1, Position.Colunm + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Down-left
            pos.SetValues(Position.Row + 2, Position.Colunm + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Left-down
            pos.SetValues(Position.Row + 1, Position.Colunm + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //Left-up
            pos.SetValues(Position.Row + 1, Position.Colunm - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //up-left
            pos.SetValues(Position.Row + 2, Position.Colunm - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            return validMovements;
        }
    }
}
