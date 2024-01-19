using static System.Console;
using System;

Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping");


static void OuterMethod()
{
    WriteLine("Outer method started...");
    Task innerTask = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
    WriteLine("Outer method finished...");
}

static void InnerMethod()
{
    WriteLine("Inner method started...");
    Thread.Sleep(6000);
    WriteLine("Inner method finished...");
}