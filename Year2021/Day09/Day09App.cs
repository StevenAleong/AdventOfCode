using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day09App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day09/Input.txt");

        private List<List<int>> Data = new List<List<int>>();

        public void Execute()
        {
            // Gather data into a grid
            for (var i = 0; i < this.Input.Count(); i++)
            {
                //data.Add(this.Input[i].ToCharArray().Select(m => new HeightPoint() { Height = Convert.ToInt32(m.ToString()) }).ToList());             
                this.Data.Add(this.Input[i].ToCharArray().Select(m => Convert.ToInt32(m.ToString())).ToList());
            }

            Part1();

            Part2();
        }

        private void Part1()
        {
            var output = 0L;

            for (var x = 0; x < this.Data.Count(); x++)
            {
                for(var y = 0; y < this.Data[x].Count(); y++)
                {
                    var currentHeight = this.Data[x][y];

                    var lowerUp = x > 0 ? currentHeight < this.Data[x - 1][y] : true;
                    var lowerRight = y < this.Data[x].Count() - 1 ? currentHeight < this.Data[x][y + 1] : true;
                    var lowerBottom = x < this.Data.Count() - 1 ? currentHeight < this.Data[x + 1][y] : true;
                    var lowerLeft = y > 0 ? currentHeight < this.Data[x][y - 1] : true;

                    if (lowerUp && lowerRight && lowerBottom && lowerLeft)
                    {
                        output += currentHeight + 1;
                    }
                }
            }

            Console.WriteLine($"The sum of the risk level for part 1 is: {output}");
        }

        private List<string> CheckedIndexes = new List<string>();

        private void Part2()
        {
            var basinSizes = new List<int>();

            for (var x = 0; x < this.Data.Count(); x++)
            {
                for (var y = 0; y < this.Data[x].Count(); y++)
                {
                    if (!CheckedIndexes.Any(m => m == $"{x},{y}") && this.Data[x][y] != 9)
                    {
                        var basinSize = 0;

                        GetBasinSize(ref basinSize, x, y);

                        if (basinSize > 0)
                            basinSizes.Add(basinSize);
                    }
                }
            }
            
            var output = basinSizes.OrderByDescending(m => m).Take(3).Aggregate((a, x) => a * x);
            Console.WriteLine($"The total of the three largest basins is: {output}");
        }

        private void GetBasinSize(ref int basinSize, int x, int y)
        {
            if (!CheckedIndexes.Any(m => m == $"{x},{y}"))
            {
                CheckedIndexes.Add($"{x},{y}");
                                
                if (this.Data[x][y] != 9)
                    basinSize++;

                // Check up
                if (x - 1 >= 0 && this.Data[x - 1][y] != 9)
                    GetBasinSize(ref basinSize, x - 1, y);

                // Check right
                if (y + 1 <= this.Data[x].Count() - 1 && this.Data[x][y + 1] != 9)
                    GetBasinSize(ref basinSize, x, y + 1);

                // Check below
                if (x + 1 <= this.Data.Count() - 1 && this.Data[x + 1][y] != 9)
                    GetBasinSize(ref basinSize, x + 1, y);

                // Check left
                if (y - 1 >= 0 && this.Data[x][y - 1] != 9)
                    GetBasinSize(ref basinSize, x, y - 1);

            }                
        }

    }

    internal class BasinResult
    {
        public int BasinSize { get; set; } = 0;
    }
}
