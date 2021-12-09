using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day08App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day08/Input.txt");

        public void Execute()
        {
            var part1Output = this.Input.Select(m => DecodeDayOne(m)).ToList();
            Console.WriteLine($"The part 1 count is: {part1Output.Sum(m => m)}");

            var possibleCombinations = AppExtensions.GetPermutations(new List<string> { "A", "B", "C", "D", "E", "F", "G" }, 7);

            var part2Output = this.Input.Select(m => DecodeDayTwo(possibleCombinations, m)).ToList();
            Console.WriteLine($"The part 2 count is: {part2Output.Sum(m => m)}");
        }

        public int DecodeDayOne(string line)
        {
            var digits = new List<int>() { 2, 4, 3, 7 };

            var signalList = line.Split('|')[1].Split(' ').Where(m => !String.IsNullOrEmpty(m)).ToList();
            var result = signalList.Where(m => digits.Contains(m.Length)).Count();

            return result;
        }

        // 1 - 2 letters

        // 7 - 3 letters

        // 4 - 4 letters

        // 8 - 7 letters

        // 2 - 5 letters
        // 3 - 5 letters
        // 5 - 5 letters

        // 0 - 6 letters
        // 6 - 6 letters
        // 9 - 6 letters

        public int DecodeDayTwo(IEnumerable<IEnumerable<string>> possiblities, string line)
        {
            var signalList = line.Split('|')[0].Split(' ').Where(m => !String.IsNullOrEmpty(m)).OrderBy(m => m.Length).ToList();

            var dd = new Dictionary<int, char>();

            // Position 0 can always be guaranteed from the 2char signal
            dd[0] = signalList.First(m => m.Length == 3).ToList().Where(m => !signalList.First(sl => sl.Length == 2).Contains(m)).First();

            // Position 6: use all chars from 4 and pos0 and find the unique character from 9
            var charsToUse = signalList.First(m => m.Length == 4).ToList();
            charsToUse.Add(dd[0]);
            dd[6] = signalList.Where(m => m.Length == 6 && charsToUse.All(c => m.ToList().Any(x => c == x))).First().ToList()
                              .Where(m => !charsToUse.Contains(m)).First();

            // Position 3: Using 1 signal + the dd0 and dd6, we can figure out the middle position from the 3 signal
            charsToUse = signalList.First(m => m.Length == 2).ToList();
            charsToUse.Add(dd[0]);
            charsToUse.Add(dd[6]);
            dd[3] = signalList.Where(m => m.Length == 5 && charsToUse.All(c => m.ToList().Any(x => c == x))).First().ToList()
                              .Where(m => !charsToUse.Contains(m)).First();

            // position 2 and 5: Using 0 6 9 signals, of the 3, we need to find the one that doesn't have 1 of the parts from the 1 signal
            var oneSignalChars = signalList.First(m => m.Length == 2).ToCharArray();
            foreach(var sixLength in signalList.Where(m => m.Length == 6))
            {
                if (sixLength.IndexOf(oneSignalChars[0]) == -1)
                {
                    dd[2] = Convert.ToChar(oneSignalChars[0]);
                    dd[5] = Convert.ToChar(oneSignalChars[1]);
                    break;
                }
                else if (sixLength.IndexOf(oneSignalChars[1]) == -1)
                {
                    dd[2] = Convert.ToChar(oneSignalChars[1]);
                    dd[5] = Convert.ToChar(oneSignalChars[0]);
                    break;
                }
            }

            // Position 1 and 4 
            
            foreach(var fiveLength in signalList.Where(m => m.Length == 5))
            {
                if (fiveLength.IndexOf(dd[2]) == -1 && fiveLength.IndexOf(dd[5]) > -1)
                {
                    // 2 signal
                    dd[1] = fiveLength.Replace(dd[0].ToString(), "").Replace(dd[6].ToString(), "").Replace(dd[3].ToString(), "").Replace(dd[2].ToString(), "").Replace(dd[5].ToString(), "").First();

                } else if (fiveLength.IndexOf(dd[2]) > -1 && fiveLength.IndexOf(dd[5]) == -1)
                {
                    // 5 signal
                    dd[4] = fiveLength.Replace(dd[0].ToString(), "").Replace(dd[6].ToString(), "").Replace(dd[3].ToString(), "").Replace(dd[2].ToString(), "").Replace(dd[5].ToString(), "").First();
                }
            }

            // Compile all possible number strings
            /*     0000
            *     1    2
            *     1    2
            *      3333
            *     4    5
            *     4    5
            *      6666
            */

            var orderedDecoded = new Dictionary<string, int>();
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[1], dd[2], dd[4], dd[5], dd[6]).OrderBy(c => c)), 0); // 0
            orderedDecoded.Add(String.Concat(String.Concat(dd[2], dd[5]).OrderBy(c => c)), 1); // 1
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[2], dd[3], dd[4], dd[6]).OrderBy(c => c)), 2); // 2
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[2], dd[3], dd[5], dd[6]).OrderBy(c => c)), 3); // 3
            orderedDecoded.Add(String.Concat(String.Concat(dd[1], dd[2], dd[3], dd[5]).OrderBy(c => c)), 4); // 4
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[1], dd[3], dd[5], dd[6]).OrderBy(c => c)), 5); // 5
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[1], dd[3], dd[4], dd[5], dd[6]).OrderBy(c => c)), 6); // 6
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[2], dd[5]).OrderBy(c => c)), 7); // 7
            orderedDecoded.Add(String.Concat(String.Concat(dd.Select(m => m.Value)).OrderBy(c => c)), 8); // 8
            orderedDecoded.Add(String.Concat(String.Concat(dd[0], dd[1], dd[2], dd[3], dd[5], dd[6]).OrderBy(c => c)), 9); // 9

            // Get the output now
            var outputList = line.Split('|')[1].Split(' ').Where(m => !String.IsNullOrEmpty(m)).ToList();
            var stringNumber = "";
            foreach (var output in outputList)
            {
                stringNumber += orderedDecoded[String.Concat(output.OrderBy(c => c))].ToString();
            }

            return Convert.ToInt32(stringNumber);
        }
        
    }
}
