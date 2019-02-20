namespace Tests
{
    using FluentAssertions;
    using LINQ;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class StatisticsTests
    {
        private SearchEngine engine;
        private DbContext dbContext;

        [OneTimeSetUp]
        public void SetUp()
        {
            dbContext = new DbContext();
            engine = new SearchEngine(dbContext);
        }

        [Test]
        public void Test()
        {
            var result = engine.GetPartsStatistic();

            var marks = dbContext.Marks.Select(o => o.Name).ToArray();
            var models = dbContext.Models.Select(o => o.Name).Distinct().ToArray();

            result.Should().OnlyContain(o => o.Marks.SequenceEqual(marks));
            result.Should().OnlyContain(o => o.Models.SequenceEqual(models));
            result.Should().OnlyContain(o => o.SuitableForAuto == true);
        }
    }
}
