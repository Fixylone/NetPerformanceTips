using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class StringParsing
    {
        private string _longString;

        [GlobalSetup]
        public void Setup()
        {
            _longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis " +
                "aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident," +
                " sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }

        [Benchmark]
        public void Substring()
        {
            _ = _longString.Substring(0, 5);
            _ = _longString.Substring(5, 5);
            _ = _longString.Substring(10, 5);
            _ = _longString.Substring(15, 10);
            _ = _longString.Substring(25, 10);
            _ = _longString.Substring(35, 15);
            _ = _longString.Substring(50, 5);
            _ = _longString.Substring(70, 3);
            _ = _longString.Substring(80, 20);
            _ = _longString.Substring(100, 4);
            _ = _longString.Substring(130, 22);
            _ = _longString.Substring(140, 17);
        }

        [Benchmark]
        public void SpanSlice()
        {
            var span = _longString.AsSpan();

            _ = span.Slice(0, 5);
            _ = span.Slice(5, 5);
            _ = span.Slice(10, 5);
            _ = span.Slice(15, 10);
            _ = span.Slice(25, 10);
            _ = span.Slice(35, 15);
            _ = span.Slice(50, 5);
            _ = span.Slice(70, 3);
            _ = span.Slice(80, 20);
            _ = span.Slice(100, 4);
            _ = span.Slice(130, 22);
            _ = span.Slice(140, 17);
        }
    }
}