using System;
using System.Collections.Generic;

namespace SnakeAPI
{
    public class Snake
    {
        public int Length
        {
            get
            {
                var length = 0;
                for (int partIndex = 0; partIndex < Parts.Count - 1; partIndex++)
                {
                    length += Math.Abs(Parts[partIndex].Row - Parts[partIndex + 1].Row) + Math.Abs(Parts[partIndex].Column - Parts[partIndex + 1].Column) + 1;
                }
                return length;
            }
        }
        public List<Coordinate> Parts { get; set; }
        public bool AppleEaten { get; set; } // When the snake eats an apple, we don't want the tail to move
        public bool Dead { get; set; }

        public Coordinate Head
        {
            get
            {
                return Parts[0];
            }
        }

        public Coordinate Tail
        {
            get
            {
                return Parts[Parts.Count - 1];
            }
        }
    }
}
