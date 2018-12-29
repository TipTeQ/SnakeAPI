using System.Collections.Generic;

namespace SnakeAPI
{
    public abstract class BaseSnakeAgent : ISnakeAgent
    {
        private Snake _snake;
        private string _playerName;
        private int _currentScore, _highScore;

        public string PlayerName
        {
            get => _playerName;
            set
            {
                if (_playerName != value) _playerName = value;
            }
        }

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                if (_currentScore != value) _currentScore = value;
                if (_highScore < value) _highScore = value;
            }
        }

        public int HighScore
        {
            get => _highScore;
        }

        public Snake Snake
        {
            get => _snake;
            set
            {
                if (_snake != value) _snake = value;
            }
        }

        public BaseSnakeAgent(Snake snake, string playerName)
        {
            _snake = snake;
            _playerName = playerName;
            CurrentScore = Snake.Length;
        }

        public abstract int ChooseMove(List<Snake> snakes, Apple apple);

        public void SnakeDead()
        {
            CurrentScore = 0;
            Snake.Dead = true;
        }

        public void RespawnSnake(List<Coordinate> parts)
        {
            Snake.Parts = parts;
            CurrentScore = Snake.Length;
        }

        public void SnakeAlive()
        {
            Snake.Dead = false;
        }
    }
}
