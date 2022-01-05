using BoardChess;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board{ get; set; }
        public bool finish;
        private int shift;
        private Color currentPlayer;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.Branca;
            PutPiecesMatch();
            finish = false;

        }

        public void MakeMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.MoveAmountIncrement();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
        }

        private void PutPiecesMatch()
        {

            Board.PutPiece(new Tower(Board, Color.Preta), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.Preta), new ChessPosition('e', 1).ToPosition());


        }
    }
}
