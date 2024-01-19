using System.Diagnostics;
using static System.Diagnostics.Process;
using static System.Console;
using System;

namespace MonitoringLib
{
    public static class Recorder
    {
        private static Stopwatch timer = new Stopwatch();

        private static long bytePhysicalBefore = 0;
        private static long byteVirtualBefore = 0;

        public static void Start()
        {
            //Clear memory by C# cleaner
            //Clear memory for not using data
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            //Save current using size memory
            bytePhysicalBefore = GetCurrentProcess().WorkingSet64;
            byteVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }

        public static void Stop()
        {
            timer.Stop();
            long  bytePhysicalAfter = GetCurrentProcess().WorkingSet64;
            long byteVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

            WriteLine("{0:N0} physic bytes used", arg0: bytePhysicalAfter - bytePhysicalBefore);
            WriteLine("{0:N0} virtual bytes used", arg0: byteVirtualAfter - byteVirtualBefore);
            WriteLine("{0} time span elapsed", arg0: timer.Elapsed);
            WriteLine("{0} total millisecond elapsed", arg0: timer.ElapsedMilliseconds);
        }

    }
}
