using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day7App
    {
        private string Input = AppExtensions.GetInputString($"./Year2021/Day07/Input.txt");

        public void Execute()
        {
            var startingPositions = this.Input.Split(',').Select(m => Convert.ToInt32(m)).ToList();

            var counts = startingPositions.GroupBy(x => x).Select(g => new { Position = g.Key, Count = g.Count() }).OrderByDescending(m => m.Count).ToList();

            var costs = new Dictionary<long, long>();

            var min = 0;
            var max = startingPositions.Max();
            
            for (var position = min; position <= max; position++)
            {
                long fuel = 0;

                foreach (var positionCount in counts)
                {
                    if (positionCount.Position != position)
                    {
                        // Part 1
                        // fuel += Math.Abs(positionCount.Position - position) * positionCount.Count;

                        // Part 2
                        fuel += StepSum(Math.Abs(positionCount.Position - position)) * positionCount.Count;
                       
                    }
                }

                costs.Add(position, fuel);
            }

            var lowest = costs.OrderBy(m => m.Value).First().Value;

            Console.WriteLine($"The cost to align is {lowest}");
        }

        public long StepSum(int steps)
        {
            var start = 1;

            return (steps * (steps + 1) - (start - 1) * start) / 2;
        }

    }
}
