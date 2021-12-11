using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day11App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day11/Input.txt");

        private int Flashes = 0;

        public void Execute()
        {
            var dumbos = new List<List<int>>();

            // Gather data into a grid
            for (var i = 0; i < this.Input.Count(); i++)
            {
                dumbos.Add(this.Input[i].ToCharArray().Select(m => Convert.ToInt32(m.ToString())).ToList());
            }

            var daysToDo = 1000; // Change to 100 for part 1

            for (var day = 1; day <= daysToDo; day++)
            {

                for (var x = 0; x < dumbos.Count(); x++)
                {
                    for (var y = 0; y < dumbos[x].Count(); y++)
                    {
                        UpDumboPosition(ref dumbos, x, y);
                    }
                }

                // Reset all the dumbos to check if they've flashed
                ResetAllFlashed(ref dumbos);

                DisplayCurrentDumbos(ref dumbos, day);

                // For part 2
                if (DidAllFlashAtSameTime(ref dumbos))
                    break;
            }

            Console.WriteLine($"Flash Count: {this.Flashes}");
        }

        private bool DidAllFlashAtSameTime(ref List<List<int>> dumbos)
        {
            var count = 0;
            for (var x = 0; x < dumbos.Count(); x++)
            {
                for (var y = 0; y < dumbos[x].Count(); y++)
                {
                    if (dumbos[x][y] == 0)
                        count++;
                }
            }

            return count == 100;
        }

        private void DisplayCurrentDumbos(ref List<List<int>> dumbos, int day)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Day {day}");

            for (var x = 0; x < dumbos.Count(); x++)
            {
                for (var y = 0; y < dumbos[x].Count(); y++)
                {
                    if (dumbos[x][y] == 0)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"{dumbos[x][y]}");

                    if (dumbos[x][y] == 0)
                        Console.ResetColor();

                }
                Console.WriteLine("");
            }
        }

        private void ResetAllFlashed(ref List<List<int>> dumbos)
        {
            for (var x = 0; x < dumbos.Count(); x++)
            {
                for (var y = 0; y < dumbos[x].Count(); y++)
                {
                    if (dumbos[x][y] > 9)
                        dumbos[x][y] = 0;
                }
            }
        }

        private void UpDumboPosition(ref List<List<int>> dumbos, int x, int y)
        {
            dumbos[x][y]++;

            if (dumbos[x][y] == 10)
            {
                this.Flashes++;

                // Up Left
                if (x - 1 >= 0 && y - 1 >= 0)
                    UpDumboPosition(ref dumbos, x - 1, y - 1);

                // Up
                if (x - 1 >= 0)
                    UpDumboPosition(ref dumbos, x - 1, y);

                // Up Right
                if (x - 1 >= 0 && y + 1 < dumbos[x].Count())
                    UpDumboPosition(ref dumbos, x - 1, y + 1);

                // Left
                if (y - 1 >= 0)
                    UpDumboPosition(ref dumbos, x, y - 1);

                // Right
                if (y + 1 < dumbos[x].Count())
                    UpDumboPosition(ref dumbos, x, y + 1);

                // Down Left
                if (x + 1 < dumbos.Count() && y - 1 >= 0)
                    UpDumboPosition(ref dumbos, x + 1, y - 1);

                // Down
                if (x + 1 < dumbos.Count())
                    UpDumboPosition(ref dumbos, x + 1, y);

                // Down Right
                if (x + 1 < dumbos.Count() && y + 1 < dumbos[x].Count())
                    UpDumboPosition(ref dumbos, x + 1, y + 1);

            }
        }

    }

}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode.Year2021
//{
//    internal class Day11App
//    {
//        private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day11/Input.txt");

//        private int Flashes = 0;

//        public void Execute()
//        {
//            Part1();
//        }

//        private void Part1()
//        {
//            var dumbos = new List<List<Day11Dumbo>>();

