using MonitoringLib;

using static System.Console;
using System;

namespace MonitoringApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            WriteLine("Processing... Please wait");
            Recorder.Start();

            int[] largeArrayOfInts = Enumerable.Range(start: 1, count: 10_000).ToArray();

            Thread.Sleep(new Random().Next(5, 10) * 1_000);

            Recorder.Stop();
            */

            int[] numbers = Enumerable.Range(start: 1, count: 50_000).ToArray();

            WriteLine("Using string with '+'");

            Recorder.Start();
            string str = string.Empty;
            for(int i = 0; i < numbers.Length; i++)
            {
                str += numbers[i] + ", ";
            }
            Recorder.Stop();

            WriteLine("Using string builder");
            Recorder.Start();
            System.Text.StringBuilder sb = new();
            for(int i = 0; i < numbers.Length; i++)
            {
                sb.Append(numbers[i]);
                sb.Append(", ");
            }
            Recorder.Stop();
        }
    }
}
