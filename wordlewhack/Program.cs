// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using wordlewhack;

Stopwatch sw = Stopwatch.StartNew();

Whack w = new Whack();

w.LoadFile("words_alpha.txt");

int[] five = w.FindFive();

sw.Stop();

Console.WriteLine("Finished in {0}:{1}.{2}", sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

foreach (int i in five)
{
    Console.WriteLine(w.ConvertIntToWord(i));
}

Console.ReadLine();