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

        public void MoveAmountDecrement()
        {
            MoveAmount--;
        }

        public bool ThereValidMovement()
        {
            bool[,] mat = ValidMovements();
            for(int i = 0; i < Board.Rows; i++)
            {
                for(int j =0; j < Board.Colunms; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return ValidMovements()[pos.Row, pos.Colunm];
        }

        public abstract bool[,] ValidMovements();
        
    }
}
