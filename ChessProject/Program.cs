﻿using BoardChess;
namespace ChessProject
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Position position = new Position(3, 4);
            Console.WriteLine(position);
            Board board = new Board(8, 8);
        }
    }
}