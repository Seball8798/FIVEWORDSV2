using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace FiveWordFiveLetters
{
    public static class FiveWordFiveLetters
    {
        private const int EXPECTED_LENGTH = 5;
        public static void Main(string[] args)
        {
            var words = LoadWords("5words.txt");
            Solve(words);
            Console.WriteLine("Number of solved words: " + count);
        }

        static List<string> LoadWords(string filename)
        {
            var words = new List<string>();
            var file = new StreamReader(filename);
            var seen = new HashSet<int>();

            string word = null;
            while ((word = file.ReadLine()) != null)
            {
                if (word.Length != EXPECTED_LENGTH) continue;
                var tmp = word.ToCharArray().OrderBy(c => c).ToArray();
                bool bad_word = false;
                for (int i = 0; i < 4; ++i)
                {
                    if (tmp[i] == tmp[i + 1])
                    {
                        bad_word = true;
                        break;
                    }
                }
                if (bad_word) continue;
                int hash = 0;
                for (int i = 0; i < 5; ++i)
                {
                    hash = hash * 26 + tmp[i] - 'a';
                }
                if (seen.Contains(hash)) continue;
                seen.Add(hash);
                words.Add(word);

            }
            return words;


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
                Console.WriteLine(i);

                int mask = 0;
                foreach (var c in words[i])
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
                stopwatch.Stop();
            }

            stopWatch.Stop();

            Console.WriteLine("\nNumber of combinations: " + numberOfWords);

            Console.WriteLine("Task was completed in " + stopWatch.Elapsed.TotalMilliseconds + " milliseconds");
        }

    }


}

