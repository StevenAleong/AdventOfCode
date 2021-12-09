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


        /*      0000
         *     1    2
         *     1    2
         *      3333
         *     4    5
         *     4    5
         *      6666
         */

        

        public int DecodeDayTwo(IEnumerable<IEnumerable<string>> possiblities, string line)
        {
            var signalList = line.Split('|')[0].Split(' ').Where(m => !String.IsNullOrEmpty(m)).OrderBy(m => m.Length).ToList();

            

            //// 
            //var dd = new Dictionary<int, string>();
            //dd[1] = signalList.First(m => m.Length == 2);
            //dd[4] = signalList.First(m => m.Length == 4);
            //dd[7] = signalList.First(m => m.Length == 3);
            //dd[8] = signalList.First(m => m.Length == 8);





            //var dd = new Dictionary<int, char>();

            //// Position 0 can always be guaranteed from the 2char signal
            //dd[0] = signalList.First(m => m.Length == 3).ToList().Where(m => !signalList.First(sl => sl.Length == 2).Contains(m)).First();

            //// Position 6: use all chars from 4 and pos0 and find the unique character from 9
            //var charsToUse = signalList.First(m => m.Length == 4).ToList();
            //charsToUse.Add(dd[0]);
            //dd[6] = signalList.Where(m => m.Length == 6 && charsToUse.All(c => m.ToList().Any(x => c == x))).First().ToList()
            //                  .Where(m => !charsToUse.Contains(m)).First();

            //// Using 1 signal + the dd0 and dd6, we can figure out the middle position from the 3 signal
            //charsToUse = signalList.First(m => m.Length == 2).ToList();
            //charsToUse.Add(dd[0]);
            //charsToUse.Add(dd[6]);
            //dd[3] = signalList.Where(m => m.Length == 5 && charsToUse.All(c => m.ToList().Any(x => c == x))).First().ToList()
            //                  .Where(m => !charsToUse.Contains(m)).First();

            //// Using 4 signal + the unique char from difference from 1
            //charsToUse = signalList.First(m => m.Length == 4).ToList();
            //charsToUse.Add(dd[])

            // Get the output now
            //var outputList = line.Split('|')[1].Split(' ').Where(m => !String.IsNullOrEmpty(m)).ToList();
            var stringNumber = "0";
            //foreach(var output in outputList)
            //{
            //    stringNumber += orderedDecoded[String.Concat(output.OrderBy(c => c))].ToString();
            //}

            return Convert.ToInt32(stringNumber);
        }
        
    }
}