//            // Gather data into a grid
//            for (var i = 0; i < this.Input.Count(); i++)
//            {
//                dumbos.Add(this.Input[i].ToCharArray().Select(m => new Day11Dumbo() { EnergyLevel = Convert.ToInt32(m.ToString()) }).ToList());
//            }

//            var daysToDo = 100;

//            for (var day = 1; day <= daysToDo; day++)
//            {

//                for (var x = 0; x < dumbos.Count(); x++)
//                {
//                    for (var y = 0; y < dumbos[x].Count(); y++)
//                    {
//                        UpDumboPosition(ref dumbos, x, y);                       
//                    }
//                }

//                // Reset all the dumbos to check if they've flashed
//                ResetAllFlashed(ref dumbos);

//                DisplayCurrentDumbos(ref dumbos, day);
//            }

//            Console.WriteLine($"Part 1 Flash Count: {this.Flashes}");
//        }

//        private void DisplayCurrentDumbos(ref List<List<Day11Dumbo>> dumbos, int day) {
//            Console.WriteLine("------------------------------------");
//            Console.WriteLine($"Day {day}");

//            for (var x = 0; x < dumbos.Count(); x++)
//            {
//                for (var y = 0; y < dumbos[x].Count(); y++)
//                {
//                    if (dumbos[x][y].EnergyLevel == 0)
//                        Console.ForegroundColor = ConsoleColor.Red;

//                    Console.Write($"{dumbos[x][y].EnergyLevel}");

//                    if (dumbos[x][y].EnergyLevel == 0)
//                        Console.ResetColor();

//                }
//                Console.WriteLine("");
//                //Console.WriteLine(String.Join("", dumbos[x].Select(m => m.EnergyLevel).ToList()));
//            }
//        }

//        private void ResetAllFlashed(ref List<List<Day11Dumbo>> dumbos)
//        {
//            for (var x = 0; x < dumbos.Count(); x++)
//            {
//                for (var y = 0; y < dumbos[x].Count(); y++)
//                {
//                    dumbos[x][y].Flashed = false;
//                    if (dumbos[x][y].EnergyLevel > 9)
//                    {
//                        dumbos[x][y].EnergyLevel = 0;
//                    }
//                }
//            }
//        }

//        private void UpDumboPosition(ref List<List<Day11Dumbo>> dumbos, int x, int y)
//        {
//            dumbos[x][y].EnergyLevel++;

//            if (dumbos[x][y].EnergyLevel >= 10 && dumbos[x][y].Flashed == false)
//            {
//                this.Flashes++;
//                dumbos[x][x].Flashed = true;

//                // Up Left
//                if (x - 1 >= 0 && y - 1 >= 0)
//                    UpDumboPosition(ref dumbos, x - 1, y - 1);

//                // Up
//                if (x - 1 >= 0)
//                    UpDumboPosition(ref dumbos, x - 1, y);

//                // Up Right
//                if (x - 1 >= 0 && y + 1 < dumbos[x].Count())
//                    UpDumboPosition(ref dumbos, x - 1, y + 1);

//                // Left
//                if (y - 1 >= 0)
//                    UpDumboPosition(ref dumbos, x, y - 1);

//                // Right
//                if (y + 1 < dumbos[x].Count())
//                    UpDumboPosition(ref dumbos, x, y + 1);

//                // Down Left
//                if (x + 1 < dumbos.Count() && y - 1 >= 0)
//                    UpDumboPosition(ref dumbos, x + 1, y - 1);

//                // Down
//                if (x + 1 < dumbos.Count())
//                    UpDumboPosition(ref dumbos, x + 1, y);

//                // Down Right
//                if (x + 1 < dumbos.Count() && y + 1 < dumbos[x].Count())
//                    UpDumboPosition(ref dumbos, x + 1, y + 1);

//            }
//        }

//    }

//    internal class Day11Dumbo
//    {
//        public int EnergyLevel { get; set; } = 0;
//        public bool Flashed { get; set; } = false;
//    }
//}
