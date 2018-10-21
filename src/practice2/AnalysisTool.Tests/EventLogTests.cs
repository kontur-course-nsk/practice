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
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(DataAnalyserTests).Assembly.Location);
            eventLog = new EventLog("events.csv");
        }

        [Test]
        public void ZeroIndexTest()
        {
            var val = eventLog[0];
            Assert.AreEqual(DateTime.Parse("2018-06-01T08:00:00"), val.Date);
            Assert.AreEqual("1186091", val.Article);
            Assert.AreEqual("M14", val.Store);
            Assert.AreEqual(8, val.Count);
        }

        [Test]
        public void Tailing0InStoreNameTest()
        {
            var val = eventLog[11];
            Assert.AreEqual(DateTime.Parse("2018-06-01T08:00:11"), val.Date);
            Assert.AreEqual("1134086", val.Article);
            Assert.AreEqual("M2", val.Store);
            Assert.AreEqual(30, val.Count);
        }

        [Test]
        public void LastItemIndexTest()
        {
            var val = eventLog[eventLog.Count - 1];
            Assert.AreEqual(DateTime.Parse("2018-10-21T19:59:59"), val.Date);
            Assert.AreEqual("1187659", val.Article);
            Assert.AreEqual("M13", val.Store);
            Assert.AreEqual(80, val.Count);
        }

        [Test]
        public void CountIndexTest()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { var val = eventLog[eventLog.Count]; });
        }
    }
}