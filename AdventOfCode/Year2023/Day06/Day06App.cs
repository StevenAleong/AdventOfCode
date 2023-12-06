using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day06App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day06/input.txt");

        public void Part1() {
            var times = Input[0].Replace("Time:", "").Split(' ').Where(m => m.Trim() != "").Select(m => Convert.ToInt32(m)).ToList();
            var distances = Input[1].Replace("Distance:", "").Split(' ').Where(m => m.Trim() != "").Select(m => Convert.ToInt32(m)).ToList();

            var records = new List<int>();

            for (var i = 0; i < times.Count; i++) {
                var time = times[i];
                var distance = distances[i];
                var timesRecordBeaten = 0;

                for (var hold = 1; hold < time; hold++) {
                    var distanceTravelled = hold * (time - hold);

                    if (distanceTravelled > distance) {
                        timesRecordBeaten++;
                    }
                }

                records.Add(timesRecordBeaten);
            }

            Console.WriteLine(records.Aggregate(1, (total, next) => total * next));
        }

        public void Part2() {
            long time = 56977875;
            long distance = 546192711311139;
            long beatRecordCount = 0;

            for (long hold = 1; hold < time; hold++) {
                long distanceTravelled = hold * (time - hold);

                if (distanceTravelled > distance) {
                    beatRecordCount++;
                }
            }

            Console.WriteLine(beatRecordCount);

        }

    }
}
