using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day10App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day10/Input.txt");


        private List<char> Openings = new List<char>() { '(', '[', '{', '<' };

        private List<char> Closings = new List<char>() { ')', ']', '}', '>' };

        public void Execute()
        {

            Part1();
            Part2();
        }

        public void Part1()
        {
            var total = 0L;

            var pointSystem = new Dictionary<string, int>();
            pointSystem.Add(")", 3);
            pointSystem.Add("]", 57);
            pointSystem.Add("}", 1197);
            pointSystem.Add(">", 25137);

            foreach (var line in this.Input)
            {
                var result = IsLineCorrupted(line);
                if (result != "")
                {
                    total += pointSystem[result];
                }
                //Console.WriteLine($"Line Corrupted: {IsLineCorrupted(line) != ""} - Line Complete: {IsLineComplete(line)} - {line}");
            }

            Console.WriteLine($"The total for part 1 is: {total}");
        }

        public void Part2()
        {
            var pointSystem = new Dictionary<string, int>();
            pointSystem.Add(")", 1);
            pointSystem.Add("]", 2);
            pointSystem.Add("}", 3);
            pointSystem.Add(">", 4);

            var totals = new List<long>();

            foreach (var line in this.Input)
            {
                var result = IsLineComplete(line);
                if (result.Length > 0 && IsLineCorrupted(line) == "")
                {
                    var missing = CompleteMissingBrackets(result);

                    var total = 0L;
                    foreach (var b in missing.ToCharArray())
                    {
                        total = (total * 5) + pointSystem[b.ToString()];
                    }

                    totals.Add(total);

                }

            }

            var middle = totals.OrderBy(x => x).Skip((int)Math.Floor((decimal)totals.Count / (decimal)2)).First();

            Console.WriteLine($"The middle for part 2 is: {middle}");

        }

        public string CompleteMissingBrackets(string line)
        {
            var pairs = new List<string>() { "[]", "{}", "()", "<>" };

            while (pairs.Any(m => line.IndexOf(m) > -1))
            {
                foreach (var pair in pairs)
                {
                    line = line.Replace(pair, "");
                }
            }

            line = line.Replace("(", ")").Replace("{", "}").Replace("[", "]").Replace("<", ">");

            return AppExtensions.ReverseString(line);
        }

        public string IsLineCorrupted(string line)
        {
            var bucket = new List<char>();

            var output = "";

            foreach(var c in line)
            {
                // If it's an opening, add it to the bucket
                if (this.Openings.Contains(c)) {
                    bucket.Insert(0, c);
                }

                // If it's a closing, check to see if we can close it
                switch(c)
                {
                    case ')':
                        if (bucket[0] != '(')
                            output = c.ToString();
                        else
                            bucket.RemoveAt(0);
                        break;

                    case ']':
                        if (bucket[0] != '[')
                            output = c.ToString();
                        else
                            bucket.RemoveAt(0);
                        break;

                    case '}':
                        if (bucket[0] != '{')
                            output = c.ToString();
                        else
                            bucket.RemoveAt(0);
                        break;

                    case '>':
                        if (bucket[0] != '<')
                            output = c.ToString();
                        else
                            bucket.RemoveAt(0);
                        break;
                }

                if (output != "")
                    break;                
            }

            return output;
        }

        public string IsLineComplete(string line)
        {
            var pairs = new List<string>() { "[]", "{}", "()", "<>" };

            while (pairs.Any(m => line.IndexOf(m) > -1))
            {
                foreach (var pair in pairs)
                {
                    line = line.Replace(pair, "");
                }
            }

            return line;
        }

    }
}
