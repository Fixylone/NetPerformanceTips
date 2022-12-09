using System.Buffers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class ReusingArraysWithArrayPool
    {
        [Params(10, 100)]
        public int Size { get; set; }

        [Benchmark]
        public void ArraysWithoutArrayPool()
        {
            for (int i = 0; i < Size; i++)
            {
                _ = new int[256 * 1024];
            }
        }

        [Benchmark]
        public void ArraysWithArrayPool()
        {
            for (int i = 0; i < Size; i++)
            {
                var array = ArrayPool<int>.Shared.Rent(256 * 1024);
                ArrayPool<int>.Shared.Return(array);
            }
        }
    }
}