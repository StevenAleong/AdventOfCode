using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode.Year2023
{
    internal class Node {
        public string Pos { get; set; } = "";
        public string Left { get; set; } = "";
        public string Right { get; set; } = "";
    }

    internal class NodeTrack {
        public int Index { get; set; } = 0;

        public List<int> StepsToZ { get; set; } = new List<int>();
    }

    internal class Day08App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day08/input.txt");

        public void Execute() {
            Part1();
            Part2();
        }

        public void Part1()
        {
            var instructions = Input[0].ToCharArray().Select(m => m.ToString()).ToList();
            var paths = new List<Node>();

            for (var i = 2; i < Input.Count; i++) {
                var line = Input[i];
                paths.Add(new Node() {
                    Pos = line.Substring(0, 3),
                    Left = line.Substring(7, 3),
                    Right = line.Substring(12, 3)
                });
            }

            var current = paths.First(m => m.Pos == "AAA");
            var steps = 0;

            while (current.Pos != "ZZZ") {
                instructions.ForEach(step => {                     
                    current = paths.FirstOrDefault(m => m.Pos == (step == "L" ? current.Left : current.Right));
                    steps++;
                });
            }

            Console.WriteLine(steps);

        }

        public void Part2() {
            var instructions = Input[0].ToCharArray().Select(m => m.ToString()).ToList();
            var paths = new List<Node>();

            for (var i = 2; i < Input.Count; i++) {
                var line = Input[i];
                paths.Add(new Node() {
                    Pos = line.Substring(0, 3),
                    Left = line.Substring(7, 3),
                    Right = line.Substring(12, 3)
                });
            }

            // Get all starting nodes
            var nodes = paths.Where(m => m.Pos.EndsWith("A")).ToList();

            // Prepare placeholders to track how many steps it takes each node to reach Z
            var nodeTrackers = new List<int>();
            for (var n = 0; n < nodes.Count; n++) {
                nodeTrackers.Add(0);
            }

            for (var n = 0; n < nodes.Count; n++) {
                var node = nodes[n];
                var nodeSteps = 0;

                while (!node.Pos.EndsWith("Z")) {
                    for (var i = 0; i < instructions.Count; i++) {
                        var step = instructions[i];

                        node = paths.First(m => m.Pos == (step == "L" ? node.Left : node.Right));
                        nodeSteps++;

                        if (node.Pos.EndsWith("Z")) {
                            nodeTrackers[n] = nodeSteps;
                            break;
                        }
                    }

                    nodes[n] = node;
                }
            }

            var allNumbers = nodeTrackers.Select(x => Convert.ToInt64(x)).Distinct().ToList();
            long lcm = LCM(allNumbers);

            Console.WriteLine(lcm);

            // Bruta force, not cool.
            //var steps = 0;
            //while (!nodes.All(m => m.Pos.EndsWith("Z"))) {
            //    for (var i = 0; i < instructions.Count; i++) {
            //        var step = instructions[i];

            //        for (var n = 0; n < nodes.Count; n++) {
            //            var found = paths.First(m => m.Pos == (step == "L" ? nodes[n].Left : nodes[n].Right));
            //            nodes[n] = found;
            //        }

            //        steps++;

            //        if (nodes.All(m => m.Pos.EndsWith("Z"))) {
            //            break;
            //        }
            //    }
            //}

            //Console.WriteLine(steps);
        }

        static long LCM(List<long> numbers) {
            return numbers.Aggregate(1L, (a, b) => a * b / GCD(a, b));
        }

        static long GCD(long a, long b) {
            while (b != 0) {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

    }
}
