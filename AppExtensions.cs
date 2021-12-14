using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }

        public static IEnumerable<T> DeepCopy<T>(this IEnumerable<T> collectionToDeepCopy)
        {
            var serializedCollection = JsonConvert.SerializeObject(collectionToDeepCopy);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(serializedCollection);
        }

        public static bool IsStringLowercase(this string text)
        {            
            foreach(var c in text)
            {
                if (!Char.IsLower(c))
                    return false;

            }

            return true;
        }

        public static IEnumerable<int> GetAllIndexes(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
                index--;
            }
        }

    }
}
