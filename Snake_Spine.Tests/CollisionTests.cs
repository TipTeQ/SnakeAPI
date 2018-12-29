using NUnit.Framework;
using SnakeAPI;
using SnakeAPI.Agents;
using System.Collections.Generic;

namespace Snake_Spine.Tests
{
    public class CollisionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SnakesMoveHeadsToSameLocation_BothShouldDie()
        {
            // Common head location (3,3)
            var snake1 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(2, 3),
                    new Coordinate(1, 3)
                }
            };
            var agent1 = new GoStraightAgent(snake1, "Player1");

            var snake2 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(4, 3),
                    new Coordinate(5, 3)
                }
            };
            var agent2 = new GoStraightAgent(snake2, "Player2");

            var game = new SnakeGame(16, 16, 4);
            game.InitialiseNewGame(new List<ISnakeAgent> { agent1, agent2 });
            game.Iterate();

            Assert.IsTrue(agent1.Snake.Dead);
            Assert.IsTrue(agent2.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToOwnBody_SnakeShouldDie()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(1, 3),
                    new Coordinate(2, 3),
                    new Coordinate(2, 4),
                    new Coordinate(0, 4),
                    new Coordinate(0, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToBodyOfSnake_MovingSnakeShouldDie()
        {
            var snake1 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(1, 1),
                    new Coordinate(9, 1)
                }
            };
            var agent1 = new GoStraightAgent(snake1, "Player1");

            var snake2 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(3, 2),
                    new Coordinate(3, 3)
                }
            };
            var agent2 = new GoStraightAgent(snake2, "Player2");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent1, agent2 });
            snakeGame.Iterate();

            Assert.IsFalse(agent1.Snake.Dead);
            Assert.IsTrue(agent2.Snake.Dead);
        }

        [Test]
        public void SnakeMovesToPositionOfOtherSnakesOldTail_SnakeShouldSurvive()
        {
            var snake1 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(1, 1),
                    new Coordinate(9, 1)
                }
            };
            var agent1 = new GoStraightAgent(snake1, "Player1");

            var snake2 = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(9, 2),
                    new Coordinate(9, 3)
                }
            };
            var agent2 = new GoStraightAgent(snake2, "Player2");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent1, agent2 });
            snakeGame.Iterate();

            Assert.IsFalse(agent1.Snake.Dead);
            Assert.IsFalse(agent2.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToTopBorder_SnakeShouldDie()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 5),
                    new Coordinate(1, 5)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToLeftBorder_SnakeShouldDie()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(5, 0),
                    new Coordinate(5, 1)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToBottomBorder_SnakeShouldDie()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(15, 0),
                    new Coordinate(14, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);
        }

        [Test]
        public void SnakeMovesInToRightBorder_SnakeShouldDie()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 15),
                    new Coordinate(0, 14)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);
        }
    }
}
