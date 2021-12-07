using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day5Pipe
    {
        public Point Entry { get; set; }
        public Point Exit { get; set; }

        public int HighestPoint { get; set; }

        public Day5Pipe(string input)
        {            
            var data = input.Split(new string[] { ",", "->" }, StringSplitOptions.RemoveEmptyEntries).Select(m => Convert.ToInt32(m)).ToList();

            this.HighestPoint = data.Max();
            this.Entry = new Point(data[0], data[1]);
            this.Exit = new Point(data[2], data[3]);

        }
    }
}
