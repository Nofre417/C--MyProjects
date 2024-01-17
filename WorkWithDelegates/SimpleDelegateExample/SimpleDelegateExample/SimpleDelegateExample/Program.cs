using static System.Console;
using static System.Environment;
using System.IO;
using System;

namespace SimpleDelegateExample
{
    class Program
    {
        delegate void LogDel(string text);

        static void Main(string[] args)
        {
            Clear();

            Log log = new Log();

            LogDel WriteToFile, PrintTextToConsole;
            PrintTextToConsole = new(log.PrintTextToConsole);
            WriteToFile = new(log.WriteToFile);

            LogDel multiDel = WriteToFile + PrintTextToConsole;

            Write("Enter text: ");
            string input = ReadLine();

            UseMultiDelegate(multiDel, input);
        }

        static void UseMultiDelegate(LogDel logDel, string text)
        {
            logDel(text);
        }

    }
    class Log
    {
        public void PrintTextToConsole(string text)
        {
            WriteLine($"Date: {DateTime.Now}\nLog: {text}");
        }

        public void WriteToFile(string text) 
        {
            string fileName = Path.Combine(CurrentDirectory, "Log.txt");

            using(StreamWriter sw = new StreamWriter(fileName, true)) 
            {
                sw.WriteLine($"Log: {text}\nDate: {DateTime.Now}");
            }
        }

    }
}
