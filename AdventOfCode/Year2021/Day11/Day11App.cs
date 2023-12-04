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
                        UpDumboPosition(ref dumbos, y, x);
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
            for (var y = 0; y < dumbos.Count(); y++)
            {
                for (var x = 0; x < dumbos[y].Count(); x++)
                {
                    if (dumbos[y][x] == 0)
                        count++;
                }
            }

            return count == 100;
        }

        private void DisplayCurrentDumbos(ref List<List<int>> dumbos, int day)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Day {day}");

            for (var y = 0; y < dumbos.Count(); y++)
            {
                for (var x = 0; x < dumbos[y].Count(); x++)
                {
                    if (dumbos[y][x] == 0)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"{dumbos[y][x]}");

                    if (dumbos[y][x] == 0)
                        Console.ResetColor();

                }
                Console.WriteLine("");
            }
        }

        private void ResetAllFlashed(ref List<List<int>> dumbos)
        {
            for (var y = 0; y < dumbos.Count(); y++)
            {
                for (var x = 0; x < dumbos[y].Count(); x++)
                {
                    if (dumbos[y][x] > 9)
                        dumbos[y][x] = 0;
                }
            }
        }

        private void UpDumboPosition(ref List<List<int>> dumbos, int y, int x)
        {
            dumbos[y][x]++;

            if (dumbos[y][x] == 10)
            {
                this.Flashes++;

                // Up Left
                if (y - 1 >= 0 && x - 1 >= 0)
                    UpDumboPosition(ref dumbos, y - 1, x - 1);

                // Up
                if (y - 1 >= 0)
                    UpDumboPosition(ref dumbos, y - 1, x);

                // Up Right
                if (y - 1 >= 0 && x + 1 < dumbos[y].Count())
                    UpDumboPosition(ref dumbos, y - 1, x + 1);

                // Left
                if (x - 1 >= 0)
                    UpDumboPosition(ref dumbos, y, x - 1);

                // Right
                if (x + 1 < dumbos[y].Count())
                    UpDumboPosition(ref dumbos, y, x + 1);

                // Down Left
                if (y + 1 < dumbos.Count() && x - 1 >= 0)
                    UpDumboPosition(ref dumbos, y + 1, x - 1);

                // Down
                if (y + 1 < dumbos.Count())
                    UpDumboPosition(ref dumbos, y + 1, x);

                // Down Right
                if (y + 1 < dumbos.Count() && x + 1 < dumbos[y].Count())
                    UpDumboPosition(ref dumbos, y + 1, x + 1);

            }
        }

    }

}
