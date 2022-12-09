using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class StringConcatenationWithArray
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random random = new();
        private string[] ArrayOfStrings;

        [Params(10, 100, 1_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            ArrayOfStrings = new string[Size];

            for (int i = 0; i < Size; i++)
            {
                ArrayOfStrings[i] = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        [Benchmark]
        public void ConcatenationInLoop()
        {
            string testString = string.Empty;

            for (int i = 0; i < Size; i++)
            {
                testString += ArrayOfStrings[i];
            }
        }

        [Benchmark]
        public void FormatWithoutInterpolation()
        {
            string testString = string.Empty;

            for (int i = 0; i < Size; i++)
            {
                testString = string.Format("{0}{1}", testString, ArrayOfStrings[i]);
            }
        }

        [Benchmark]
        public void FormatWithInterpolation()
        {
            string testString = string.Empty;

            for (int i = 0; i < Size; i++)
            {
                testString = string.Format($"{testString}{ArrayOfStrings[i]}");
            }
        }

        [Benchmark]
        public void ConcatInLoop()
        {
            string testString = string.Empty;

            for (int i = 0; i < Size; i++)
            {
                testString = string.Concat(testString, ArrayOfStrings[i]);
            }
        }

        [Benchmark]
        public void ConcatWithStringArray()
        {
            _ = string.Concat(ArrayOfStrings);
        }

        [Benchmark]
        public void JoinInLoop()
        {
            string testString = string.Empty;

            for (int i = 0; i < Size; i++)
            {
                testString += string.Join(string.Empty, ArrayOfStrings[i]);
            }
        }

        [Benchmark]
        public void JoinWithStringArray()
        {
            _ = string.Join(string.Empty, ArrayOfStrings);
        }

        [Benchmark]
        public void StringBuilderAppendInLoop()
        {
            var testString = string.Empty;
            var builder = new StringBuilder(testString);

            for (int i = 0; i < Size; i++)
            {
                builder.Append(ArrayOfStrings[i]);
            }

            _ = builder.ToString();
        }
    }
}