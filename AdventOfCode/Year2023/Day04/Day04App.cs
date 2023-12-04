using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023 {
    internal class Day04App {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day04/input.txt");

        public void Part1() {
            var total = 0;
            foreach (var line in Input) {
                var cleaned = line.Remove(0, line.IndexOf(":") + 1);
                var winning = cleaned.Split('|')[0].Split(' ').Where(m => m.Trim() != "").ToList();
                var playing = cleaned.Split('|')[1].Split(' ').Where(m => m.Trim() != "").ToList();

                var matched = playing.Intersect(winning);
                if (matched.Any()) {
                    var points = 1;
                    for (int i = 1; i < matched.Count(); i++) {
                        points *= 2;
                    }
                    total += points;
                }

            }

            Console.WriteLine(total);
        }

        public void Part2() {
            var total = 0;
            var totalCards = 0;
            var copies = new Dictionary<int, int>();
            foreach (var line in Input) {
                if (line.Trim().Length > 0) {
                    totalCards++;
                }
            }

            foreach (var line in Input) {
                if (line.Trim().Length > 0) {
                    var card = Convert.ToInt32(line.Substring(0, line.IndexOf(":")).Replace("Card", "").Trim());
                    var cleaned = line.Remove(0, line.IndexOf(":") + 1);
                    var winning = cleaned.Split('|')[0].Split(' ').Where(m => m.Trim() != "").ToList();
                    var playing = cleaned.Split('|')[1].Split(' ').Where(m => m.Trim() != "").ToList();

                    var matched = playing.Intersect(winning).ToList();

                    if (matched.Any()) {
                        for (var i = card + 1; i <= card + matched.Count(); i++) {
                            // Get the total number of card copies that exist already
                            var toAdd = (copies.ContainsKey(card) ? copies[card] : 0) + 1;

                            if (i <= totalCards) {
                                if (!copies.ContainsKey(i)) {
                                    copies.Add(i, toAdd);
                                } else {
                                    copies[i] += toAdd;
                                }
                            }
                        }
                    }
                }
            }

            // Add all the original cards to the copied cards
            total = copies.Select(m => m.Value).Sum() + totalCards;

            Console.WriteLine(total);
        }
    }
}
