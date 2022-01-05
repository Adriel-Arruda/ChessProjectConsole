using BoardChess;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board{ get; private set; }
        public bool finish;
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }

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

        public void Play(Position origin, Position destiny)
        {
            MakeMovement(origin, destiny);
            shift++;
            PlayerChange();

        }

        public void ValidPlayOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in position!");
            }
            if(currentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The piece is not yours!");
            }
            if (!Board.Piece(pos).ThereValidMovement())
            {
                throw new BoardException("There are no valid moves!");
            }
        }
        public void ValidPlayDestinyPosition(Position origin, Position destiny)
        {
            if (Board.Piece(origin).CanMoveTo(destiny)){
                throw new BoardException("Target position is invalid!");
            }
        }

        private void PlayerChange()
        {
            if (currentPlayer == Color.Branca)
            {
                currentPlayer = Color.Preta;
            }
            else
            {
                currentPlayer = Color.Branca;
            }
        }

        private void PutPiecesMatch()
        {

            Board.PutPiece(new Tower(Board, Color.Preta), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.Preta), new ChessPosition('e', 1).ToPosition());

            Board.PutPiece(new Tower(Board, Color.Branca), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Branca), new ChessPosition('e', 8).ToPosition());

        }
    }
}
