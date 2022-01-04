using System;

namespace BoardChess
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
