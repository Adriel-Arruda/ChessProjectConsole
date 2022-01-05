using BoardChess;

namespace Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }
        public override string ToString()
        {
            return "K ";
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
            //Up
            pos.SetValues(Position.Row - 1, Position.Colunm);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //NE
            pos.SetValues(Position.Row - 1, Position.Colunm+1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Rigth
            pos.SetValues(Position.Row, Position.Colunm+1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //SE
            pos.SetValues(Position.Row + 1, Position.Colunm + 1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            
            //Down
            pos.SetValues(Position.Row + 1, Position.Colunm);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //SW
            pos.SetValues(Position.Row + 1, Position.Colunm -1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //Left
            pos.SetValues(Position.Row, Position.Colunm - 1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //NW
            pos.SetValues(Position.Row - 1, Position.Colunm - 1);
            if (Board.PositionIsValid(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            return validMovements;
        }
    }
}
