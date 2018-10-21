using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace AnalysisTool.Tests
{
    public class DataAnalyserTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(DataAnalyserTests).Assembly.Location);
        }

        [TestCaseSource(nameof(ValidCases))]
        [TestCaseSource(nameof(EdgeCases))]
        // [TestCaseSource(nameof(LongRunningTests))]
        public void RevenueTests(DateTime from, DateTime to, decimal result)
        {
            var catalog = new Catalog("catalog.csv");
            using (var eventLog = new EventLog("events.csv"))
            {
                var dataAnalyzer = new DataAnalyser(eventLog, catalog);
                var revenue = dataAnalyzer.GetRevenue(from, to);
                Assert.AreEqual(result, revenue);
            }
        }

        private static IEnumerable<TestCaseData> ValidCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2018, 6, 12, 8, 00, 0), new DateTime(2018, 6, 12, 8, 05, 0), decimal.Parse("46827568")).SetName("June valid test");
                yield return new TestCaseData(new DateTime(2018, 10, 11, 15, 00, 0), new DateTime(2018, 10, 11, 15, 05, 0), decimal.Parse("43279237")).SetName("October valid test"); ;
                yield return new TestCaseData(new DateTime(2018, 8, 25, 17, 00, 0), new DateTime(2018, 8, 25, 17, 05, 0), decimal.Parse("40407618")).SetName("August valid test"); ;
            }
        }

        private static IEnumerable<TestCaseData> EdgeCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2018, 10, 11, 21, 0, 0), new DateTime(2018, 10, 11, 22, 0, 0), decimal.Parse("-1")).SetName("Time without events test");
                yield return new TestCaseData(new DateTime(2019, 10, 11, 15, 0, 0), new DateTime(2019, 10, 11, 15, 05, 0), decimal.Parse("-1")).SetName("Missing future dates test"); ;
                yield return new TestCaseData(new DateTime(2017, 5, 12, 8, 0, 0), new DateTime(2017, 5, 12, 8, 05, 0), decimal.Parse("-1")).SetName("Missing past events test"); ;
                yield return new TestCaseData(new DateTime(2018, 6, 12, 8, 0, 0), new DateTime(2018, 6, 12, 8, 0, 0), decimal.Parse("-1")).SetName("Equal dates test"); ;
                yield return new TestCaseData(new DateTime(2018, 5, 12, 8, 05, 0), new DateTime(2018, 5, 12, 8, 0, 0), decimal.Parse("-1")).SetName("Wrong dates order test"); ;
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
                yield return new TestCaseData(new DateTime(2018, 6, 12, 0, 0, 0), new DateTime(2018, 6, 13, 0, 0, 0), decimal.Parse("46827568")).SetName("June valid test");
                yield return new TestCaseData(new DateTime(2018, 10, 1, 0, 0, 0), new DateTime(2018, 10, 07, 0, 0, 0), decimal.Parse("46827568")).SetName("June valid test");
            }
        }
    }
}