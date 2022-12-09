using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class ArrayAccessSpeedOptimization
    {
        private int[] _arrayElements;

        [Params(100, 1000, 100_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _arrayElements = new int[Size];
        }

        [Benchmark]
        public int FieldAccess()
        {
            int last = 0;
            for (int i = 0; i < _arrayElements.Length; i++)
            {
                last = _arrayElements[i];
            }

            return last;
        }

        [Benchmark]
        public int LocalAccess()
        {
            int last = 0;
            int[] array = _arrayElements;

            for (int i = 0; i < array.Length; i++)
            {
                last = array[i];
            }

            return last;
        }
    }
}