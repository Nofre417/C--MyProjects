using static System.Console;
using System.Diagnostics;
using System;

WriteLine("Please wait till task complete");
Stopwatch timer = Stopwatch.StartNew();

Task taskA = Task.Factory.StartNew(MethodA);
Task taskB = Task.Factory.StartNew(MethodB);
Task.WaitAll(new Task[] { taskA, taskB});

WriteLine($"\nResult: {SharedObject.Message}");
WriteLine($"{SharedObject.Count} string modifications");
WriteLine($"{timer.ElapsedMilliseconds}ms elapsed milliseconds");

static void MethodA()
{
    try
    {
        if (Monitor.TryEnter(SharedObject.Flag, TimeSpan.FromMilliseconds(15)))
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(SharedObject.Random.Next(2000));
                SharedObject.Message += "A";
                Interlocked.Increment(ref SharedObject.Count);
                Write(".");
            }
        }
        else
        {
            WriteLine("Method A timed out when entering a monitor on flag");
        }
    }
    finally
    {
        Monitor.Exit(SharedObject.Flag);
    }
}
    static void MethodB() 
{ 
 

        try
        {
            if(Monitor.TryEnter(SharedObject.Flag, TimeSpan.FromMilliseconds(15)))
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(SharedObject.Random.Next(2000));
                    SharedObject.Message += "B";
                    Interlocked.Increment(ref SharedObject.Count);
                    Write(".");
                
                }
            }
            else
            {
                WriteLine("Method B timed out when entering a monitor on flag");
            }
        }
        finally
        {
            Monitor.Exit(SharedObject.Flag);
        }
}


static class SharedObject
{
    public static Random Random = new Random();
    public static string? Message;
    public static object Flag = new();
    public static int Count;
}