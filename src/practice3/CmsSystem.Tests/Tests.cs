using System;
using System.Collections.Generic;
using System.IO;
using CmsSystem.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CmsSystem.Tests
{
    public class Tests
    {
        private static readonly Random Rnd = new Random(1123);
        private Cms cms;

        [OneTimeSetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Tests).Assembly.Location);
            this.cms = new Cms();

            // после того как первый раз заполните cms данными, эту строчку нужно закоментить
            FillTestData(6, 11);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.cms.Dispose();
        }

        [TestCaseSource(nameof(ValidCases))]
        [TestCaseSource(nameof(EdgeCases))]
        [TestCaseSource(nameof(LongRunningTests))]
        public void RevenueTests(DateTime from, DateTime to, decimal result)
        {
            var dataAnalyzer = cms.GetDataAnalyzer();
            var revenue = dataAnalyzer.GetRevenue(from, to);
            Assert.AreEqual(result, revenue);
        }

        [TestCaseSource(nameof(TopSalingArticlesForTwoDays))]
        public void TopSailingAtriclesTests(Cms.DataAnalyzer.Aggregation aggregation, DateTime startDate, DateTime endDate, Cms.DataAnalyzer.TopSellingArticlesResult[] result)
        {
            var dataAnalyzer = cms.GetDataAnalyzer();
            var topSellingArticles = dataAnalyzer.GetTopSellingArticles(aggregation, startDate, endDate);
            Assert.AreEqual(result.Length, topSellingArticles.Length);
            topSellingArticles.Should().BeEquivalentTo(result);
        }

        [TestCaseSource(nameof(TopSalingArticlesWholeYear))]
        public void TopSailingAtriclesTests(Cms.DataAnalyzer.Aggregation aggregation, DateTime startDate, DateTime endDate, int resultCount)
        {
            var dataAnalyzer = cms.GetDataAnalyzer();
            var topSellingArticles = dataAnalyzer.GetTopSellingArticles(aggregation, startDate, endDate);
            Assert.AreEqual(resultCount, topSellingArticles.Length);
        }

        [Test]
        public void IndexUpdatingTest()
        {
            var now = DateTime.Now;
            var dataAnalyzer = cms.GetDataAnalyzer();
            var result = dataAnalyzer.GetTopSellingArticles(Cms.DataAnalyzer.Aggregation.Year, DateTime.MinValue, DateTime.MaxValue);

            var rnd = new Random();
            var article = new Catalog("catalog.csv")[rnd.Next(0, 5000)].Article;
            cms.Add(new SaleEvent() { DateTime = now, Article = article, Count = 100501, StoreName = "M24" });
            var newResult = dataAnalyzer.GetTopSellingArticles(Cms.DataAnalyzer.Aggregation.Year, DateTime.MinValue, DateTime.MaxValue);
            cms.Remove(now, article);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1, newResult.Length);
            Assert.AreNotEqual(result[0].Top5SellingArticles[0], article);
            Assert.AreEqual(newResult[0].Top5SellingArticles[0], article);
        }

        private static IEnumerable<TestCaseData> ValidCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2018, 6, 12, 8, 00, 0), new DateTime(2018, 6, 12, 8, 05, 0), decimal.Parse("47177803")).SetName("June valid test");
                yield return new TestCaseData(new DateTime(2018, 10, 11, 15, 00, 0), new DateTime(2018, 10, 11, 15, 05, 0), decimal.Parse("54856407")).SetName("October valid test");
                yield return new TestCaseData(new DateTime(2018, 8, 25, 17, 00, 0), new DateTime(2018, 8, 25, 17, 05, 0), decimal.Parse("41827870")).SetName("August valid test");
                yield return new TestCaseData(new DateTime(2018, 6, 12, 8, 0, 0), new DateTime(2018, 6, 12, 8, 0, 0), decimal.Parse("44970")).SetName("Equal dates test");

            }
        }

        private static IEnumerable<TestCaseData> EdgeCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2018, 10, 11, 21, 0, 0), new DateTime(2018, 10, 11, 22, 0, 0), decimal.Parse("-1")).SetName("Time without events test");
                yield return new TestCaseData(new DateTime(2019, 10, 11, 15, 0, 0), new DateTime(2019, 10, 11, 15, 05, 0), decimal.Parse("-1")).SetName("Missing future dates test");
                yield return new TestCaseData(new DateTime(2017, 5, 12, 8, 0, 0), new DateTime(2017, 5, 12, 8, 05, 0), decimal.Parse("-1")).SetName("Missing past events test");
                yield return new TestCaseData(new DateTime(2018, 5, 12, 8, 05, 0), new DateTime(2018, 5, 12, 8, 0, 0), decimal.Parse("-1")).SetName("Wrong dates order test");
            }
        }

        /// <summary>
        /// Сделать, чтобы эти тесты проходили быстро - не простая задача, по умолчанию мы от вас ее не требуем. 
        /// Но если вы успешно справились с остальными тестами, подумайте почему эти тесты проходят так долго, даже при условии хорошей асимптотики алгоримма.
        /// </summary>
        private static IEnumerable<TestCaseData> LongRunningTests
        {
            get
            {
                yield return new TestCaseData(new DateTime(2018, 6, 12, 0, 0, 0), new DateTime(2018, 6, 13, 0, 0, 0), decimal.Parse("6921146623")).SetName("Full day long test");
                yield return new TestCaseData(new DateTime(2018, 10, 1, 0, 0, 0), new DateTime(2018, 10, 07, 0, 0, 0), decimal.Parse("41334527957")).SetName("Full week long test");
            }
        }

        private static IEnumerable<TestCaseData> TopSalingArticlesWholeYear
        {
            get
            {
                yield return new TestCaseData(Cms.DataAnalyzer.Aggregation.Day, DateTime.MinValue, DateTime.MaxValue, 137).SetName("Full per day statistics");
                yield return new TestCaseData(Cms.DataAnalyzer.Aggregation.Month, DateTime.MinValue, DateTime.MaxValue, 5).SetName("Full per month statistics");
				// если ваш код работает быстро - раскоментите TestCase ниже
                //     yield return new TestCaseData(Cms.DataAnalyzer.Aggregation.Year, DateTime.MinValue, DateTime.MaxValue, 1).SetName("Full per year statistics");
            }
        }

        private static IEnumerable<TestCaseData> TopSalingArticlesForTwoDays
        {
            get
            {
                yield return new TestCaseData(
                    Cms.DataAnalyzer.Aggregation.Day,
                    new DateTime(2018, 10, 10),
                    new DateTime(2018, 10, 11),
                    new[]
                    {
                        new Cms.DataAnalyzer.TopSellingArticlesResult()
                        {
                            Date = new DateTime(2018, 10, 10),
                            Top5SellingArticles = new[]
                            {
                               "1290323",
                               "1208675",
                               "1158864",
                               "1225843",
                               "1226911"
                            }
                        },
                        new Cms.DataAnalyzer.TopSellingArticlesResult()
                        {
                            Date = new DateTime(2018, 10, 11),
                            Top5SellingArticles = new[]
                            {
                                "1256938",
                                "1152930",
                                "1015777",
                                "1243488",
                                "1236925"
                            }
                        },
                    }).SetName("Per day statistics");
                yield return new TestCaseData(
                    Cms.DataAnalyzer.Aggregation.Month,
                    new DateTime(2018, 10, 10),
                    new DateTime(2018, 10, 11),
                    new[]
                    {
                        new Cms.DataAnalyzer.TopSellingArticlesResult()
                        {
                            Date = new DateTime(2018, 10, 10),
                            Top5SellingArticles = new[]
                            {
                                "1256938",
                                "1152930",
                                "1015777",
                                "1049923",
                                "1226911"
                            }
                        }
                    }).SetName("Per month statistics");
                yield return new TestCaseData(
                    Cms.DataAnalyzer.Aggregation.Year,
                    new DateTime(2018, 10, 10),
                    new DateTime(2018, 10, 11),
                    new[]
                    {
                        new Cms.DataAnalyzer.TopSellingArticlesResult()
                        {
                            Date = new DateTime(2018, 10, 10),
                            Top5SellingArticles = new[]
                            {
                                "1256938",
                                "1152930",
                                "1015777",
                                "1049923",
                                "1226911"
                            }
                        }
                    }).SetName("Per year statistics");
            }
        }

        private void FillTestData(int startMonth, int endMonth)
        {
            var catalog = new Catalog("catalog.csv");
            var stores = new[] { "M1\0", "M2\0", "M3\0", "M4\0", "M5\0", "M6\0", "M6\0", "M7\0", "M8\0", "M9\0", "M10", "M11", "M12", "M13", "M14", "M15", "M16", "M17", "M18", "M19", "M20", "M21", "M22", "M23", "M24", "M25", "M26", "M27" };

            for (var i = startMonth; i < endMonth; i++)
            {
                for (var j = 1; j < 30; j++)
                {
                    if ((i == 2 && j == 29) || (i == 10 && j >= 22))
                    {
                        continue;
                    }

                    for (var k = 8; k < 20; k++) //часы
                    {
                        var list = new List<SaleEvent>();

                        for (var l = 0; l < 60; l++) // минуты
                        {
                            for (var m = 0; m < 60; m++) // секунды
                            {
                                // если хотите получить многогигабайтный файл, то просто воткните еще один вложенный цикл от 0 до 100
                                var date = new DateTime(2018, i, j, k, l, m);
                                var article = catalog[Rnd.Next(0, catalog.Count - 1)].Article;
                                var store = stores[Rnd.Next(0, 26)];
                                var count = Rnd.Next(1, 100);
                                list.Add(
                                    new SaleEvent()
                                    {
                                        DateTime = date,
                                        Article = article,
                                        StoreName = store,
                                        Count = count
                                    });
                            }
                        }

                        cms.AddRange(list);
                        list.Clear();
                    }
                }
            }
        }
    }
}