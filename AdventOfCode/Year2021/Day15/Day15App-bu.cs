using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day15App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day15/Input.txt");

        private List<List<int>> Cave = new List<List<int>>();

        public void Execute()
        {
            this.Cave = this.Input.ConvertStringListTo2DIntArray();

            var startTile = new Day15Tile() {
                X = 0,
                Y = 0
            };

            var exitTile = new Day15Tile()
            {
                X = this.Cave.Last().Count() - 1,
                Y = this.Cave.Count() - 1,
                Cost = this.Cave.Last().Last()
            };

            startTile.SetDistance(exitTile.X, exitTile.Y);

            var activeTiles = new List<Day15Tile>();
            activeTiles.Add(startTile);

            var vistedTiles = new List<Day15Tile>();

            var total = 0L;

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(X => X.CostDistance).First();

                Console.WriteLine($"Checking {checkTile.Y},{checkTile.X}");

                if (checkTile.X == exitTile.X && checkTile.Y == exitTile.Y)
                {
                    Console.WriteLine("We made it, going backwards");
                    var tile = checkTile;

                    while(true)
                    {
                        Console.WriteLine($"{tile.Y},{tile.X}: {tile.Cost}");
                        total += tile.Cost;
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            Console.WriteLine($"The lowest cost path is {total}");
                            return;
                        }
                    }
                }

                vistedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var pathTiles = GetPathTiles(checkTile, exitTile);

                foreach(var tile in pathTiles)
                {
                    // We've been here befoooore
                    if (vistedTiles.Any(t => t.X == tile.X && t.Y == tile.Y))
                        continue;

                    // Check to see if we get a better value
                    if (activeTiles.Any(t => t.X == tile.X && t.Y == tile.Y))
                    {
                        var existingTile = activeTiles.First(t => t.X == tile.X && t.Y == tile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(tile);
                        }
                    }
                    else
                    {
                        // Add the active tile
                        activeTiles.Add(tile);
                    }
                }

            }
        }

        private List<Day15Tile> GetPathTiles(Day15Tile current, Day15Tile target)
        {
            var maxX = this.Cave[0].Count() - 1;
            var maxY = this.Cave.Count() - 1;

            var possible = new List<Day15Tile>();

            // North
            //if (current.Y - 1 >= 0)
                //possible.Add(new Day15Tile() { X = current.X, Y = current.Y - 1, Parent = current, Cost = Cave[current.Y - 1][current.X] });

            // East
            if (current.X + 1 <= maxX)
                possible.Add(new Day15Tile() { X = current.X + 1, Y = current.Y, Parent = current, Cost = this.Cave[current.Y][current.X + 1] });

            // South
            if (current.Y + 1 <= maxY)
                possible.Add(new Day15Tile() { X = current.X, Y = current.Y + 1, Parent = current, Cost = this.Cave[current.Y + 1][current.X] });

            // West
            if (current.X - 1 >= 0)
                possible.Add(new Day15Tile() { X = current.X - 1, Y = current.Y, Parent = current, Cost = this.Cave[current.Y][current.X - 1] });

            possible.ForEach(tile => tile.SetDistance(target.X, target.Y));

            return possible;
        }

    }

    internal class Day15Tile
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Cost { get; set; }

        public int Distance { get; set; }

        public int CostDistance => Cost + Distance;

        public Day15Tile Parent { get; set; }

        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - this.X) + Math.Abs(targetY - this.Y);
        }
    }
}
