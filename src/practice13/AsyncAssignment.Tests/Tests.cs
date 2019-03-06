using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace AsyncAssignment.Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var dir = Path.GetDirectoryName(typeof(Tests).Assembly.Location);
            Environment.CurrentDirectory = dir;
        }

        [Test]
        public void SyncTest()
        {
            var lines = File.ReadAllLines("VoinaIMir.txt");

            var stopwatch = new Stopwatch();
            var worker = new MainWorker();
            for (var index = 0; index < lines.Length; index++)
            {
                worker.SplitToWordsAndCount(lines[index]);
            }
            stopwatch.Stop();

            Console.WriteLine($"Elapsed {stopwatch.ElapsedMilliseconds} ms.");
            var top10words = worker.GetTop10АrequentlyUsedWords();
            foreach (var keyValuePair in top10words)
            {
                Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
            }
        }
    }
}
