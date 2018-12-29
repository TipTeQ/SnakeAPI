using System;
using System.Collections.Generic;

namespace SnakeAPI.Agents
{
    public enum TileType
    {
        Floor = 1,
        Forest = 5,
        Wall = System.Int32.MaxValue
    }

    public class Location
    {

        public readonly int column, row;

        public Location(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public Location(Coordinate position)
        {
            this.column = position.Column;
            this.row = position.Row;
        }

        public override bool Equals(System.Object obj)
        {
            Location loc = obj as Location;
            return this.column == loc.column && this.row == loc.row;
        }

        // This is creating collisions. How do I solve this?
        public override int GetHashCode()
        {
            return (column * 597) ^ (row * 1173);
        }
    }

    public class SquareGrid
    {
        // DIRS is directions
        // I added diagonals to this but noticed it can create problems--
        // like the path will go through obstacles that are diagonal from each other.
        public static readonly Location[] DIRS = new[] {
            new Location(1, 0), // to right of tile
            new Location(0, -1), // below tile
            new Location(-1, 0), // to left of tile
            new Location(0, 1), // above tile
            //new Location(1, 1), // diagonal top right
            //new Location(-1, 1), // diagonal top left
            //new Location(1, -1), // diagonal bottom right
            //new Location(-1, -1) // diagonal bottom left
        };

        // The x and y here represent the grid's starting point, 0,0.
        // And width and height are how many units wide and high the grid is.
        // See how I use this in TileManager.cs to see how you can keep
        // your Unity GameObjects in sync with this abstracted grid.
        public SquareGrid(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            this.tiles = new TileType[width, height];
        }

        public int x, y, width, height;

        // This is a 2d array that stores each tile's type and movement cost
        // using the TileType enum defined above
        public TileType[,] tiles;

        // Check if a location is within the bounds of this grid.
        public bool InBounds(Location id)
        {
            return (x <= id.column) && (id.column < width) && (y <= id.row) && (id.row < height);
        }

        // Everything that isn't a Wall is Passable
        public bool Passable(Location id)
        {
            return (int)tiles[id.column, id.row] < System.Int32.MaxValue;
        }

        // If the heuristic = 2f, the movement is diagonal
        public int Cost(Location a, Location b)
        {
            //if (AStarSearch.Heuristic(a, b) == 2f)
            //{
            //    return (int)tiles[b.column, b.row] * Math.Sqrt(2f);
            //}
            return (int)tiles[b.column, b.row];
        }

        // Check the tiles that are next to, above, below, or diagonal to
        // this tile, and return them if they're within the game bounds and passable
        public IEnumerable<Location> Neighbors(Location id)
        {
            foreach (var dir in DIRS)
            {
                Location next = new Location(id.column + dir.column, id.row + dir.row);
                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }

    public class PriorityQueue<T>
    {
        // From Red Blob: I'm using an unsorted array for this example, but ideally this
        // would be a binary heap. Find a binary heap class:
        // * https://bitbucket.org/BlueRaja/high-speed-priority-queue-for-c/wiki/Home
        // * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
        // * http://xfleury.github.io/graphsearch.html
        // * http://stackoverflow.com/questions/102398/priority-queue-in-net

        private List<KeyValuePair<T, int>> elements = new List<KeyValuePair<T, int>>();

        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, int priority)
        {
            elements.Add(new KeyValuePair<T, int>(item, priority));
        }

        // Returns the Location that has the lowest priority
        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Value < elements[bestIndex].Value)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].Key;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }

    // Now that all of our classes are in place, we get get
    // down to the business of actually finding a path.
    public class AStarSearch
    {
        // Someone suggested making this a 2d field.
        // That will be worth looking at if you run into performance issues.
        public Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
        public Dictionary<Location, int> costSoFar = new Dictionary<Location, int>();

        private Location start;
        private Location goal;

        static public int Heuristic(Location a, Location b)
        {
            return Math.Abs(a.column - b.column) + Math.Abs(a.row - b.row);
        }

        // Conduct the A* search
        public AStarSearch(SquareGrid graph, Location start, Location goal)
        {
            // start is current sprite Location
            this.start = start;
            // goal is sprite destination eg tile user clicked on
            this.goal = goal;

            // add the cross product of the start to goal and tile to goal vectors
            // Vector3 startToGoalV = Vector3.Cross(start.vector3,goal.vector3);
            // Location startToGoal = new Location(startToGoalV);
            // Vector3 neighborToGoalV = Vector3.Cross(neighbor.vector3,goal.vector3);

            // frontier is a List of key-value pairs:
            // Location, (int) priority
            var frontier = new PriorityQueue<Location>();
            // Add the starting location to the frontier with a priority of 0
            frontier.Enqueue(start, 0);

            cameFrom.Add(start, start); // is set to start, None in example
            costSoFar.Add(start, 0);

            while (frontier.Count > 0)
            {
                // Get the Location from the frontier that has the lowest
                // priority, then remove that Location from the frontier
                Location current = frontier.Dequeue();

                // If we're at the goal Location, stop looking.
                if (current.Equals(goal)) break;

                // Neighbors will return a List of valid tile Locations
                // that are next to, diagonal to, above or below current
                foreach (var neighbor in graph.Neighbors(current))
                {

                    // If neighbor is diagonal to current, graph.Cost(current,neighbor)
                    // will return Sqrt(2). Otherwise it will return only the cost of
                    // the neighbor, which depends on its type, as set in the TileType enum.
                    // So if this is a normal floor tile (1) and it's neighbor is an
                    // adjacent (not diagonal) floor tile (1), newCost will be 2,
                    // or if the neighbor is diagonal, 1+Sqrt(2). And that will be the
                    // value assigned to costSoFar[neighbor] below.
                    int newCost = costSoFar[current] + graph.Cost(current, neighbor);

                    // If there's no cost assigned to the neighbor yet, or if the new
                    // cost is lower than the assigned one, add newCost for this neighbor
                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {

                        // If we're replacing the previous cost, remove it
                        if (costSoFar.ContainsKey(neighbor))
                        {
                            costSoFar.Remove(neighbor);
                            cameFrom.Remove(neighbor);
                        }

                        costSoFar.Add(neighbor, newCost);
                        cameFrom.Add(neighbor, current);
                        var priority = newCost + Heuristic(neighbor, goal);
                        frontier.Enqueue(neighbor, priority);
                    }
                }
            }

        }

        // Return a List of Locations representing the found path
        public List<Location> FindPath()
        {

            List<Location> path = new List<Location>();
            Location current = goal;
            // path.Add(current);

            while (!current.Equals(start))
            {
                if (!cameFrom.ContainsKey(current))
                {
                    return new List<Location>();
                }
                path.Add(current);
                current = cameFrom[current];
            }
            // path.Add(start);
            path.Reverse();
            return path;
        }
    }
}
