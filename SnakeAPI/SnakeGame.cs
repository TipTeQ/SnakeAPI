using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAPI
{
    public class SnakeGame
    {
        private int _numberOfRows, _numberOfColumns, _initialSnakeLength;

        public Apple Apple => _apple;
        public Board Board { get; private set; }

        private Apple _apple;
        private List<ISnakeAgent> _agents;

        public SnakeGame(int numberOfRows, int numberOfColumns, int initialSnakeLength)
        {
            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            _initialSnakeLength = initialSnakeLength;
        }

        public void InitialiseNewGame(List<ISnakeAgent> agents)
        {
            _agents = agents;

            Board = new Board(_numberOfColumns, _numberOfColumns);

            Board.InitialiseSnakes(agents);

            _apple = new Apple { Position = DetermineNewApplePosition() };

            Board.InitialiseApple(_apple);
        }

        private Coordinate DetermineNewApplePosition()
        {
            var random = new Random();

            var randomRow = random.Next(_numberOfRows);
            var randomColumn = random.Next(_numberOfRows);

            while (Board.IsSnakeOrAppleAtLocation(randomRow, randomColumn))
            {
                randomRow = random.Next(_numberOfRows);
                randomColumn = random.Next(_numberOfColumns);
            }

            return new Coordinate(randomRow, randomColumn);
        }

        public void Iterate()
        {
            Board.UpdateApple(_apple);

            foreach (var agent in _agents)
            {
                var snakes = new List<Snake> { agent.Snake };
                snakes.AddRange(_agents.Where(a => a.PlayerName != agent.PlayerName).Select(a => a.Snake));
                var move = agent.ChooseMove(snakes, _apple);

                MovementService.MoveAgentSnake(agent, move);
            }

            CollisionDetector.ProcessCollisions(_agents, _numberOfRows, _numberOfColumns);

            var agentsWithSnakeOnApple = _agents.Where(a => a.Snake.Head.Row == _apple.Position.Row && a.Snake.Head.Column == _apple.Position.Column);
            if (agentsWithSnakeOnApple.Any())
            {
                _apple.Eaten = true;
            }

            var agentWithSnakeThatAteApple = agentsWithSnakeOnApple.FirstOrDefault(a => !a.Snake.Dead);
            if (agentWithSnakeThatAteApple != null)
            {
                agentWithSnakeThatAteApple.Snake.AppleEaten = true;
                agentWithSnakeThatAteApple.CurrentScore++;
            }

            Board.UpdateSnakes(_agents);
        }

        public void PrepareNextIteration()
        {
            foreach (var agent in _agents)
            {
                if (agent.Snake.Dead)
                {
                    agent.SnakeAlive();
                    RespawnSnake(agent);
                }
            }

            if (_apple.Eaten)
            {
                _apple = new Apple { Position = DetermineNewApplePosition() };
            }

            Board.UpdateApple(_apple);
        }

        private void RespawnSnake(ISnakeAgent agent)
        {
            var random = new Random();

            var placed = false;
            while (!placed)
            {
                var randomDirection = random.Next(4);
                var randomRow = random.Next(_numberOfRows);
                var randomColumn = random.Next(_numberOfColumns);

                bool validPlacement = true;
                switch (randomDirection)
                {
                    case 0: // Up
                        for (var row = randomRow; row < randomRow + _initialSnakeLength; row++)
                        {
                            if (row >= _numberOfRows || Board.IsSnakeOrAppleAtLocation(row, randomColumn))
                            {
                                validPlacement = false;
                                break;
                            }
                        }

                        if (validPlacement)
                        {
                            placed = true;
                            agent.RespawnSnake(new List<Coordinate>
                            {
                                new Coordinate(randomRow, randomColumn),
                                new Coordinate(randomRow + (_initialSnakeLength - 1), randomColumn)
                            });
                        }
                        break;

                    case 1: // Down
                        validPlacement = true;
                        for (var row = randomRow; row > randomRow - _initialSnakeLength; row--)
                        {
                            if (row < 0 || Board.IsSnakeOrAppleAtLocation(row, randomColumn))
                            {
                                validPlacement = false;
                                break;
                            }
                        }

                        if (validPlacement)
                        {
                            placed = true;
                            agent.RespawnSnake(new List<Coordinate>
                            {
                                new Coordinate(randomRow, randomColumn),
                                new Coordinate(randomRow - (_initialSnakeLength - 1), randomColumn)
                            });
                        }
                        break;

                    case 2: // Left
                        for (var column = randomColumn; column < randomColumn + _initialSnakeLength; randomColumn++)
                        {
                            if (randomColumn >= _numberOfColumns || Board.IsSnakeOrAppleAtLocation(randomRow, column))
                            {
                                validPlacement = false;
                                break;
                            }
                        }

                        if (validPlacement)
                        {
                            placed = true;
                            agent.RespawnSnake(new List<Coordinate>
                            {
                                new Coordinate(randomRow, randomColumn),
                                new Coordinate(randomRow, randomColumn + (_initialSnakeLength - 1))
                            });
                        }
                        break;

                    default: // Right
                        for (var column = randomColumn; column > randomColumn - _initialSnakeLength; randomColumn--)
                        {
                            if (randomColumn < 0 || Board.IsSnakeOrAppleAtLocation(randomRow, column))
                            {
                                validPlacement = false;
                                break;
                            }
                        }

                        if (validPlacement)
                        {
                            placed = true;
                            agent.RespawnSnake(new List<Coordinate>
                            {
                                new Coordinate(randomRow, randomColumn),
                                new Coordinate(randomRow, randomColumn - (_initialSnakeLength - 1))
                            });
                        }
                        break;
                }
            }
        }

        public void PrintBoard()
        {
            BoardPrinter.PrintBoard(Board.GetBoard());
        }

        public void PrintScores()
        {
            foreach (var agent in _agents)
            {
                Console.WriteLine($"{agent.PlayerName}: {agent.CurrentScore} ({agent.HighScore})");
            }
        }
    }
}
