using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordlewhack
{
    internal class Whack
    {

        public int[] WordIntArray;
        private Dictionary<int, string> WordArray = new Dictionary<int, string>();
        private int[] FinalWords;
        private bool Found;

        public void LoadFile(string f)
        {

            List<int> ints = new List<int>();

            foreach(var w in File.ReadLines(f))
            {
                if(w.Length == 5 && w.Distinct().Count() == w.Count())
                {
                    int i = ConvertWordToInt(w);
                    if (!ints.Contains(i))
                    {
                        WordArray.Add(i, w);
                        ints.Add(i);
                    }
                }
            }

            WordIntArray = ints.ToArray();
        }

        private int ConvertWordToInt(string w)
        {
            int output = 0;
            foreach(var c in w)
            {
                output += 1 << ((int)c - 97);
            }
            return output;
        }

        public string ConvertIntToWord(int i)
        {
            return WordArray[i];
        }

        public int[] FindFive(int startIndex = 0)
        {
            Parallel.ForEach(WordIntArray, x => getMatches(new int[] { x }, WordIntArray));
            return FinalWords;
        }

        private int[] getMatches(int[] currentList, int[] potentialList)
        {
            potentialList = potentialList.Where(x => currentList.Count(y => (x & y) > 0) == 0).ToArray();

            if (currentList.Count() + potentialList.Count() < 5)
            {
                return currentList.ToArray();
            }

            foreach (var i in potentialList)
            {
                if (currentList.Length == 4)
                {
                    FinalWords = currentList.Concat(new int[] { i }).ToArray();
                    Found = true;
                    return FinalWords;
                }

                getMatches(currentList.Append(i).ToArray() , potentialList);

                if (Found)
                {
                    return FinalWords;
                }
            }

            return currentList.ToArray();
        }

    }
}
