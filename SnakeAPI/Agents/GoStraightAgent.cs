using System.Collections.Generic;

namespace SnakeAPI.Agents
{
    public class GoStraightAgent : BaseSnakeAgent
    {
        public GoStraightAgent(Snake snake, string playerName)
            : base(snake, playerName)
        {
        }

        public override int ChooseMove(List<Snake> snakes, Apple apple)
        {
            return 0;
        }
    }
}
