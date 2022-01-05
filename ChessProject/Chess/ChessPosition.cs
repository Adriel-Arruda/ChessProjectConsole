using BoardChess;
namespace Chess
{
    class ChessPosition
    {
        public char Colunm { get; set; }
        public int Row { get; set; }

        public ChessPosition(char colunm, int row)
        {
            Colunm = colunm;
            Row = row;
        }
        
        public Position ToPosition()
        {
            return new Position(8-Row, Colunm-'a');
        }

        public override string ToString()
        {
            return $"{Colunm}{Row}";
        }
    }
}
