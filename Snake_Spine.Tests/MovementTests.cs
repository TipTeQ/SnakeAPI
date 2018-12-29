using NUnit.Framework;
using SnakeAPI;
using SnakeAPI.Agents;
using System.Collections.Generic;

namespace Tests
{
    public class MovementTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SnakeWithNoJointsMoveForward_SameRow()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(2, 9),
                    new Coordinate(2, 6)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(2, snake.Parts[0].Row);
            Assert.AreEqual(10, snake.Parts[0].Column);

            Assert.AreEqual(2, snake.Parts[1].Row);
            Assert.AreEqual(7, snake.Parts[1].Column);
        }

        [Test]
        public void SnakeWithNoJointsMoveForward_SameColumn()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(6, 2),
                    new Coordinate(9, 2)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(5, snake.Parts[0].Row);
            Assert.AreEqual(2, snake.Parts[0].Column);

            Assert.AreEqual(8, snake.Parts[1].Row);
            Assert.AreEqual(2, snake.Parts[1].Column);
        }

        [Test]
        public void SnakeWithNoJoinsTurnLeft_SameRow()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(2, 9),
                    new Coordinate(2, 6)
                }
            };
            var agent = new GoLeftAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(1, snake.Parts[0].Row);
            Assert.AreEqual(9, snake.Parts[0].Column);

            Assert.AreEqual(2, snake.Parts[1].Row);
            Assert.AreEqual(9, snake.Parts[1].Column);

            Assert.AreEqual(2, snake.Parts[2].Row);
            Assert.AreEqual(7, snake.Parts[2].Column);
        }

        [Test]
        public void SnakeWithNoJoinsTurnLeft_SameColumn()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(6, 2),
                    new Coordinate(9, 2)
                }
            };
            var agent = new GoLeftAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(6, snake.Parts[0].Row);
            Assert.AreEqual(1, snake.Parts[0].Column);

            Assert.AreEqual(6, snake.Parts[1].Row);
            Assert.AreEqual(2, snake.Parts[1].Column);

            Assert.AreEqual(8, snake.Parts[2].Row);
            Assert.AreEqual(2, snake.Parts[2].Column);
        }

        [Test]
        public void SnakeWithNoJoinsTurnRight_SameRow()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(2, 9),
                    new Coordinate(2, 6)
                }
            };
            var agent = new GoRightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(3, snake.Parts[0].Row);
            Assert.AreEqual(9, snake.Parts[0].Column);

            Assert.AreEqual(2, snake.Parts[1].Row);
            Assert.AreEqual(9, snake.Parts[1].Column);

            Assert.AreEqual(2, snake.Parts[2].Row);
            Assert.AreEqual(7, snake.Parts[2].Column);
        }

        [Test]
        public void SnakeWithNoJoinsTurnRight_SameColumn()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(6, 2),
                    new Coordinate(9, 2)
                }
            };
            var agent = new GoRightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(6, snake.Parts[0].Row);
            Assert.AreEqual(3, snake.Parts[0].Column);

            Assert.AreEqual(6, snake.Parts[1].Row);
            Assert.AreEqual(2, snake.Parts[1].Column);

            Assert.AreEqual(8, snake.Parts[2].Row);
            Assert.AreEqual(2, snake.Parts[2].Column);
        }

        [Test]
        public void SnakeWithJointBeforeTail_JointShouldBeRemoved()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(2, 9),
                    new Coordinate(2, 7),
                    new Coordinate(3, 7)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.AreEqual(2, snake.Parts.Count);

            Assert.AreEqual(2, snake.Parts[0].Row);
            Assert.AreEqual(10, snake.Parts[0].Column);

            Assert.AreEqual(2, snake.Parts[1].Row);
            Assert.AreEqual(7, snake.Parts[1].Column);
        }
    }
}