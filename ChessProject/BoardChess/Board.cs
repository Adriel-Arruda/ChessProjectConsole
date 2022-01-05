using BoardChess;
namespace BoardChess
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Colunms { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int rows, int colunms)
        {
            Rows = rows;
            Colunms = colunms;
            Pieces = new Piece[Rows, Colunms];
        }

        public Piece Piece(int row, int colunm)
        {
            return Pieces[row, colunm];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Colunm];
        }
        public bool ThereIsAPiece(Position position)
        {
            PositionIsValid(position);
            return Piece(position) != null;
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (ThereIsAPiece(position))
            {
                throw new BoardException("There is already a piece in this position!");
            }
            Pieces[position.Row, position.Colunm] = piece;
            piece.Position = position;
        }
        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece aux = Piece(position);
            Pieces[position.Row, position.Colunm] = null;
            return aux;
        }
        public bool PositionIsValid(Position position)
        {
            if (position.Row < 0 || position.Colunm < 0 || position.Row >= Rows || position.Colunm >= Colunms)
            {
                return false;
            }
            return true;
        }
        public void ValidatePosition(Position position)
        {
            if (!PositionIsValid(position))
            {
                throw new BoardException("Invalid position!");
            }
        }

    }
}
