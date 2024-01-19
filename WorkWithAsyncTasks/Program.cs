using static System.Console;
using System;
using System.Diagnostics;

OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();

/*
WriteLine("Running method synchronously on one thread");
MethodA();
MethodB();
MethodC();
*/

/*
Task taskA = new(MethodA);
Task taskB = Task.Factory.StartNew(MethodB);
Task taskC = Task.Run(MethodC);

Task[] tasks = { taskA, taskB, taskC};
Task.WaitAll(tasks);
*/

Task<string> taskServiceThenSProc = Task.Factory
    .StartNew(CallToWebService)
    .ContinueWith(previousTask =>
    CallTOStorebService(previousTask.Result));

WriteLine($"RESULT: {taskServiceThenSProc.Result}");



WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed");

static void OutputThreadInfo()
{
    Thread thread = Thread.CurrentThread;

    WriteLine("Thread id: {0}, Priority {1}, Background {2}, Name {3}", thread.ManagedThreadId, thread.Priority, thread.IsBackground, thread.Name ?? null);
}
/*
static void MethodA()
{
    WriteLine("Starting Method A...");
    OutputThreadInfo();
    Thread.Sleep(3000);
}
static void MethodB()
{
    WriteLine("Starting Method B...");
    OutputThreadInfo();
    Thread.Sleep(2000);
}
static void MethodC()
{
    WriteLine("Starting Method C...");
    OutputThreadInfo();
    Thread.Sleep(1000);
}
*/

static decimal CallToWebService()
{
    WriteLine("Start call web service...");
    OutputThreadInfo();
    Thread.Sleep(new Random().Next(2000, 40000));
    WriteLine("Finish call web service");
    return 79.50M;
}
static string CallTOStorebService(decimal amount)
{
    WriteLine("Start call store service...");
    OutputThreadInfo();
    Thread.Sleep(new Random().Next(2000, 40000));
    WriteLine("Finish store web service");
    return $"12 products cost more then {amount:C}";
}