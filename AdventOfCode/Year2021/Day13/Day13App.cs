using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day13App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day13/Input.txt");
        private List<string> Folds = AppExtensions.GetInputList($"./Year2021/Day13/Folds.txt");

        public void Execute()
        {
            var paper = new List<List<char>>();

            // Find out how big our piece of paper is
            var biggestX = 0;
            var biggestY = 0;
            foreach (var line in this.Input)
            {
                var split = line.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                if (split[0] > biggestX)
                    biggestX = split[0];

                if (split[1] > biggestY)
                    biggestY = split[1];
            }

            // Prepare the paper
            for (var y = 0; y <= biggestY; y++)
            {
                var dots = new List<char>();
                for (var x = 0; x <= biggestX; x++)
                {
                    dots.Add('.');
                }
                paper.Add(dots);
            }

            // Make some holes
            foreach (var line in this.Input)
            {
                var split = line.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                paper[split[1]][split[0]] = '#';
            }

            // Get folds
            var foldSteps = new List<Tuple<string, int>>();
            foreach(var fold in this.Folds)
            {
                var line = fold.Replace("fold along ", "").Split('=').ToList();
                foldSteps.Add(new Tuple<string, int>(line[0], Convert.ToInt32(line[1])));
            }

            // Start Folding
            // Making assumptions, you will never fold outside of the range
            foreach(var fold in foldSteps)
            {

                //Console.WriteLine("=================================================");
                //Console.WriteLine("BEFORE FOLD =====================================");
                //Console.WriteLine("=================================================");

                //// Display the paper
                //foreach (var line in paper)
                //{
                //    Console.WriteLine(String.Join("", line));
                //}

                if (fold.Item1 == "y")
                {
                    // Fold > y upwards

                    var align = 2;
                    // Go over each line
                    for(var y = fold.Item2 + 1; y < paper.Count(); y++)
                    {
                        // Go through each x 
                        for (var x = 0; x < paper[y].Count(); x++)
                        {
                            var text = paper[y][x] == '#' || paper[y - align][x] == '#' ? '#' : '.';
                            paper[y - align][x] = text;
                        }

                        align = align + 2;
                    }

                    // Remove the lines cause we are simulating a folded piece of paper
                    paper.RemoveRange(fold.Item2, paper.Count() - fold.Item2);

                }
                else
                {
                    // Fold > x leftwards
                    var align = 2;

                    for(var x = fold.Item2 + 1; x < paper[0].Count(); x++)
                    {
                        // go through each y
                        for(var y = 0; y < paper.Count(); y++)
                        {
                            var text = paper[y][x] == '#' || paper[y][x - align] == '#' ? '#' : '.';
                            paper[y][x - align] = text;
                        }

                        align = align + 2;
                    }

                    // Remove extra ones cause we done with them.
                    for (var y = 0; y < paper.Count(); y++)
                    {
                        paper[y].RemoveRange(fold.Item2, paper[y].Count() - fold.Item2);
                    }

                }
            }

            Console.WriteLine("=================================================");
            Console.WriteLine("AFTER FOLD ======================================");
            Console.WriteLine("=================================================");

            // Display the paper
            foreach (var line in paper)
            {
                Console.WriteLine(String.Join("", line));
            }

            // Get count of dots
            var countOfDots = paper.Sum(m => m.Count(d => d == '#'));

            Console.WriteLine($"There are {countOfDots} dots");
            
        }

    }
}
