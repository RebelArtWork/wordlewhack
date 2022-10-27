// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using wordlewhack;

Stopwatch sw = Stopwatch.StartNew();

Whack w = new Whack();

w.LoadFile("words_alpha.txt");

//Console.WriteLine("Total number of words = {0}", w.WordIntArray.Count());
//Console.WriteLine("Finished in {0}:{1}.{2}", sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

int[] five = w.FindFive();

sw.Stop();

Console.WriteLine("Finished in {0}:{1}.{2}", sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

foreach (int i in five)
{
    Console.WriteLine(w.ConvertIntToWord(i));
}

Console.WriteLine("FIN");
Console.ReadLine();