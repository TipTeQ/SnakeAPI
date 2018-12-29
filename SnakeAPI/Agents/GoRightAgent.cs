using System.Collections.Generic;

namespace SnakeAPI.Agents
{
    public class GoRightAgent : BaseSnakeAgent
    {
        public GoRightAgent(Snake snake, string playerName)
            : base(snake, playerName)
        {
        }

        public override int ChooseMove(List<Snake> snakes, Apple apple)
        {
            return 2;
        }
    }
}
