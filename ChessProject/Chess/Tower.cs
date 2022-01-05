using BoardChess;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }
        public override string ToString()
        {
            return "T ";
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
            //Tower movements
            //Up
            pos.SetValues(Position.Row - 1, Position.Colunm);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row = pos.Row - 1;
            }

            //Down
            pos.SetValues(Position.Row + 1, Position.Colunm);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row = pos.Row + 1;
            }

            //Right
            pos.SetValues(Position.Row, Position.Colunm + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Colunm = pos.Colunm + 1;
            }

            //Left
            pos.SetValues(Position.Row, Position.Colunm - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Colunm = pos.Colunm - 1;
            }
            return validMovements;
        }
    }
}
