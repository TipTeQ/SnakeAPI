using System.Collections.Generic;
using System.Linq;

namespace SnakeAPI
{
    public static class CollisionDetector
    {
        public static void ProcessCollisions(List<ISnakeAgent> agents, int numberOfRows, int numberOfColumns)
        {
            var collisionData = GetCollisionData(agents);

            CheckForCollisions(agents, collisionData, numberOfRows, numberOfColumns);
        }

        private static List<CollisionData> GetCollisionData(List<ISnakeAgent> agents)
        {
            var collisionData = new List<CollisionData>();

            foreach (var agent in agents)
            {
                var snake = agent.Snake;

                if (snake.Dead) continue;

                var headRow = snake.Parts[0].Row;
                var headColumn = snake.Parts[0].Column;

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
                                collisionData.Add(new CollisionData { Coordinate = new Coordinate(part1.Row, column), Owner = agent.PlayerName, DeadlyToSelf = !(part1.Row == headRow && column == headColumn && partIndex == 0) });
                            }
                        }
                        else
                        {
                            for (var column = part2.Column; column <= part1.Column; column++)
                            {
                                collisionData.Add(new CollisionData { Coordinate = new Coordinate(part1.Row, column), Owner = agent.PlayerName, DeadlyToSelf = !(part1.Row == headRow && column == headColumn && partIndex == 0) });
                            }
                        }
                    }
                    else
                    {
                        if (part1.Row < part2.Row)
                        {
                            for (var row = part1.Row; row <= part2.Row; row++)
                            {
                                collisionData.Add(new CollisionData { Coordinate = new Coordinate(row, part1.Column), Owner = agent.PlayerName, DeadlyToSelf = !(row == headRow && part1.Column == headColumn && partIndex == 0) });
                            }
                        }
                        else
                        {
                            for (var row = part2.Row; row <= part1.Row; row++)
                            {
                                collisionData.Add(new CollisionData { Coordinate = new Coordinate(row, part1.Column), Owner = agent.PlayerName, DeadlyToSelf = !(row == headRow && part1.Column == headColumn && partIndex == 0) });
                            }
                        }
                    }
                }
            }

            return collisionData;
        }

        private static void CheckForCollisions(List<ISnakeAgent> agents, List<CollisionData> collisionData, int numberOfRows, int numberOfColumns)
        {
            foreach (var agent in agents)
            {
                if (agent.Snake.Head.Column < 0 || agent.Snake.Head.Row < 0 || agent.Snake.Head.Row == numberOfRows || agent.Snake.Head.Column == numberOfColumns)
                {
                    agent.SnakeDead();
                    continue;
                }

                var snake = agent.Snake;
                var head = snake.Parts[0];

                var collisionDetected = collisionData.Any(c => c.Coordinate.Row == head.Row && c.Coordinate.Column == head.Column && (c.Owner != agent.PlayerName || c.DeadlyToSelf));
                if (collisionDetected)
                {
                    agent.SnakeDead();
                }
            }
        }
    }

    public class CollisionData
    {
        public string Owner { get; set; }
        public Coordinate Coordinate { get; set; }
        public bool DeadlyToSelf { get; set; }
    }
}
