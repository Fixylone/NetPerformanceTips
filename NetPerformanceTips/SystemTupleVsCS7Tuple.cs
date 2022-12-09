using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class SystemTupleVsCS7Tuple
    {
        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        [Benchmark]
        public void CS7Tuple()
        {
            for (int i = 0; i < Size; i++)
            {
                _ = GetCS7Tuple();
            }
        }

        [Benchmark]
        public void SystemTuple()
        {
            for (int i = 0; i < Size; i++)
            {
                _ = GetSystemTuple();
            }
        }

        public static Tuple<int, int> GetSystemTuple()
        {
            return Tuple.Create<int, int>(7, 22);
        }

        public static (int min, int max) GetCS7Tuple()
        {
            return (7, 22);
        }
    }
}