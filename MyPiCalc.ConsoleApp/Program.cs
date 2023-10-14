using System;
using System;
using System.Threading;
namespace MyPiCalc.ConsoleApp
{
    class Program
    {
        static string[] text = {
@"      ___                       ___           ___           ___       ___     ",
@"     /\  \          ___        /\  \         /\  \         /\__\     /\  \    ",
@"    /::\  \        /\  \      /::\  \       /::\  \       /:/  /    /::\  \   ",
@"   /:/\:\  \       \:\  \    /:/\:\  \     /:/\:\  \     /:/  /    /:/\:\  \  ",
@"  /::\~\:\  \      /::\__\  /:/  \:\  \   /::\~\:\  \   /:/  /    /:/  \:\  \ ",
@" /:/\:\ \:\__\  __/:/\/__/ /:/__/ \:\__\ /:/\:\ \:\__\ /:/__/    /:/__/ \:\__\",
@" \/__\:\/:/  / /\/:/  /    \:\  \  \/__/ \/__\:\/:/  / \:\  \    \:\  \  \/__/",
@"      \::/  /  \::/__/      \:\  \            \::/  /   \:\  \    \:\  \      ",
@"       \/__/    \:\__\       \:\  \           /:/  /     \:\  \    \:\  \     ",
@"                 \/__/        \:\__\         /:/  /       \:\__\    \:\__\    ",
@"                               \/__/         \/__/         \/__/     \/__/    " };
        static double Pi = 0;
        static double n = 1;
        static long i = 0L;

        static void Main(string[] args)
        {
            foreach (string s in text)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            //while (i < long.MaxValue)
            while (i < 100)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                Pi += (4.0 / n);
                n += 2.0;
                Pi -= (4.0 / n);
                n += 2.0;
                Console.WriteLine("Generated value of Pi: " + Pi);
                i++;
                Thread.Sleep(50);
            }

        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


    }
}