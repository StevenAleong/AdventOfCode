using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day12App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day12/Input.txt");

        private List<string> Paths = new List<string>();

        private Dictionary<string, List<string>> Options = new Dictionary<string, List<string>>();

        public void Execute()
        {
            // loop through each that have a start to find all starting positions

            // loop through each that have an end to find all ending positions

            // For each point, show which ones it can go to?

            // is this like a linked list thing?

            
            // Gather each node and list each of the paths that it can go.
            // Don't add "end" the dictionary for something to go through
            foreach (var option in this.Input)
            {
                var split = option.Split('-');
                if (split[0] != "end")
                {
                    if (!this.Options.ContainsKey(split[0]))
                        this.Options[split[0]] = new List<string>();


                    if (!this.Options[split[0]].Any(m => m == split[1]) && split[1] != "start")
                        this.Options[split[0]].Add(split[1]);

                }

                if (split[1] != "end")
                {
                    if (!this.Options.ContainsKey(split[1]))
                        this.Options[split[1]] = new List<string>();

                    if (!this.Options[split[1]].Any(m => m == split[0]) && split[0] != "start")
                        this.Options[split[1]].Add(split[0]);
                }
            }

            //

            do
            {
                var currentPath = "start,";
                var travelledPaths = new List<string>();

                TraversePaths(ref currentPath, "start", ref travelledPaths);

                if (currentPath != "start,")
                    this.Paths.Add(currentPath);
                else
                    break;

            } while (true);

            // Display the results
            foreach(var path in this.Paths)
            {
                Console.WriteLine(path);
            }

        }

        public void TraversePaths(ref string currentPath, string key, ref List<string> travelledPaths)
        {
            if (key != "end" && !currentPath.EndsWith("end"))
            {
                foreach (var option in this.Options[key])
                {
                    if (!currentPath.EndsWith("end"))
                    {
                        var process = true;

                        // Check if we've hit the lower case one previously
                        // Check if we've travelled this path previously
                        if ((option.IsStringLowercase() && currentPath.IndexOf($",{option},") != -1) || travelledPaths.Contains($",{key},{option},"))
                            process = false;

                        if (process)
                        {
                            travelledPaths.Add($",{key},{option},");

                            //var text = currentPath + option;

                            currentPath += option + (option != "end" ? "," : "");
                            TraversePaths(ref currentPath, option, ref travelledPaths);
                        }
                    }
                    
                }
            }
        }

    }

    internal class Day12Node
    {
        public Day12Node()
        {
            Linked = new List<Day12Node>();
        }

        public string Point { get; set; } = "";

        public List<Day12Node> Linked { get; set; }
    }
}

//start-A
//start-b
//A-c
//A-b
//b-d
//A-end
//b-end