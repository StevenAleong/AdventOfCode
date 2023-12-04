using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day03App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day03/input.txt");

        public void Part1()
        {
            var symbolRegex = new Regex(@"[^a-zA-Z\d\s:.]");
            var numberRegex = new Regex(@"\d+");
            var sum = 0;
            
            for (var i = 0; i < Input.Count; i++) {
                var line = Input[i];

                var matches = numberRegex.Matches(line);
                if (matches.Count > 0) {
                    matches.ToList().ForEach(part => {
                        var startIndex = part.Index - 1;
                        var stringLength = part.Length + (part.Index - 1 < 0 ? 1 : 2);
                        var foundSymbol = false;

                        // Check previous line for symbols
                        if (i > 0) {
                            var prevLine = AppExtensions.SafeSubstring(Input[i - 1], startIndex, stringLength);
                            foundSymbol = symbolRegex.IsMatch(prevLine);
                        }

                        // Check current line for symbols
                        if (!foundSymbol) {
                            var currentLine = AppExtensions.SafeSubstring(line, startIndex, stringLength);
                            foundSymbol = symbolRegex.IsMatch(currentLine);
                        }

                        // Check next line for symbols
                        if (!foundSymbol && i < Input.Count - 1) {
                            var nextLine = AppExtensions.SafeSubstring(Input[i + 1], startIndex, stringLength);
                            foundSymbol = symbolRegex.IsMatch(nextLine);
                        }

                        if (foundSymbol) {
                            sum += Convert.ToInt32(part.Value);
                        }
                    });
                }
            }

            Console.WriteLine(sum);
        }


        //index 3* (2 3 4)

        //0 to 2 = 467
        //2 to 3 = 35


        //index 5* (4 5 6)


        //6 to 8 = 755
        //5 to 7 = 598

        //start <= max start end index
        //end >= max start index

        public void Part2() {
            var starRegex = new Regex(@"\*");
            var numberRegex = new Regex(@"\d+");

            Int64 sum = 0;

            for (var i = 0; i < Input.Count; i++) {
                var matches = starRegex.Matches(Input[i]);
                var lineLength = Input[i].Length;

                if (matches.Count > 0) {
                    matches.ToList().ForEach(star => {
                        var startIndex = star.Index - 1;
                        var ratios = new List<int>();

                        var starIndexes = new List<int>();
                        if (star.Index - 1 != -1) starIndexes.Add(star.Index - 1);
                        starIndexes.Add(star.Index);
                        if (star.Index + 1 < lineLength - 1) starIndexes.Add(star.Index + 1);

                        // Check the previous line if there's any numbers around it
                        if (i > 0) {
                            var prevNumbersFound = numberRegex.Matches(Input[i -1]);

                            prevNumbersFound.ToList().ForEach(num => {
                                var prevIndexes = new List<int>();
                                for (var i = 0; i < num.Length; i++) {
                                    prevIndexes.Add(num.Index + i);
                                }

                                if (starIndexes.Any(item => prevIndexes.Contains(item))) {
                                    ratios.Add(Convert.ToInt32(num.Value));
                                }
                            });
                        }

                        // Check the current line if there's any numbers around it
                        var currentNumbers = numberRegex.Matches(Input[i]);
                        currentNumbers.ToList().ForEach(num => {
                            var currIndexes = new List<int>();
                            for (var i = 0; i < num.Length; i++) {
                                currIndexes.Add(num.Index + i);
                            }

                            if (starIndexes.Any(item => currIndexes.Contains(item))) {
                                ratios.Add(Convert.ToInt32(num.Value));
                            }
                        });

                        // Check the next line if there's any numbers around it
                        if (i < Input.Count - 1) {
                            var nextNumbersFound = numberRegex.Matches(Input[i + 1]);

                            nextNumbersFound.ToList().ForEach(num => {
                                var nextIndexes = new List<int>();
                                for (var i = 0; i < num.Length; i++) {
                                    nextIndexes.Add(num.Index + i);
                                }

                                if (starIndexes.Any(item => nextIndexes.Contains(item))) {
                                    ratios.Add(Convert.ToInt32(num.Value));
                                }
                            });
                        }

                        if (ratios.Count == 2) {
                            var ratio = ratios.Aggregate(1, (acc, val) => acc * val);
                            sum += ratio;
                        }
                    });
                }
            }

            Console.WriteLine(sum);
        }
                

    }
}
