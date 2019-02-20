using FluentAssertions;
using LINQ;
using NUnit.Framework;

namespace Tests
{
    using System;
    using System.Linq;

    [TestFixture]
    public class SearchPartsTests
    {
        private SearchEngine engine;

        [OneTimeSetUp]
        public void SetUp()
        {
            engine = new SearchEngine(new DbContext());
        }

        [Test]
        [TestCase("BMW")]
        [TestCase("Toyota")]
        [TestCase("bmW")]
        public void FilterByMarkName(string markName)
        {
            var result = engine.SearchParts(new SearchPartsQuery { MarkName = markName });

            result.Should().OnlyContain(o => o.MarkName.Equals(markName, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void NoMarkTest()
        {
            var result = engine.SearchParts(new SearchPartsQuery { MarkName = "!@#$" });

            result.Should().BeEmpty();
        }

        [Test]
        [TestCase("X5")]
        [TestCase("camry")]
        [TestCase("POLO")]
        [TestCase("Ceed")]
        public void FilterByModelTest(string modelName)
        {
            var result = engine.SearchParts(new SearchPartsQuery { ModelName = modelName });

            result.Should().OnlyContain(o => o.ModelName.Equals(modelName, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void NoModelTest()
        {
            var result = engine.SearchParts(new SearchPartsQuery { ModelName = "!@#$" });

            result.Should().BeEmpty();
        }

        [Test]
        [TestCase(1990)]
        [TestCase(1994)]
        [TestCase(2018)]
        public void FilterByYearTest(int year)
        {
            var result = engine.SearchParts(new SearchPartsQuery { Year = year });

            result.Should().OnlyContain(o => o.Year == year);
        }

        [Test]
        public void NoYearTest()
        {
            var result = engine.SearchParts(new SearchPartsQuery { Year = 0 });

            result.Should().BeEmpty();
        }

        [Test]
        [TestCase(TransmissionType.Auto)]
        [TestCase(TransmissionType.Manual)]
        public void FilterTransmissionTest(TransmissionType transmissionType)
        {
            var result = engine.SearchParts(new SearchPartsQuery { TransmissionType = transmissionType });

            result.Should().OnlyContain(o => o.TransmissionType == transmissionType);
        }

        [Test]
        [TestCase("part-code-1")]
        [TestCase("part-code-10")]
        [TestCase("part-code-0")]
        [TestCase("part-code-59")]
        public void FilterByCodeTest(string code)
        {
            var result = engine.SearchParts(new SearchPartsQuery { PartCode = code });

            result.Should().OnlyContain(o => o.PartCode == code);
        }

        [Test]
        public void NoCodeTest()
        {
            var result = engine.SearchParts(new SearchPartsQuery { PartCode = "%$#!" });

            result.Should().BeEmpty();
        }

        [Test]
        [TestCase("cross-code-1")]
        [TestCase("cross-code-2")]
        [TestCase("cross-code-4")]
        public void FilterByCrossCodeTest(string code)
        {
            var result = engine.SearchParts(new SearchPartsQuery { PartCode = code });

            result.Should().OnlyContain(o => o.CrossCodes.Contains(code));
        }

        [Test]
        [TestCase("запчасть#0")]
        [TestCase("запчастЬ#12")]
        [TestCase("ЗАПЧАСТЬ#67")]
        public void FilterByPartNameTest(string name)
        {
            var result = engine.SearchParts(new SearchPartsQuery { PartName = name });

            result.Should().OnlyContain(o => o.PartName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void NoNameTest()
        {
            var result = engine.SearchParts(new SearchPartsQuery { PartName = "%$#@!" });

            result.Should().BeEmpty();
        }

        [Test]
        [TestCase("Kia", "Rio", TransmissionType.Auto, 2017)]
        [TestCase("VW", "Polo", TransmissionType.Manual, 2014)]
        public void FilterByQueryTest(string markName, string modelName, TransmissionType transmissionType, int year)
        {
            var query = new SearchPartsQuery
            {
                TransmissionType = transmissionType,
                MarkName = markName,
                ModelName = modelName,
                Year = year
            };
            var result = engine.SearchParts(query);

            result.Should().OnlyContain(o =>
                o.MarkName == markName && 
                o.ModelName == modelName && 
                o.TransmissionType == transmissionType && 
                o.Year == year);
        }
    }
}
