
namespace BoardChess
{
    internal class Position
    {
        public int Row { get; set; }
        public int Colunm { get; set; }

        public Position(int row, int colunm)
        {
            Row = row;
            Colunm = colunm;
        }

        public override string ToString()
        {
            return Row + ", " + Colunm;
        }


    }
}
