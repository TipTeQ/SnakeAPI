using System.Collections.Generic;
using System.Linq;

namespace SnakeAPI
{
    public class Board
    {
        private int _rows, _columns;
        public string[,] _board;

        public Board(int rows, int columns)
        {
            _board = new string[rows, columns];

            _rows = rows;
            _columns = columns;

            for (var row = 0; row < _rows; row++)
            {
                for (var column = 0; column < _columns; column++)
                {
                    _board[row, column] = " ";
                }
            }
        }

        public string[,] GetBoard()
        {
            return _board;
        }

        public void InitialiseSnakes(List<ISnakeAgent> agents)
        {
            foreach (var snake in agents.Select(a => a.Snake))
            {
                DrawSnake(snake);
            }
        }

        public void UpdateSnakes(List<ISnakeAgent> agents)
        {
            for (var row = 0; row < _rows; row++)
            {
                for (var column = 0; column < _columns; column++)
                {
                    _board[row, column] = " ";
                }
            }

            InitialiseSnakes(agents);
        }

        public void UpdateApple(Apple apple)
        {
            _board[apple.Position.Row, apple.Position.Column] = "*";
        }

        public void DrawSnake(Snake snake)
        {
            if (snake.Dead) return;

            for (var partIndex = 0; partIndex < snake.Parts.Count - 1; partIndex++)
            {
                var part1 = snake.Parts[partIndex];
                var part2 = snake.Parts[partIndex + 1];

                if (part1.Row == part2.Row)
                {
                    if (part1.Column < part2.Column)
                    {
                        for (var column = part1.Column; column <= part2.Column; column++)
                        {
                            DrawSnakePart(part1.Row, column, snake);
                        }
                    }
                    else
                    {
                        for (var column = part2.Column; column <= part1.Column; column++)
                        {
                            DrawSnakePart(part1.Row, column, snake);
                        }
                    }
                }
                else
                {
                    if (part1.Row < part2.Row)
                    {
                        for (var row = part1.Row; row <= part2.Row; row++)
                        {
                            DrawSnakePart(row, part1.Column, snake);
                        }
                    }
                    else
                    {
                        for (var row = part2.Row; row <= part1.Row; row++)
                        {
                            DrawSnakePart(row, part1.Column, snake);
                        }
                    }
                }
            }
        }

        private void DrawSnakePart(int row, int column, Snake snake)
        {
            if (row == snake.Head.Row && column == snake.Head.Column)
            {
                _board[row, column] = "H";
            }
            else if (row == snake.Tail.Row && column == snake.Tail.Column)
            {
                _board[row, column] = "T";
            }
            else
            {
                _board[row, column] = "X";
            }
        }

        public void InitialiseApple(Apple apple)
        {
            _board[apple.Position.Row, apple.Position.Column] = "*";
        }

        public bool IsSnakeOrAppleAtLocation(int row, int column)
        {
            return _board[row, column] != " ";
        }
    }
}
