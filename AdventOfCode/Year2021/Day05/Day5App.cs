using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day5App
    {
        private List<string> Input = AppExtensions.GetInputList(@"./Year2021/Day05/Input.txt");

        public void Execute()
        {
            var pipes = this.Input.Select(m => new Day5Pipe(m)).ToList();
            var gridPointsMax = pipes.Max(m => m.HighestPoint) + 1;

            var grid = new int[gridPointsMax, gridPointsMax];
            for(var i = 0; i < gridPointsMax; i++)
            {
                for(var x = 0; x < gridPointsMax; x++)
                {
                    grid[i, x] = 0;
                }
            }

            foreach(var pipe in pipes)
            {
                if (pipe.Entry.X == pipe.Exit.X)
                {
                    var start = pipe.Entry.Y > pipe.Exit.Y ? pipe.Exit.Y : pipe.Entry.Y; 
                    var end = pipe.Entry.Y > pipe.Exit.Y ? pipe.Entry.Y : pipe.Exit.Y;

                    for(var i = start; i <= end; i++)
                    {
                        grid[pipe.Entry.X, i]++;

                    }
                }
                else if (pipe.Entry.Y == pipe.Exit.Y)
                {
                    var start = pipe.Entry.X > pipe.Exit.X ? pipe.Exit.X : pipe.Entry.X;
                    var end = pipe.Entry.X > pipe.Exit.X ? pipe.Entry.X : pipe.Exit.X;

                    for (var i = start; i <= end; i++)
                    {
                        grid[i, pipe.Entry.Y]++;
                    }
                }
                else
                {
                    // Diagonal
                    // This could be used for all lines but ah well.
                    var current = pipe.Entry;

                    do
                    {
                        grid[current.X, current.Y]++;

                        current.X = current.X > pipe.Exit.X ? current.X - 1 : current.X + 1;
                        current.Y = current.Y > pipe.Exit.Y ? current.Y - 1 : current.Y + 1;

                    } while (current != pipe.Exit);

                    grid[pipe.Exit.X, pipe.Exit.Y]++;
                }
            }

            var count = 0;
            for(var x = 0; x < gridPointsMax; x++)
            {
                for(var y = 0; y < gridPointsMax; y++)
                {
                    if (grid[x,y] > 1) {
                        count++;
                    }
                }
            }

            Console.WriteLine($"Overlapping pipes: {count}");

        }

    }
}
