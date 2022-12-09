using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class ValueTypeVsReferenceType
    {
        private List<PointClass> _pointClasses;
        private List<PointStruct> _pointStructs;
        private List<PointStructEquatable> _pointEquatableStructs;

        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _pointClasses = new List<PointClass>(Size);
            _pointStructs = new List<PointStruct>(Size);
            _pointEquatableStructs = new List<PointStructEquatable>(Size);

            var random = new Random();

            for (int i = 0; i < Size; i++)
            {
                _pointClasses.Add(new PointClass() { X = random.Next(), Y = random.Next() });
                _pointStructs.Add(new PointStruct() { x = random.Next(), y = random.Next() });
                _pointEquatableStructs.Add(new PointStructEquatable() { x = random.Next(), y = random.Next() });
            }
        }

        [Benchmark]
        public void ManyClassesCreation()
        {
            var random = new Random();
            var list = new List<PointClass>();

            for (int i = 0; i < Size; i++)
            {
                list.Add(new PointClass() { X = random.Next(), Y = random.Next() });
            }
        }

        [Benchmark]
        public void ManyStructsCeation()
        {
            var random = new Random();
            var list = new List<PointStruct>();

            for (int i = 0; i < Size; i++)
            {
                list.Add(new PointStruct() { x = random.Next(), y = random.Next() });
            }
        }

        [Benchmark]
        public void ManyEquatableStructsCreation()
        {
            var random = new Random();
            var list = new List<PointStructEquatable>();

            for (int i = 0; i < Size; i++)
            {
                list.Add(new PointStructEquatable() { x = random.Next(), y = random.Next() });
            }
        }

        [Benchmark]
        public void ManyClassesSearch()
        {
            _pointClasses.Contains(new PointClass { X = -1, Y = -1 });
        }

        [Benchmark]
        public void ManyStructsSearch()
        {
            _pointStructs.Contains(new PointStruct { x = -1, y = -1 });
        }

        [Benchmark]
        public void ManyEquatableStructsSearch()
        {
            _pointEquatableStructs.Contains(new PointStructEquatable { x = -1, y = -1 });
        }
    }

    public class PointClass : IEquatable<PointClass>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PointClass pointClass && Equals(pointClass);
        }

        public bool Equals(PointClass other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(PointClass left, PointClass right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointClass left, PointClass right)
        {
            return !(left == right);
        }
    }

    public struct PointStruct
    {
        public int x;
        public int y;
    }

    public struct PointStructEquatable : IEquatable<PointStructEquatable>
    {
        public int x;
        public int y;

        public override bool Equals(object? obj)
        {
            return obj is PointStructEquatable equatable && Equals(equatable);
        }

        public bool Equals(PointStructEquatable other)
        {
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static bool operator ==(PointStructEquatable left, PointStructEquatable right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointStructEquatable left, PointStructEquatable right)
        {
            return !(left == right);
        }
    }
}