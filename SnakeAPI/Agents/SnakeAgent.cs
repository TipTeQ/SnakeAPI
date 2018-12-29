using System.Collections.Generic;

namespace SnakeAPI.Agents
{
    public class SnakeAgent : BaseSnakeAgent
    {
        public const int Forward = 0;
        public const int Left = 1;
        public const int Right = 2;

        public SnakeAgent(Snake snake, string playerName)
            : base(snake, playerName)
        {
        }

        public override int ChooseMove(List<Snake> snakes, Apple apple)
        {
            var mySnake = snakes[0];
            var head = mySnake.Head;
            var part = mySnake.Parts[1];

            var direction = DetermineDirection(head, part);

            if (head.Row < apple.Position.Row)
            {
                if (direction != "S")
                {
                    if (direction == "W") return Left;
                    if (direction == "E") return Right;
                    return Right;
                }
                return Forward;
            }

            if (head.Row > apple.Position.Row)
            {
                if (direction != "N")
                {
                    if (direction == "W") return Right;
                    if (direction == "E") return Left;
                    return Left;
                }
                return Forward;
            }

            if (head.Column < apple.Position.Column)
            {
                if (direction != "E")
                {
                    if (direction == "N") return Right;
                    if (direction == "S") return Left;
                    return Left;
                }
                return Forward;
            }

            if (head.Column > apple.Position.Column)
            {
                if (direction != "W")
                {
                    if (direction == "N") return Left;
                    if (direction == "S") return Right;
                    return Right;
                }
                return Forward;
            }

            return Forward;
        }

        private string DetermineDirection(Coordinate head, Coordinate part)
        {
            if (head.Column == part.Column)
            {
                return head.Row < part.Row ? "N" : "S";
            }
            else
            {
                return head.Column < part.Column ? "W" : "E";
            }
        }
    }
}
