
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


    }
}
