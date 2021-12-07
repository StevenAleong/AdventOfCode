using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day4Board
    {
        public Day4Board()
        {
            Numbers = new List<int>();
            HorizontalLines = new List<List<int>>();
        }

        public List<List<int>> BoardLines {  
            get
            {
                var output = new List<List<int>>();
                output.AddRange(this.HorizontalLines);
                output.AddRange(this.VerticalLines);
                return output;
            }
        }

        public void AddLine(string line)
        {
            var numbers = line.Split(" ").Where(m => m.Trim() != "").Select(m => Convert.ToInt32(m)).ToList();
            this.Numbers.AddRange(numbers);
            this.HorizontalLines.Add(numbers);
        }

        public bool AnyLineMatched(List<int> currentNumbers)
        {
            foreach(var line in BoardLines)
            {
                if (line.All(m => currentNumbers.Contains(m)))
                {
                    return true;
                }
            }

            return false;
        }

        public int FinalScore(List<int> currentNumbers, int lastNumberCalled)
        {
            var leftOverNumbers = this.Numbers.Except(currentNumbers);
            var total = leftOverNumbers.Sum();

            return total * lastNumberCalled;
        }

        public List<int> Numbers { get; set; }

        public bool AlreadyWon { get; set; } = false;

        public List<List<int>> HorizontalLines { get; set; }

        public List<List<int>> VerticalLines {
            get
            {
                var output = new List<List<int>>();

                if (this.HorizontalLines != null)
                {
                    for(var i = 0; i < this.HorizontalLines.Count; i++) {
                        var line = new List<int>()
                        {
                            this.HorizontalLines[0][i],
                            this.HorizontalLines[1][i],
                            this.HorizontalLines[2][i],
                            this.HorizontalLines[3][i],
                            this.HorizontalLines[4][i]
                        };
                        output.Add(line);
                    }
                }

                return output;
            }
        }
    }
}
