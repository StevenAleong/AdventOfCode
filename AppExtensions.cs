using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class AppExtensions
    {
        public static List<string> GetInputList(string relativePath)
        {
            return System.IO.File.ReadAllLines(Path.GetFullPath(relativePath)).ToList();
        }

        public static string GetInputString(string relativePath)
        {
            if (System.IO.File.Exists(Path.GetFullPath(relativePath)))
                return System.IO.File.ReadAllText(Path.GetFullPath(relativePath));

            return "";
        }
    }
}
