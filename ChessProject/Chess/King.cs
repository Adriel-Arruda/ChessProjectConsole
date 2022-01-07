using BoardChess;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K ";
        }
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool CastlingTowerTest(Position pos)
        {
            Piece piece = Board.Piece(pos);
            return piece == null || piece is Tower && MoveAmount ==0;
        }

        public override bool[,] ValidMovements()
        {
            bool[,] validMovements = new bool[Board.Rows, Board.Colunms];

            Position pos = new Position(0, 0);
            //King movements
            //Up
            pos.SetValues(Position.Row - 1, Position.Colunm);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //NE
            pos.SetValues(Position.Row - 1, Position.Colunm+1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //Rigth
            pos.SetValues(Position.Row, Position.Colunm+1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //SE
            pos.SetValues(Position.Row + 1, Position.Colunm + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            
            //Down
            pos.SetValues(Position.Row + 1, Position.Colunm);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            //SW
            pos.SetValues(Position.Row + 1, Position.Colunm -1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //Left
            pos.SetValues(Position.Row, Position.Colunm - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }
            //NW
            pos.SetValues(Position.Row - 1, Position.Colunm - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                validMovements[pos.Row, pos.Colunm] = true;
            }

            // Castling
                      
            if(MoveAmount == 0 && !match.check)
            {
                //Kingside
                Position towerPos1 = new Position(Position.Row, Position.Colunm + 3);
                if (CastlingTowerTest(towerPos1))
                {
                    Position p1 = new Position(Position.Row, Position.Colunm +1);
                    Position p2 = new Position(Position.Row, Position.Colunm + 2);
                    if(Board.Piece(p1)==null && Board.Piece(p2) == null)
                    {
                        validMovements[Position.Row, Position.Colunm + 2] = true;
                    }
                }

                //Queenside
                Position towerPos2 = new Position(Position.Row, Position.Colunm - 4);
                if (CastlingTowerTest(towerPos2))
                {
                    Position p1 = new Position(Position.Row, Position.Colunm - 1);
                    Position p2 = new Position(Position.Row, Position.Colunm - 2);
                    Position p3 = new Position(Position.Row, Position.Colunm - 3);
                    if(Board.Piece(p1)==null && Board.Piece(p2)==null && Board.Piece(p3) == null)
                    {
                        validMovements[Position.Row, Position.Colunm - 2] = true;
                    }
                }
            }


            return validMovements;
        }
    }
}
