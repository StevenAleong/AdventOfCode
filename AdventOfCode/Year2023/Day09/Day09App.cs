using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day09App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day09/input.txt");

        public void Execute() {
            long part1Total = 0;
            long part2Total = 0;

            Input.ForEach(line => {
                var numbers = line.Split(' ').Select(num => Convert.ToInt64(num)).ToList();
                part1Total += GetExtrapolation(numbers);
                part2Total += GetExtrapolationBeginning(numbers);
            });

            Console.WriteLine(part1Total);
            Console.WriteLine(part2Total);
        }

        private long GetExtrapolation(List<long> numbers) {
            long extrapolation = 0;

            var differences = new List<long>();

            for (var n = 0; n < numbers.Count - 1; n++) {
                differences.Add(numbers[n + 1] - numbers[n]);
            }

            if (!differences.All(m => m == 0)) {
                extrapolation = GetExtrapolation(differences);
            }

            return extrapolation + numbers.Last();
        }

        private long GetExtrapolationBeginning(List<long> numbers) {
            long extrapolation = 0;

            var differences = new List<long>();

            for (var n = 0; n < numbers.Count - 1; n++) {
                differences.Add(numbers[n + 1] - numbers[n]);
            }

            if (!differences.All(m => m == 0)) {
                extrapolation = GetExtrapolationBeginning(differences);
            }

            return numbers.First() - extrapolation;
        }

    }
}
