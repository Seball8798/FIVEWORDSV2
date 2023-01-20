using System.Diagnostics;

namespace FiveWordsWpfLibary
{

    public static class FiveWordsWpfLibaryClass
    {
        public static string? word;
        public static List<string> AllCombinations = new List<string>();


        private const int EXPECTED_LENGTH = 5;
        public static int count = 0;
        public static int currentProgress = 0;

        public static List<string> LoadWords(string filename)
        {
            var words = new List<string>();
            var file = new StreamReader(filename);
            var seen = new HashSet<int>();

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

        }
        public static void OutputAllCombinations(List<bool[]> can_construct, List<string> words, List<int> masks, List<int> result, int mask, int start_from)
        {
            if (result.Count == 5)
            {
                string combination = "";
                for (int i = 0; i < 5; ++i)
                {
                    combination += words[result[i]] + " ";
                }
                AllCombinations.Add(combination);
                Console.WriteLine();
                count++;
                return;

            }
            for (int cur_word = start_from; cur_word < words.Count; ++cur_word)
            {
                if (((mask & masks[cur_word]) == masks[cur_word]) && (result.Count == 4 || can_construct[3 - result.Count][mask ^ masks[cur_word]]))
                {
                    result.Add(cur_word);
                    OutputAllCombinations(can_construct, words, masks, result, mask ^ masks[cur_word], cur_word + 1);
                    result.RemoveAt(result.Count - 1);
                }
            }
        }

        public static int Solve(List<string> words)
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var can_construct = new List<bool[]>();
            for (int i = 0; i < 5; i++)
            {
                can_construct.Add(new bool[1 << 26]);
            }
            var masks = new List<int>();
            for (int i = 0; i < words.Count; ++i)
            {
                int mask = 0;
                foreach (var c in words[i])
                {
                    mask |= 1 << (c - 'a');
                }
                masks.Add(mask);
                can_construct[0][mask] = true;
            }
            for (int cnt = 0; cnt < 4; ++cnt)
            {
                //And'ing bits 
                for (int mask = 0; mask < (1 << 26); ++mask)
                {
                    if (!can_construct[cnt][mask]) continue;
                    for (int i = 0; i < words.Count; ++i)
                    {

                        if ((masks[i] & mask) == 0)
                        {

                            can_construct[cnt + 1][masks[i] | mask] = true;

                        }
                        currentProgress++;
                    }
                }
                stopwatch.Stop();
            }

            var result = new List<int>();

            for (int mask = 0; mask < (1 << 26); mask++)
            {

                if (can_construct[4][mask])
                {

                    OutputAllCombinations(can_construct, words, masks, result, mask, 0);

                }
            }
            Console.WriteLine("Time taken: " + stopwatch.ElapsedMilliseconds + "ms");
            return count;
            //progressCallback(count / words.Count);
        }
        private static void UpdateProgress(double progress)
        {
            //ProgressBar.Value = progress;
        }
    }
}