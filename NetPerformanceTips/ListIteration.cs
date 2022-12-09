using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class ListIteration
    {
        private List<int> _listElements;

        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _listElements = Enumerable.Range(1, Size).ToList();
        }

        [Benchmark]
        public void For_List()
        {
            for (int i = 0; i < _listElements.Count; i++)
            {
                _ = _listElements[i];
            }
        }

        [Benchmark]
        public void ForEach_List()
        {
            foreach (var x in _listElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_Linq()
        {
            _listElements.ForEach(x => { });
        }

        [Benchmark]
        public void Parallel_Foreach()
        {
            Parallel.ForEach(_listElements, x => { });
        }

        [Benchmark]
        public void Parallel_Linq()
        {
            _listElements.AsParallel().ForAll(x => { });
        }

        [Benchmark]
        public void ForEach_Span()
        {
            foreach (var x in CollectionsMarshal.AsSpan(_listElements))
            {
            }
        }

        [Benchmark]
        public void For_Span()
        {
            var listAsSpan = CollectionsMarshal.AsSpan(_listElements);
            for (int i = 0; i < listAsSpan.Length; i++)
            {
                _ = listAsSpan[i];
            }
        }
    }
}