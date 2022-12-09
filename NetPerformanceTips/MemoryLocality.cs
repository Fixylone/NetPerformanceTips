using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class MemoryLocality
    {
        private int[][] jaggedArrays;

        [Params(100, 1000, 10_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            jaggedArrays = new int[Size][];

            for (int i = 0; i < Size; i++)
            {
                jaggedArrays[i] = new int[Size];
            }
        }

        [Benchmark]
        public void TestWithMemoryLocality()
        {
            var result = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (jaggedArrays[i][j] >= 0)
                    {
                        result++;
                    }
                }
            }
        }

        [Benchmark]
        public void TestWithoutMemoryLocality()
        {
            var result = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (jaggedArrays[j][i] >= 0)
                    {
                        result++;
                    }
                }
            }
        }
    }
}