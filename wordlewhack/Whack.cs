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
        private List<int> DuffList = new List<int>();
        private Dictionary<int, double> rankingScheme = new Dictionary<int, double>();

        public void LoadFile(string f)
        {

            //init ranking

            rankingScheme.Add(1, 8.4966);//A
            rankingScheme.Add(2, 2.0720);//B
            rankingScheme.Add(3, 4.5388);//C
            rankingScheme.Add(4, 3.3844);//D
            rankingScheme.Add(5, 11.1607);//E
            rankingScheme.Add(6, 1.8121);//F
            rankingScheme.Add(7, 2.4705);//G
            rankingScheme.Add(8, 3.0034);//H
            rankingScheme.Add(9, 7.5448);//I
            rankingScheme.Add(10, 0.1965);//J
            rankingScheme.Add(11, 1.1016);//K
            rankingScheme.Add(12, 5.4893);//L
            rankingScheme.Add(13, 3.0129);//M
            rankingScheme.Add(14, 6.6544);//N
            rankingScheme.Add(15, 7.1635);//O
            rankingScheme.Add(16, 7.1635);//P
            rankingScheme.Add(17, 0.1962);//Q
            rankingScheme.Add(18, 7.5809);//R
            rankingScheme.Add(19, 5.7351);//S
            rankingScheme.Add(20, 6.9509);//T
            rankingScheme.Add(21, 3.6308);//U
            rankingScheme.Add(22, 1.0074);//V
            rankingScheme.Add(23, 1.2899);//W
            rankingScheme.Add(24, 0.2902);//X
            rankingScheme.Add(25, 1.7779);//Y
            rankingScheme.Add(26, 0.2722);//Z


            //List<string> words = new List<string>();
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

            WordIntArray = rankWords(ints.ToArray());
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

        

        private int[] rankWords(int[] words)
        {
            Dictionary<int, double> ranks = new Dictionary<int, double>();
            foreach(var w in words)
            {
                double score = 0;
                for(int i = 1; i <= 6; i++)
                {
                    if ((1 << i - 1 & w) > 0)
                    {
                        score += rankingScheme[i];
                    }
                }
                ranks[w] = score;
            }

            return ranks.OrderBy(x => x.Value).Select(x=> x.Key).ToArray();
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

                //getMatches(currentList.Concat(new int[] { i }).ToArray(), potentialList);
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
