using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode.Year2023
{
    internal class Day10App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day10/input.txt");

        private List<char[]> maze = new List<char[]>();
        private List<char[]> maze2 = new List<char[]>();

        private int[,] grid = new int[0, 0];

        public void Execute() {
            var startingRow = 0;
            var startingColumn = 0;

            for (var l = 0; l < Input.Count; l++) {
                var line = Input[l];

                var nodes = line.ToCharArray();

                if (nodes.Any(m => m == 'S')) {
                    startingRow = l;
                    startingColumn = Array.IndexOf(nodes, 'S');
                }

                maze.Add(nodes);
            }

            maze2 = maze.ToList();

            grid = new int[maze.Count, maze[0].Length];

            Part1(startingRow, startingColumn);

            int furthestStep = 0;

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    //Console.Write(grid[i, j] + "\t");

                    if (grid[i, j] > furthestStep) {
                        furthestStep = grid[i, j];
                    }
                }
                //Console.WriteLine();
            }

            //Console.WriteLine();
            Console.WriteLine(furthestStep);

            Part2();

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    if (maze[i][j] != '.' || grid[i, j] == -1) {
                        grid[i, j] = -2;
                        maze2[i][j] = 'x';
                    }
                }
            }

            int countOfZeros = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    Console.Write(maze2[i][j]);

                    if (grid[i, j] == 0) {
                        countOfZeros++;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(countOfZeros);
        }

        // Learned about this Queue object
        public void Part1(int startingRow, int startingColumn) 
        {
            Queue<(int step, int row, int column)> queue = new Queue<(int, int, int)>();
            queue.Enqueue((0, startingRow, startingColumn));

            while (queue.Count > 0) {
                var (previousStep, row, column) = queue.Dequeue();

                if (grid[row, column] == 0 || previousStep < grid[row, column]) {
                    grid[row, column] = previousStep;

                    var nextStep = previousStep + 1;

                    // Get N node
                    if (row > 0) {
                        var northNode = maze[row - 1][column];
                        if (northNode == '|' || northNode == '7' || northNode == 'F') {
                            queue.Enqueue((nextStep, row - 1, column));
                        }
                    }

                    // Get E node 
                    if (column < maze[0].Length - 1) {
                        var eastNode = maze[row][column + 1];
                        if (eastNode == '-' || eastNode == 'J' || eastNode == '7') {
                            queue.Enqueue((nextStep, row, column + 1));
                        }
                    }

                    // Get S node
                    if (row < maze.Count - 1) {
                        var southNode = maze[row + 1][column];
                        if (southNode == '|' || southNode == 'L' || southNode == 'J') {
                            queue.Enqueue((nextStep, row + 1, column));
                        }
                    }

                    // Get W node
                    if (column > 0) {
                        var westNode = maze[row][column - 1];
                        if (westNode == '-' || westNode == 'L' || westNode == 'F') {
                            queue.Enqueue((nextStep, row, column - 1));
                        }
                    }
                }
            }

            //TraverseMaze(0, startingRow, startingColumn);
        }

        /// <summary>
        /// Does not do what is needed. just part 1 works only. me and my homies hate part 2
        /// </summary>
        public void Part2() {
            Queue<(int row, int column)> queue = new Queue<(int, int)>();

            for (var line = 0; line < maze.Count; line++) {
                if (line == 0 || line == maze.Count - 1) {
                    for (var y = 0; y < maze[line].Length; y++) {
                        var c = maze[line][y];
                        if (c == '.') {
                            queue.Enqueue((line, y));
                        }
                    }

                } else {
                    var left = maze[line][0];
                    if (left == '.') {
                        queue.Enqueue((line, 0));
                    }

                    var right = maze[line][maze[line].Length - 1];
                    if (right == '.') {
                        queue.Enqueue((line, maze[line].Length - 1));
                    }
                }
            }

            while (queue.Count > 0) {
                var (row, column) = queue.Dequeue();

                if (grid[row, column] == 0) {
                    grid[row, column] = -1;

                    // Get N node
                    if (row > 0) {
                        var northNode = maze[row - 1][column];
                        if (northNode == '.') {
                            queue.Enqueue((row - 1, column));
                        }
                    }

                    // Get NE node
                    if (row > 0 && column < maze[0].Length - 1) {
                        var northNode = maze[row - 1][column];
                        var eastNode = maze[row][column + 1];
                        var neNode = maze[row - 1][column + 1];

                        if (neNode == '.' && northNode == 'J' && eastNode == 'F') {
                            queue.Enqueue((row - 1, column + 1));
                        }
                    }

                    // Get E node 
                    if (column < maze[0].Length - 1) {
                        var eastNode = maze[row][column + 1];
                        if (eastNode == '.') {
                            queue.Enqueue((row, column + 1));
                        }
                    }

                    // Get SE node
                    if (row < maze.Count - 1 && column < maze[0].Length - 1) {
                        var southNode = maze[row + 1][column];
                        var eastNode = maze[row][column + 1];
                        var seNode = maze[row + 1][column + 1];

                        if (seNode == '.' && southNode == '7' && eastNode == 'L') {
                            queue.Enqueue((row + 1, column + 1));
                        }
                    }

                    // Get S node
                    if (row < maze.Count - 1) {
                        var southNode = maze[row + 1][column];
                        if (southNode == '.') {
                            queue.Enqueue((row + 1, column));
                        }
                    }

                    // Get SW node
                    if (row < maze.Count - 1 && column > 0) {
                        var southNode = maze[row + 1][column];
                        var westNode = maze[row][column - 1];
                        var swNode = maze[row + 1][column - 1];

                        if (swNode == '.' && southNode == 'F' && westNode == 'J') {
                            queue.Enqueue((row + 1, column - 1));
                        }
                    }

                    // Get W node
                    if (column > 0) {
                        var westNode = maze[row][column - 1];
                        if (westNode == '.') {
                            queue.Enqueue((row, column - 1));
                        }
                    }

                    // Get NW node
                    if (row > 0 && column > 0) {
                        var northNode = maze[row - 1][column];
                        var westNode = maze[row][column - 1];
                        var nwNode = maze[row - 1][column - 1];

                        if (nwNode == '.' && northNode == 'L' && westNode == '7') {
                            queue.Enqueue((row - 1, column - 1));
                        }
                    }
                }

            }
        }

        // Recursion that failed.
        // Stack overflowed
        private void TraverseMaze(int previousStep, int row, int column) {
            if (grid[row, column] == 0 || previousStep < grid[row, column]) {
                grid[row, column] = previousStep;

                // Get N node
                if (row > 0) {
                    var northNode = maze[row - 1][column];
                    if (northNode == '|' || northNode == '7' || northNode == 'F') {
                        TraverseMaze(previousStep + 1, row - 1, column);
                    }
                }

                // Get E node 
                if (column < maze[0].Length - 1) {
                    var eastNode = maze[row][column + 1];
                    if (eastNode == '-' || eastNode == 'J' || eastNode == '7') {
                        TraverseMaze(previousStep + 1, row, column + 1);
                    }
                }

                // Get S node
                if (row < maze.Count - 1) {
                    var southNode = maze[row + 1][column];
                    if (southNode == '|' || southNode == 'L' || southNode == 'J') {
                        TraverseMaze(previousStep + 1, row + 1, column);
                    }
                }

                // Get W node
                if (column > 0) {
                    var westNode = maze[row][column - 1];
                    if (westNode == '-' || westNode == 'L' || westNode == 'F') {
                        TraverseMaze(previousStep + 1, row, column - 1);
                    }
                }
            }
        }


    }
}
