using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Order;

namespace NetPerformanceTips
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Default)]
    [HideColumns(Column.Error, Column.StdDev, Column.Median, Column.Gen0, Column.Gen1)]
    [RankColumn]
    public class StringConcatenationWithoutArray
    {
        [Benchmark]
        public void ConcatenationInOneLine()
        {
            string testString = "random string" + "random string" + "random string" + "random string" + "random string" + "random string"
                + "random string" + "random string" + "random string" + "random string";
        }

        [Benchmark]
        public void ConcatenationInMultipleLines()
        {
            string testString = "";

            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
            testString += "random string";
        }

        [Benchmark]
        public void StringBuilder()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");
            stringBuilder.Append("random string");

            stringBuilder.ToString();
        }
    }
}