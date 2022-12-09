using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class BoxingVsNonBoxing
    {
        private List<object> boxedElements;
        private List<int> nonBoxedElements;

        [Params(10)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            boxedElements = new List<object>(Size);
            nonBoxedElements = new List<int>(Size);
        }

        [Benchmark]
        public void Boxing()
        {
            for (int i = 0; i < Size; i++)
            {
                boxedElements.Add(i);
            }
        }

        [Benchmark]
        public void NonBoxing()
        {
            for (int i = 0; i < Size; i++)
            {
                nonBoxedElements.Add(i);
            }
        }
    }
}