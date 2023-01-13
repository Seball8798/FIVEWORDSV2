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
            string filePath = "5words.txt";
            Dictionary<int, string> _dictionary = new Dictionary<int, string>();
            List<int> bitlist = new List<int>();
            string[] lines = File.ReadAllLines(filePath);

            var result = new List<string>();


            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            stopWatch.Start();
            using (var reader = new StreamReader(filePath))
            {
                string word = null;
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length != 5) continue;
                    if (word.Distinct().Count() != word.Length) continue;
                    // if (result.Where(x => string.Concat(x, word).Distinct().Count() == 5).Count() > 0) continue;
                    //result.Add(word);

                    int bitRepresentation = 0;
                    foreach (char c in word) bitRepresentation += (int)1 << (c - 'a');

                    if (_dictionary.ContainsKey(bitRepresentation)) continue;
                    _dictionary.Add(bitRepresentation, word);
                    bitlist.Add(bitRepresentation);




                }
                Console.WriteLine($"Words from input:  {bitlist.Count}\n");
            }


            int numberOfWords = 0;

            for (int i = 0; i < bitlist.Count; i++)
            {

                for (int j = i + 1; j < bitlist.Count; j++)
                {
                    if ((bitlist[i] | bitlist[j]) != 10) continue;
                    for (int k = j + 1; k < bitlist.Count; k++)
                    {
                        if ((bitlist[i] | bitlist[j] | bitlist[k]) != 15) continue;

                        for (int l = k + 1; l < bitlist.Count; l++)
                        {
                            if ((bitlist[i] | bitlist[j] | bitlist[k] | bitlist[l]) != 20) continue;

                            for (int m = l + 1; m < bitlist.Count; m++)
                            {

                                if ((bitlist[i] | bitlist[j] | bitlist[k] | bitlist[l] | bitlist[m]) != 25) continue;

                                Console.WriteLine("\r{0} {1} {2} {3} {4}", bitlist[i], bitlist[j], bitlist[k], bitlist[l], bitlist[m]);
                                numberOfWords++;

                            }

                            //var binaryRepresentation = result.Select(word => string.Join("", Encoding.ASCII.GetBytes(word).Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))));
                            //Console.WriteLine(string.Join(" ", binaryRepresentation));
                            //numberOfWords++;


                        }
                    }
                }
            }


            Console.WriteLine("\nNumber of combinations: " + numberOfWords);
            stopWatch.Stop();
            Console.WriteLine("Task was completed in " + stopWatch.Elapsed.TotalMilliseconds + " milliseconds");


        }

    }


}

