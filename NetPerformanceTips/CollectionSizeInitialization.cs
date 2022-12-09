using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class CollectionSizeInitialization
    {
        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        [Benchmark]
        public void ListWithoutInitialSize()
        {
            var listOfElements = new List<int>();

            for (int i = 0; i < Size; i++)
            {
                listOfElements.Add(i);
            }
        }

        [Benchmark]
        public void ListWithInitialSize()
        {
            var listOfElements = new List<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                listOfElements.Add(i);
            }
        }

        [Benchmark]
        public void DictionaryWithoutInitialSize()
        {
            var listOfElements = new Dictionary<int, int>();

            for (int i = 0; i < Size; i++)
            {
                listOfElements.Add(i, i);
            }
        }

        [Benchmark]
        public void DictionaryWithInitialSize()
        {
            var listOfElements = new Dictionary<int, int>(Size);

            for (int i = 0; i < Size; i++)
            {
                listOfElements.Add(i, i);
            }
        }
    }
}