using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class LinqPerformance
    {
        private readonly List<string> data = new();

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 65; i < 90; i++)
            {
                char c = (char)i;
                data.Add(c.ToString());
            }
        }

        [Benchmark]
        public string Single() => data.SingleOrDefault(x => x.Equals("M"));

        [Benchmark]
        public string First() => data.FirstOrDefault(x => x.Equals("M"));
    }
}