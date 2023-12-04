using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day01App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day01/input.txt");

        public void Execute()
        {
            PartTwo();
        }

        private void PartOne() {
            var regex = new Regex(@"\d");
            var sum = 0;

            Input.ForEach(line => {
                var matches = regex.Matches(line);
                var number = "";

                if (matches.Count > 0) {
                    number = matches[0].Value;
                    number += matches[matches.Count - 1].Value;
                    sum += Convert.ToInt32(number);
                }
            });

            Console.WriteLine(sum.ToString());
        }

        private void PartTwo() {
            var regex = new Regex(@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))");

            var valuePairs = new Dictionary<string, string>() {
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5"},
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
            };

            var sum = 0;

            Input.ForEach(line => {
                var matches = regex.Matches(line);

                var number = "";

                if (matches.Count > 0) {
                    if (int.TryParse(matches[0].Groups[1].Value, out int resultStart)) {
                        number = matches[0].Groups[1].Value;
                    } else {
                        number = valuePairs[matches[0].Groups[1].Value];
                    }
                    
                    if (int.TryParse(matches[matches.Count - 1].Groups[1].Value, out int resultEnd)) {
                        number += matches[matches.Count - 1].Groups[1].Value;
                    } else {
                        number += valuePairs[matches[matches.Count - 1].Groups[1].Value];
                    }
                    
                    sum += Convert.ToInt32(number);
                }
            });

            Console.WriteLine(sum);

        }

    }
}
