using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day14App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day14/Input.txt");

        public void Execute()
        {
            var pairs = new Dictionary<string, string>();
            foreach (var line in this.Input)
            {
                var split = line.Split(" -> ").ToList();
                pairs.Add(split[0], split[1]);
            }

            // Second attempt
            // Can we make buckets?
            var polymerTemplate = "HBHVVNPCNFPSVKBPPCBH";

            var output = new Dictionary<string, long>();

            // Get starting buckets
            for (var i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var key = String.Join("", polymerTemplate.Skip(i).Take(2).ToList());

                if (output.ContainsKey(key))
                    output[key]++;
                else
                    output.Add(key, 1);
            }

            // Loop through steps
            for (var steps = 1; steps <= 40; steps++)
            {
                var toAdd = new Dictionary<string, long>();

                foreach(var pair in pairs)
                {
                    if (output.ContainsKey(pair.Key) && output[pair.Key] > 0)
                    {
                        var currentCount = output[pair.Key];
                        output[pair.Key] = 0;

                        var leftKey = $"{pair.Key[0]}{pair.Value}";
                        if (toAdd.ContainsKey(leftKey))
                            toAdd[leftKey] += currentCount;
                        else
                            toAdd.Add(leftKey, currentCount);

                        var rightKey = $"{pair.Value}{pair.Key[1]}";
                        if (toAdd.ContainsKey(rightKey))
                            toAdd[rightKey] += currentCount;
                        else
                            toAdd.Add(rightKey, currentCount);
                    }
                }

                foreach(var add in toAdd)
                {
                    if (output.ContainsKey(add.Key))
                        output[add.Key] += add.Value;
                    else
                        output.Add(add.Key, add.Value);
                }

            }

            var letters = new Dictionary<char, long>();
            foreach (var pair in output)
            {
                if (letters.ContainsKey(pair.Key[0]))
                    letters[pair.Key[0]] += pair.Value;
                else
                    letters.Add(pair.Key[0], pair.Value);

                //if (letters.ContainsKey(pair.Key[1]))
                //    letters[pair.Key[1]] += pair.Value;
                //else
                //    letters.Add(pair.Key[1], pair.Value);
            }
            letters[polymerTemplate[polymerTemplate.Length - 1]]++;

            var highest = letters.OrderByDescending(m => m.Value).First().Value;
            var lowest = letters.OrderBy(m => m.Value).First().Value;

            Console.WriteLine($"The answer is {highest - lowest}");


            // NN 1
            // NC 1
            // CB 1



            // First attempt.
            // Actually build the string... seems to time out

            //var polymerTemplate = "HBHVVNPCNFPSVKBPPCBH";

            //for (var steps = 1; steps <= 10; steps++)
            //{
            //    var toInsert = new Dictionary<int, string>();

            //    // build strings to insert at indexes
            //    foreach(var pair in pairs)
            //    {
            //        if (polymerTemplate.IndexOf(pair.Key) > -1) {

            //            // Find all indexes of the pair
            //            var allIndexes = AppExtensions.GetAllIndexes(polymerTemplate, pair.Key);

            //            foreach(var index in allIndexes)
            //            {
            //                if (toInsert.ContainsKey(index + 1))
            //                    toInsert[index + 1] += pair.Value;
            //                else
            //                    toInsert.Add(index + 1, pair.Value);
            //            }
            //        }
            //    }

            //    // Insert from back to front.
            //    foreach(var key in toInsert.Keys.OrderByDescending(m => m))
            //    {
            //        polymerTemplate = polymerTemplate.Insert(key, toInsert[key]);
            //    }

            //    //Console.WriteLine(polymerTemplate);
            //}

            //var results = polymerTemplate.ToCharArray().GroupBy(m => m).Select(m => new { Count = m.Count(), Letter = m.ToString() }).OrderByDescending(m => m.Count);

            //var highest = results.First().Count;
            //var lowest = results.Last().Count;

            //Console.WriteLine($"The answer is {highest - lowest}");


        }

    }
}
