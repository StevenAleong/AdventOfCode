using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day02App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day02/input.txt");

        public void Part1() {
            var maxRedCubes = 12;
            var maxGreenCubes = 13;
            var maxBlueCubes = 14;

            var regex = new Regex(@"(\d+ (blue|red|green))");

            var gameSum = 0;

            Input.ForEach(line => {
                var games = line.Split(';');
                var gamePossible = true;

                for (var gi = 0; gi < games.Count(); gi++) {
                    var matches = regex.Matches(games[gi]);
                    var possible = true;

                    if (matches.Count > 0) {
                        for (var i = 0; i < matches.Count; i++) {
                            var match = matches[i];
                            var value = match.Value;

                            if (value.EndsWith("red")) {
                                var reds = Convert.ToInt32(value.Replace(" red", ""));
                                if (reds > maxRedCubes) {
                                    possible = false;
                                    gamePossible = false;
                                    break;
                                }

                            } else if (value.EndsWith("green")) {
                                var greens = Convert.ToInt32(value.Replace(" green", ""));
                                if (greens > maxGreenCubes) {
                                    possible = false;
                                    gamePossible = false;
                                    break;
                                }

                            } else {
                                var blues = Convert.ToInt32(value.Replace(" blue", ""));
                                if (blues > maxBlueCubes) {
                                    possible = false;
                                    gamePossible = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (!possible) {
                        break;                        
                    }
                }

                if (gamePossible) {
                    var id = line.Substring(0, line.IndexOf(":")).Replace("Game ", "");
                    gameSum += Convert.ToInt32(id);
                }

            });

            Console.WriteLine(gameSum);

        }

        public void Part2() {
            var regex = new Regex(@"(\d+ (blue|red|green))");
            var sum = 0;

            Input.ForEach(line => {
                var green = 0;
                var blue = 0;
                var red = 0;

                var matches = regex.Matches(line);
                for (var i = 0; i < matches.Count; i++) {
                    var match = matches[i];
                    var value = match.Value;

                    if (value.EndsWith("red")) {
                        var reds = Convert.ToInt32(value.Replace(" red", ""));
                        if (reds > red) red = reds;

                    } else if (value.EndsWith("green")) {
                        var greens = Convert.ToInt32(value.Replace(" green", ""));
                        if (greens > green) green = greens;

                    } else {
                        var blues = Convert.ToInt32(value.Replace(" blue", ""));
                        if (blues > blue) blue = blues;
                    }
                }

                var power = green * blue * red;
                sum += power;
            });

            Console.WriteLine(sum);
        }

    }
}
