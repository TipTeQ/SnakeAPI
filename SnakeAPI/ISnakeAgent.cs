using System.Collections.Generic;

namespace SnakeAPI
{
    public interface ISnakeAgent
    {
        string PlayerName { get; set; }
        int CurrentScore { get; set; }
        int HighScore { get; }
        Snake Snake { get; set; }

        int ChooseMove(List<Snake> snakes, Apple apple);
        void SnakeDead();
        void RespawnSnake(List<Coordinate> parts);
        void SnakeAlive();
    }
}
