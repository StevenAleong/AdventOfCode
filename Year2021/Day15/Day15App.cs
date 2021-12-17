using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day15App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day15/Input.txt");

        private List<List<int>> Cave = new List<List<int>>();

        public void Execute()
        {
            this.Cave = this.Input.ConvertStringListTo2DIntArray();

            var start = new Coord() { 
                Total = 0,
                ContainsExit = false,
                Y = 0,
                X = 0
            };

            var total = 0L;

            var tile = GetNextPath(0, 0);
            while (tile.ContainsExit == false)
            {
                total += this.Cave[tile.Y][tile.X];
                
                tile = GetNextPath(tile.Y, tile.X);
            }

            total += tile.Total;

            Console.WriteLine($"The path cost is {total}");
        }

        private Coord GetNextPath(int Y, int X)
        {
            // 0: 0 1 2 3
            // 1: 0 1 2 
            // 2: 0 1 
            // 3: 0

            // Paths
            // 0,0 0,1 0,2 0,3
            // 0,0 0,1 0,2 1,2
            // 0,0 0,1 1,1 1,2
            // 0,0 0,1 1,1 2,2
            // 0,0 1,0 1,1 1,2
            // 0,0 1,0 1,1 2,1
            // 0,0 1,0 2,0 2,1
            // 0,0 1,0 2,0 3,0


            var paths = new List<string>();
            //paths.Add("0,0 0,1 0,2 0,3");
            //paths.Add("0,0 0,1 0,2 1,2");
            //paths.Add("0,0 0,1 1,1 1,2");
            //paths.Add("0,0 0,1 1,1 2,2");
            //paths.Add("0,0 1,0 1,1 1,2");
            //paths.Add("0,0 1,0 1,1 2,1");
            //paths.Add("0,0 1,0 2,0 2,1");
            //paths.Add("0,0 1,0 2,0 3,0");


            // 0: 0 1 2 3 4
            // 1: 0 1 2 3 
            // 2: 0 1 2
            // 3: 0 1
            // 4: 0

            var ignore = new List<Coord>()
            {
                new Coord() { Y = 1, X = 4 },
                new Coord() { Y = 2, X = 3 },
                new Coord() { Y = 2, X = 4 },
                new Coord() { Y = 3, X = 2 },
                new Coord() { Y = 3, X = 3 },
                new Coord() { Y = 3, X = 4 },
                new Coord() { Y = 4, X = 1 },
                new Coord() { Y = 4, X = 2 },
                new Coord() { Y = 4, X = 3 },
                new Coord() { Y = 4, X = 4 }
            };

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (!ignore.Any(m => m.X == x && m.Y == y))
                    {
                        //paths.Add(new Coord() { Y = y, X = x });
                    }
                }
            }


            var results = new List<Coord>();

            var maxX = this.Cave[0].Count() - 1;
            var maxY = this.Cave.Count() - 1;

            foreach (var path in paths)
            {
                var total = 0;
                var options = path.Split(' ').ToList();
                var fullPath = true;
                var containsExit = false;
                var point = new Coord();

                for (var i = 0; i < options.Count(); i++)
                {
                    var pointY = Convert.ToInt32(options[i][0].ToString()) + Y;
                    var pointX = Convert.ToInt32(options[i][2].ToString()) + X;

                    if (pointY > maxY || pointX > maxX)
                    {
                        fullPath = false;
                        break;
                    }

                    if (pointY == maxY && pointX == maxX)
                        containsExit = true;

                    if (i + 1 == 2)
                    {
                        point.X = pointX;
                        point.Y = pointY;
                    }

                    total += this.Cave[pointY][pointX];
                }

                if (fullPath)
                {
                    point.ContainsExit = containsExit;
                    point.Total = total;

                    results.Add(point);
                }

            }

            // Find the lowest total
            return results.OrderBy(m => m.Total).First();
        }        

    }

    internal class Coord
    {
        public bool ContainsExit { get; set; } = false;
        public int Total { get; set; } = 0;
        public int X { get; set; }
        public int Y { get; set; }
    }
}
