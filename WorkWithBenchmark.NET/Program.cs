using WorkWithBenchmark;
using BenchmarkDotNet.Running;

using static System.Console;
using System;
using WorkWithBenchmark.NET;

WriteLine("_BENCHMARKING_");

BenchmarkRunner.Run<StringBenchmarking>();