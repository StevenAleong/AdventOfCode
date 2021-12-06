using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class AppExtensions
    {
        public static List<string> GetInputList(string path)
        {
            return System.IO.File.ReadAllLines(path).ToList();
        }

        public static string GetInputString(string path)
        {
            if (System.IO.File.Exists(path))
                return System.IO.File.ReadAllText(path);

            return "";
        }
    }
}
