using BoardChess;

namespace BoardChess
{
    abstract class Piece
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

        public void MoveAmountIncrement()
        {
            MoveAmount++;
        }

        public abstract bool[,] ValidMovements();
    }
}
