namespace Tests
{
    using FluentAssertions;
    using LINQ;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class SearchModelTests
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
        [TestCase("Toyota")]
        [TestCase("BMW")]
        public void SearchModelTest(string markName)
        {
            var result = engine.SearchModels(new SearchModelsQuery { MarkName = markName });

            var mark = dbContext.Marks.FirstOrDefault(o => o.Name == markName);
            var models = dbContext.Models.Where(o => o.MarkId == mark.Id).ToArray();

            result.Select(o => o.ModelName).Should().BeEquivalentTo(models.Select(o => o.Name).Distinct());
            result.Should().OnlyContain(o => o.MarkName == mark.Name);

            result.Select(o => o.Years.Min()).Should().BeInAscendingOrder();

            var model = models[0];

            var selectedModels = models.Where(o => o.Name == model.Name).ToArray();

            var resultModel = result.FirstOrDefault(o => o.ModelName == model.Name);

            resultModel.Years.Should().BeEquivalentTo(selectedModels.Select(o => o.Year).Distinct());

            resultModel.TransmissionTypes.Should().BeEquivalentTo(selectedModels.Select(o => o.TransmissionType).Distinct());
        }
    }
}
