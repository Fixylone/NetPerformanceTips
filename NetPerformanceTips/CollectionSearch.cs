using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class CollectionSearch
    {
        private int[] _arrayElements;
        private List<int> _listElements;
        private HashSet<int> _hashSetElements;
        private IEnumerable<int> _enumerableElements;
        private Dictionary<int, int> _dictionaryElements;

        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _arrayElements = Enumerable.Range(1, Size).ToArray();
            _listElements = Enumerable.Range(1, Size).ToList();
            _hashSetElements = Enumerable.Range(1, Size).ToHashSet();
            _enumerableElements = Enumerable.Range(1, Size);
            _dictionaryElements = Enumerable.Range(1, Size).ToDictionary(x => x);
        }

        [Benchmark]
        public void Search_Array()
        {
            _arrayElements.Contains(-1);
        }

        [Benchmark]
        public void Search_List()
        {
            _listElements.Contains(-1);
        }

        [Benchmark]
        public void Search_HashSet()
        {
            _hashSetElements.Contains(-1);
        }

        [Benchmark]
        public void Search_Dictionary()
        {
            _dictionaryElements.ContainsKey(-1);
        }

        [Benchmark]
        public void Search_IEnumerable()
        {
            _enumerableElements.Contains(-1);
        }
    }
}