using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day4App
    {
        private string Input = AppExtensions.GetInputString(@"./Year2021/Day04/Input.txt");

        private List<int> Numbers = "99,56,7,15,81,26,75,40,87,59,62,24,58,34,78,86,44,65,18,94,20,17,98,29,57,92,14,32,46,79,85,84,35,68,55,22,41,61,90,11,69,96,23,47,43,80,72,50,97,33,53,25,28,51,49,64,12,63,21,48,27,19,67,88,66,45,3,71,16,70,76,13,60,77,73,1,8,10,52,38,36,74,83,2,37,6,31,91,89,54,42,30,5,82,9,95,93,4,0,39".Split(",").Select(m => Convert.ToInt32(m)).ToList();

        public void Execute()
        {
            var splitLines = this.Input.Split(Environment.NewLine + Environment.NewLine);

            // Prepare all the boards
            var allBoards = new List<Day4Board>();
            foreach (var lineString in splitLines)
            {
                var board = new Day4Board();

                foreach (var line in lineString.Split(Environment.NewLine))
                {
                    board.AddLine(line);
                }

                allBoards.Add(board);                
            }

            var pickedNumbers = new List<int>();

            var count = 1;
            // Now process each picked number in order
            foreach(var number in this.Numbers)
            {
                Console.WriteLine($"{count}: {number}");
                pickedNumbers.Add(number);

                foreach(var board in allBoards)
                {
                    if (board.AnyLineMatched(pickedNumbers))
                    {
                        if (board.AlreadyWon == false)
                        {
                            Console.Write("The Board Score is: ");
                            Console.Write(board.FinalScore(pickedNumbers, number).ToString());
                            Console.WriteLine("");
                            board.AlreadyWon = true;
                        }                        
                    }
                }

                count++;
            }
        }

    }
}
