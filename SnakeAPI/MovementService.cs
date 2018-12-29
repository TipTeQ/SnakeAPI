using System.Linq;

namespace SnakeAPI
{
    public static class MovementService
    {
        public static void MoveAgentSnake(ISnakeAgent agent, int move)
        {
            
            switch (move)
            {
                case 1: // Turn left
                    GoLeft(agent);
                    break;

                case 2: // Turn right
                    GoRight(agent);
                    break;

                default: // Go straight
                    GoForward(agent);
                    break;
            }
        }

        private static void GoLeft(ISnakeAgent agent)
        {
            var parts = agent.Snake.Parts;
            var head = parts[0];
            var part1 = parts[1];
            var newCoordinate = new Coordinate(0, 0);

            if (head.Column == part1.Column)
            {
                if (head.Row < part1.Row)
                {
                    newCoordinate.Column = head.Column - 1;
                }
                else
                {
                    newCoordinate.Column = head.Column + 1;
                }
                newCoordinate.Row = head.Row;
            }
            else
            {
                if (head.Column < part1.Column)
                {
                    newCoordinate.Row = head.Row + 1;
                }
                else
                {
                    newCoordinate.Row = head.Row - 1;
                }
                newCoordinate.Column = head.Column;
            }

            parts.Insert(0, newCoordinate);

            if (agent.Snake.AppleEaten)
            {
                agent.Snake.AppleEaten = false;
                return;
            }

            var tail = parts[parts.Count - 1];
            var part2 = parts[parts.Count - 2];

            if (tail.Column == part2.Column)
            {
                if (tail.Row < part2.Row)
                {
                    tail.Row++;
                }
                else
                {
                    tail.Row--;
                }
            }
            else
            {
                if (tail.Column < part2.Column)
                {
                    tail.Column++;
                }
                else
                {
                    tail.Column--;
                }
            }

            if (parts.Where(p => p.Row == tail.Row && p.Column == tail.Column).Count() > 1)
            {
                parts.Remove(tail);
            }
        }

        private static void GoRight(ISnakeAgent agent)
        {
            var parts = agent.Snake.Parts;
            var head = parts[0];
            var part1 = parts[1];
            var newCoordinate = new Coordinate(0, 0);

            if (head.Column == part1.Column)
            {
                if (head.Row < part1.Row)
                {
                    newCoordinate.Column = head.Column + 1;
                }
                else
                {
                    newCoordinate.Column = head.Column - 1;
                }
                newCoordinate.Row = head.Row;
            }
            else
            {
                if (head.Column < part1.Column)
                {
                    newCoordinate.Row = head.Row - 1;
                }
                else
                {
                    newCoordinate.Row = head.Row + 1;
                }
                newCoordinate.Column = head.Column;
            }

            parts.Insert(0, newCoordinate);
            
            if (agent.Snake.AppleEaten)
            {
                agent.Snake.AppleEaten = false;
                return;
            }

            var tail = parts[parts.Count - 1];
            var part2 = parts[parts.Count - 2];

            if (tail.Column == part2.Column)
            {
                if (tail.Row < part2.Row)
                {
                    tail.Row++;
                }
                else
                {
                    tail.Row--;
                }
            }
            else
            {
                if (tail.Column < part2.Column)
                {
                    tail.Column++;
                }
                else
                {
                    tail.Column--;
                }
            }

            if (parts.Where(p => p.Row == tail.Row && p.Column == tail.Column).Count() > 1)
            {
                parts.Remove(tail);
            }
        }

        private static void GoForward(ISnakeAgent agent)
        {
            var parts = agent.Snake.Parts;
            var head = parts[0];
            var part1 = parts[1];

            if (head.Column == part1.Column)
            {
                if (head.Row < part1.Row)
                {
                    head.Row--;
                }
                else
                {
                    head.Row++;
                }
            }
            else
            {
                if (head.Column < part1.Column)
                {
                    head.Column--;
                }
                else
                {
                    head.Column++;
                }
            }

            if (agent.Snake.AppleEaten)
            {
                agent.Snake.AppleEaten = false;
                return;
            }

            var tail = parts[parts.Count - 1];
            var part2 = parts[parts.Count - 2];

            if (tail.Column == part2.Column)
            {
                if (tail.Row < part2.Row)
                {
                    tail.Row++;
                }
                else
                {
                    tail.Row--;
                }
            }
            else
            {
                if (tail.Column < part2.Column)
                {
                    tail.Column++;
                }
                else
                {
                    tail.Column--;
                }
            }

            if (parts.Where(p => p.Row == tail.Row && p.Column == tail.Column).Count() > 1)
            {
                parts.Remove(tail);
            }
        }
    }
}
