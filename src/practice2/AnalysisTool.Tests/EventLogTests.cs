using System;
using System.IO;
using NUnit.Framework;

namespace AnalysisTool.Tests
{
    public class EventLogTests
    {
        private EventLog eventLog;

        [OneTimeSetUp]
        public void Setup()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(DataAnalyzerTests).Assembly.Location);
            eventLog = new EventLog("events.csv");
        }

        [Test]
        public void ZeroIndexTest()
        {
            var val = eventLog[0];
            Assert.AreEqual(DateTime.Parse("2018-06-01T08:00:00"), val.Date);
            Assert.AreEqual("1122033", val.Article);
            Assert.AreEqual("M8", val.Store);
            Assert.AreEqual(73, val.Count);
        }

        [Test]
        public void Tailing0InStoreNameTest()
        {
            var val = eventLog[11];
            Assert.AreEqual(DateTime.Parse("2018-06-01T08:00:11"), val.Date);
            Assert.AreEqual("1028481", val.Article);
            Assert.AreEqual("M20", val.Store);
            Assert.AreEqual(43, val.Count);
        }

        [Test]
        public void LastItemIndexTest()
        {
            var val = eventLog[eventLog.Count - 1];
            Assert.AreEqual(DateTime.Parse("2018-10-21T19:59:59"), val.Date);
            Assert.AreEqual("1240539", val.Article);
            Assert.AreEqual("M21", val.Store);
            Assert.AreEqual(82, val.Count);
        }

        [Test]
        public void CountIndexTest()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { var val = eventLog[eventLog.Count]; });
        }
    }
}