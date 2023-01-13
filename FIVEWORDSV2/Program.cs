using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace FiveWordFiveLetters
{
    public static class FiveWordFiveLetters
    {
        public static bool ContainsTwoSameLetters(string value)
        {
            // Initialize an array of 26 integers with all elements set to 0
            int[] charCounts = new int[26];

            // Convert the input string to lower case
            value = value.ToLower();

            // Iterate through each character of the input string
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                // Check if the current character is a letter
                if (c >= 'a' && c <= 'z')
                {
                    // Increment the count for the corresponding element in the charCounts array
                    charCounts[c - 'a']++;
                    // Check if the count for the current character is 2 or more
                    if (charCounts[c - 'a'] >= 2)
                    {
                        // Return true if it is
                        return true;
                    }
                }
            }
            // Return false if no character occurs twice or more
            return false;
        }


        static void Main(string[] args)
        {
            string filePath = "5WORDS.txt";
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            FindUniqueCombinations(filePath);
            stopWatch.Stop();

        }

        private static int ToNumber(char c)
        {
            return c - 'a';
        }
        private static int WordToMask(string word)
        {
            const int EXPECTED_LENGTH = 5;
            if (word.Length != EXPECTED_LENGTH) return 0;
            int result = 0;

            foreach (char c in word)
            {
                int bit = 1 << ToNumber(c);
                if ((result & bit) != 0) return 0; // a letter occurred twice
                result |= bit;
            }
            return result;
        }





        public static void FindUniqueCombinations(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var result = new List<string>();
            const int EXPECTED_LENGTH = 5;
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            stopWatch.Start();
            using (var reader = new StreamReader(filePath))
            {
                string word = null;
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length != EXPECTED_LENGTH) continue;
                    if (word.Distinct().Count() != word.Length) continue;
                    if (result.Where(x => string.Concat(x, word).Distinct().Count() == 5).Count() > 0) continue;
                    result.Add(word);
                }
                Console.WriteLine($"Words from input:  {result.Count}\n");
            }

            int numberOfWords = 0;

            for (int i = 0; i < result.Count; i++)
            {

                for (int j = i + 1; j < result.Count; j++)
                {
                    if (string.Concat(result[i], result[j]).Distinct().Count() != 10) continue;
                    for (int k = j + 1; k < result.Count; k++)
                    {
                        if (string.Concat(result[i], result[j], result[k]).Distinct().Count() != 15) continue;

                        for (int l = k + 1; l < result.Count; l++)
                        {
                            if (string.Concat(result[i], result[j], result[k], result[l]).Distinct().Count() != 20) continue;

                            for (int m = l + 1; m < result.Count; m++)
                            {

                                if (string.Concat(result[i], result[j], result[k], result[l], result[m]).Distinct().Count() != 25) continue;

                                Console.WriteLine("\r{0} {1} {2} {3} {4}", result[i], result[j], result[k], result[l], result[m]);
                                numberOfWords++;

                            }
                        }
                    }
                }
            }

            stopWatch.Stop();

            Console.WriteLine("\nNumber of combinations: " + numberOfWords);

            Console.WriteLine("Task was completed in " + stopWatch.Elapsed.TotalMilliseconds + " milliseconds");
        }
    }
}
