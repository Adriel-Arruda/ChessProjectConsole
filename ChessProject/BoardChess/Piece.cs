using BoardChess;

namespace BoardChess
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color{ get; set; }

        public int MoveAmount { get; set; }
        public Board Board { get; set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MoveAmount = 0;
        }


    }
}
