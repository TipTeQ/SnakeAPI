using System.Collections.Generic;

namespace SnakeAPI.Agents
{
    public class GoLeftAgent : BaseSnakeAgent
    {
        public GoLeftAgent(Snake snake, string playerName)
            : base(snake, playerName)
        {
        }

        public override int ChooseMove(List<Snake> snakes, Apple apple)
        {
            return 1;
        }
    }
}
