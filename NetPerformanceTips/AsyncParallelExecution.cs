using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class AsyncParallelExecution
    {
        [Benchmark]
        public async Task SynchronousExecution()
        {
            var first = await GetValue();
            var second = await GetValue();
            var third = await GetValue();
            var fourth = await GetValue();
            var fifth = await GetValue();
            _ = first + second + third + fourth + fifth;
        }

        [Benchmark]
        public async Task ParallelExecution()
        {
            var first = GetValue();
            var second = GetValue();
            var third = GetValue();
            var fourth = GetValue();
            var fifth = GetValue();

            await Task.WhenAll(first, second, third, fourth, fifth);
            _ = await first + await second + await third + await fourth + await fifth;
        }

        private static async Task<int> GetValue()
        {
            await Task.Delay(50);
            return 1;
        }
    }
}