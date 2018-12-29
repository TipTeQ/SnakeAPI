using NUnit.Framework;
using SnakeAPI;
using SnakeAPI.Agents;
using System.Collections.Generic;

namespace Snake_Spine.Tests
{
    public class SnakeGameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SnakeCrashesAndDies_ShouldRespawnNextIterationWithResetScore()
        {
            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(0, 0),
                    new Coordinate(0, 6)
                }
            };
            var agent = new GoStraightAgent(snake, "Player1");

            Assert.AreEqual(7, agent.CurrentScore);

            var snakeGame = new SnakeGame(16, 16, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });
            snakeGame.Iterate();

            Assert.IsTrue(agent.Snake.Dead);

            snakeGame.PrepareNextIteration();

            Assert.IsFalse(agent.Snake.Dead);
            Assert.AreEqual(4, agent.CurrentScore);
        }
    }
}
