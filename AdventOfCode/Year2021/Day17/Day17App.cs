using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day17App
    {
        //private List<string> Input = AppExtensions.GetInputList($"./Year2021/Day17/Input.txt");

        // Test Data
        //private int TargetXMin = 20;
        //private int TargetXMax = 30;
        //private int TargetYMin = -5;
        //private int TargetYMax = -10;

        // Part 1
        private int TargetXMin = 211;
        private int TargetXMax = 232;
        private int TargetYMin = -69;
        private int TargetYMax = -124;

        public void Execute()
        {
            var highestX = 0;
            var highestY = 0;

            var stepsToTry = 200;

            var successful = new List<string>();

            for(var x = 0; x <= TargetXMax; x++)
            {
                for(var y = TargetYMax; y <= stepsToTry; y++)
                {
                    var result = TestVelocity(x, y);

                    if (result != null)
                    {
                        successful.Add($"{x},{y}");

                        if (result.Item1 > highestX)
                            highestX = result.Item1;

                        if (result.Item2 > highestY)
                            highestY = result.Item2;
                    }
                }
            }

            var test = "";

        }

        private Tuple<int, int>? TestVelocity(int x, int y)
        {
            var velocityX = 0;
            var velocityY = 0;

            var highestX = 0;
            var highestY = 0;

            var hitsTarget = false;

            //var maxStepAttempts = 400;
            //for(var i = 0; i < maxStepAttempts; i++)
            while (true)
            {
                velocityX += x;
                velocityY += y;

                if (velocityX > highestX)
                    highestX = velocityX;

                if (velocityY > highestY)
                    highestY = velocityY;

                if (IsWithinTarget(velocityX, velocityY))
                {
                    hitsTarget = true;
                    break;
                }

                if (x > 0)
                    x--;

                y--;

                if (x == 0 && velocityX < this.TargetXMin || velocityY < this.TargetYMax)
                    break;
            }

            if (hitsTarget)
                return new Tuple<int, int>(highestX, highestY);

            return null;
        }

        private bool IsWithinTarget(int x, int y)
        {
            return x >= this.TargetXMin && x <= this.TargetXMax && y <= this.TargetYMin && y >= this.TargetYMax;
        }

    }
}
