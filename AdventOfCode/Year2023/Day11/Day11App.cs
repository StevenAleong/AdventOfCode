using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    internal class Day11App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day11/input.txt");

        private List<List<char>> Space = new List<List<char>>();

        private int[,] Galaxies = new int[0,0];
        private List<Point> GalaxyLocations = new List<Point>();

        private List<int> EmptyRows = new List<int>();
        private List<int> EmptyCols = new List<int>();

        // Part 1: 1
        // Part 2: 999,999
        private int ExpandGalaxyAmount = 1;

        public void Execute() {
            Galaxies = new int[Input.Count(), Input[0].ToCharArray().Length];

            // Prexpanded
            for (var l = 0; l < Input.Count; l++) {
                var line = Input[l];

                var nodes = line.ToCharArray().ToList();

                for (var i = 0; i < nodes.Count(); i++) {
                    if (nodes[i] == '#') {
                        Galaxies[l, i] = 1;
                    }
                }

                Space.Add(nodes);
            }

            // Look up empty Rows
            int rows = Space.Count();
            int columns = Space[0].Count();

            for (int r = 0; r < rows; r++) {
                var containsGalaxy = false;

                for (int c = 0; c < columns; c++) {
                    if (Space[r][c] == '#') {
                        containsGalaxy = true;
                        break;
                    }
                }

                if (!containsGalaxy) {
                    EmptyRows.Add(r);
                }
            }

            // Look up empty Cols
            for (int c = 0; c < columns; c++) {
                var containsGalaxy = false;

                for (int r = 0; r < rows; r++) {
                    if (Space[r][c] == '#') {
                        containsGalaxy = true;

                        break;
                    }
                }

                if (!containsGalaxy) {
                    EmptyCols.Add(c);
                }
            }

            // Get the galaxy locations
            for (int i = 0; i < Space.Count(); i++) {
                for (int j = 0; j < Space[0].Count(); j++) {
                    if (Space[i][j] == '#') {
                        GalaxyLocations.Add(new Point(i, j));
                    }
                }
            }

            GetDistances();
        }

        public void GetDistances()
        {
            var processed = new HashSet<(Point, Point)>();
            var distances = new List<long>(0);
            GalaxyLocations.ForEach(galaxy => {

                GalaxyLocations.ForEach(other => { 
                    if (galaxy != other && !processed.Contains((galaxy, other)) && !processed.Contains((other, galaxy))) {
                        var distance = Math.Abs(other.X - galaxy.X) + Math.Abs(other.Y - galaxy.Y);

                        var rowsBetween = CountBetween(EmptyRows, Math.Abs(galaxy.X), Math.Abs(other.X)) * ExpandGalaxyAmount;
                        var colsBetween = CountBetween(EmptyCols, Math.Abs(galaxy.Y), Math.Abs(other.Y)) * ExpandGalaxyAmount;

                        distances.Add(distance + rowsBetween + colsBetween);
                    }
                    processed.Add((galaxy, other));
                });

            });

            Console.WriteLine(distances.Sum());
        }

        private int CountBetween(List<int> values, int start, int end) {
            int count = 0;
            foreach (var value in values) {
                if ((value > Math.Min(start, end) && value < Math.Max(start, end)) || (value > Math.Min(end, start) && value < Math.Max(end, start))) {
                    count++;
                }
            }
            return count;
        }

    }
}
