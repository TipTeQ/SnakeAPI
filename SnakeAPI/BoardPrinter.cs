using System;

namespace SnakeAPI
{
    public static class BoardPrinter
    {
        public static void PrintBoard(string[,] board)
        {
            for (var row = 0; row < board.GetLength(0); row++)
            {
                var rowString = "";
                for (var column = 0; column < board.GetLength(1); column++)
                {
                    rowString += board[row, column];
                }

                Console.WriteLine(rowString + "|");
            }
            Console.WriteLine("".PadLeft(board.GetLength(1), '-'));
            Console.WriteLine();
        }
    }
}
