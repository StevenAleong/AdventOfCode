using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day6App
    {
        private string Input = AppExtensions.GetInputString(@"./Year2021/Day06/Input.txt");
        private List<int> Fish = new List<int>();

        public void Execute()
        {
            this.Fish = this.Input.Split(',').Where(m => m.Trim() != "").Select(m => Convert.ToInt32(m)).ToList();

            Calculate(80);
            Calculate(256);

            //var days = 256;

            //for (var d = 0; d < days; d++)
            //{

            //    var fishToAdd = lanternfish.Count(f => f == 0);
            //    lanternfish.AddRange(Enumerable.Repeat(9, fishToAdd).ToList());

            //    for (var f = 0; f < lanternfish.Count; f++)
            //    {
            //        if (lanternfish[f] == 0)
            //            lanternfish[f] = 7;
            //    }

            //    lanternfish = lanternfish.Select(f => { f = f - 1; return f; }).ToList();

            //}

            //Console.WriteLine($"There are {lanternfish.Count} lanternfish");

        }

        public void Calculate(int days)
        {
            var fishLifeSpan = new long[9];

            foreach (var f in this.Fish)
            {
                fishLifeSpan[f]++;
            }

            for (var d = 0; d < days; d++)
            {
                var buffer = new long[9];
                for (var f = 0; f < fishLifeSpan.Length; f++)
                {
                    if (f == 0)
                    {
                        buffer[6] += fishLifeSpan[f];
                        buffer[8] += fishLifeSpan[f];
                    }
                    else
                    {
                        buffer[f - 1] += fishLifeSpan[f];
                    }
                }

                fishLifeSpan = buffer;
            }

            Console.WriteLine($"After {days} days, there are {fishLifeSpan.Sum()} lanternfish");

        }
    }
}
