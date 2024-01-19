using BenchmarkDotNet.Attributes;

namespace WorkWithBenchmark.NET
{
    public class StringBenchmarking
    {
        int[] numbers;

        public StringBenchmarking()
        {
            numbers = Enumerable.Range(start: 1, count: 20).ToArray();
        }

        [Benchmark(Baseline = true)]
        public string StringConcatenationTest()
        {
            string str = string.Empty;
            for (int i = 0; i < numbers.Length; i++)
            {
                str += numbers[i] + ", ";
            }
            return str;
        }

        [Benchmark]
        public string StringBuilderTest()
        {
            System.Text.StringBuilder sb = new();
            for(int i = 0; i < numbers.Length; i++) 
            {
                sb.Append(numbers[i]);
                sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}
