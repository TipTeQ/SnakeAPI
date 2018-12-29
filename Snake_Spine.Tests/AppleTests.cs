using NUnit.Framework;
using SnakeAPI;
using SnakeAPI.Agents;
using System.Collections.Generic;

namespace Snake_Spine.Tests
{
    public class AppleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SnakeEatsApple_AppleEatenShouldBeTrue()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 3),
                    new Coordinate(0, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(1, 5, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(snakeGame.Apple.Eaten);
        }

        [Test]
        public void SnakeEatsApple_AppleShouldAppearInFreeLocation()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 3),
                    new Coordinate(0, 1)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(1, 5, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });

            // Set the apple to be in front of the snake
            var appleRow = 0;
            var appleColumn = 4;

            snakeGame.Apple.Position.Row = appleRow;
            snakeGame.Apple.Position.Column = appleColumn;

            snakeGame.Iterate();

            Assert.IsTrue(snakeGame.Apple.Eaten);

            snakeGame.PrepareNextIteration();

            Assert.IsTrue(snakeGame.Apple.Position.Row != appleRow || snakeGame.Apple.Position.Column != appleColumn);
        }

        [Test]
        public void SnakeEatsApple_SnakeAppleEatenShouldBeTrue()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 3),
                    new Coordinate(0, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(1, 5, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.AppleEaten);
        }

        [Test]
        public void SnakeEatsApple_AgentScoreShouldBeIncreased()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 3),
                    new Coordinate(0, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(1, 5, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });

            var agentScore = agent.CurrentScore;

            snakeGame.Iterate();

            Assert.AreEqual(agentScore + 1, agent.CurrentScore);
        }

        [Test]
        public void SnakeEatsApple_SnakeLengthShouldIncreaseByOne_OnlyAfterAnExtraIteration()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 3),
                    new Coordinate(0, 0)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            var snakeGame = new SnakeGame(1, 5, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });

            var snakeLength = agent.Snake.Length;

            snakeGame.Iterate();

            Assert.AreEqual(snakeLength, agent.Snake.Length);

            snakeGame.PrepareNextIteration();
            snakeGame.Iterate();

            Assert.AreEqual(snakeLength + 1, agent.Snake.Length);
        }
    }
}
