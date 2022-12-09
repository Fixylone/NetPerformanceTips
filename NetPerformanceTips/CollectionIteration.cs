using System.Collections;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class CollectionIteration
    {
        private int[] _arrayElements;
        private List<int> _listElements;
        private HashSet<int> _hashSetElements;
        private IEnumerable<int> _enumerableElements;
        private Dictionary<int, int> _dictionaryElements;
        private SortedList<int, int> _sortedListElements;
        private Queue<int> _queueElements;
        private Stack<int> _stackElements;
        private ArrayList _arrayListElements;

        [Params(100, 1000, 100_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _arrayElements = Enumerable.Range(1, Size).ToArray();
            _listElements = Enumerable.Range(1, Size).ToList();
            _hashSetElements = Enumerable.Range(1, Size).ToHashSet();
            _enumerableElements = Enumerable.Range(1, Size);
            _dictionaryElements = Enumerable.Range(1, Size).ToDictionary(x => x);
            _sortedListElements = new SortedList<int, int>(Enumerable.Range(1, Size).ToDictionary(x => x));
            _queueElements = new Queue<int>(Enumerable.Range(1, Size));
            _stackElements = new Stack<int>(Enumerable.Range(1, Size));
            _arrayListElements = new ArrayList(Enumerable.Range(1, Size).ToArray());
        }

        [Benchmark]
        public void For_Array()
        {
            for (int i = 0; i < _arrayElements.Length; i++)
            {
                _ = _arrayElements[i];
            }
        }

        [Benchmark]
        public void ForEach_Array()
        {
            foreach (var x in _arrayElements)
            {
            }
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
        public void ForEach_HashSet()
        {
            foreach (var x in _hashSetElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_Enumerable()
        {
            foreach (var x in _enumerableElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_Dictionary()
        {
            foreach (var x in _dictionaryElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_SortedList()
        {
            foreach (var x in _sortedListElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_Queue()
        {
            foreach (var x in _queueElements)
            {
            }
        }

        [Benchmark]
        public void ForEach_Stack()
        {
            foreach (var x in _stackElements)
            {
            }
        }

        [Benchmark]
        public void For_ArrayList()
        {
            for (int i = 0; i < _arrayListElements.Count; i++)
            {
                _ = _arrayListElements[i];
            }
        }

        [Benchmark]
        public void ForEach_ArrayList()
        {
            foreach (var x in _arrayListElements)
            {
            }
        }
    }
}