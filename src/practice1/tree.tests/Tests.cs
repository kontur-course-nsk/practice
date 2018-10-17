using System;
using System.IO;
using NUnit.Framework;

namespace Tree.Tests
{
    public class Tests
    {
        public const string FullResult = @"├───project
│	├───file.txt (19b)
│	└───kontur.png (6796b)
├───static
│	├───a_lorem
│	│	├───dolor.txt (empty)
│	│	├───ipsum
│	│	│	└───kontur.png (6796b)
│	│	└───kontur.png (6796b)
│	├───css
│	│	└───body.css (28b)
│	├───empty.txt (empty)
│	├───html
│	│	└───index.html (61b)
│	├───js
│	│	└───site.js (10b)
│	└───z_lorem
│		├───dolor.txt (empty)
│		├───kontur.png (6796b)
│		└───z_ipsum
│			└───kontur.png (6796b)
├───zline
│	├───empty.txt (empty)
│	└───lorem
│		├───dolor.txt (empty)
│		└───kontur.png (6796b)
└───zzfile.txt (empty)";

        public const string DirectoriesOnlyResult = @"├───project
├───static
│	├───a_lorem
│	│	└───ipsum
│	├───css
│	├───html
│	├───js
│	└───z_lorem
│		└───z_ipsum
└───zline
	└───lorem";

        [OneTimeSetUp]
        public void Setup()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Tests).Assembly.Location);
        }

        [Test]
        public void FullResultTest()
        {
            var writer = new StringWriter();
            Program.PrintDirectoryTree("testData", true, writer);

            Assert.AreEqual(FullResult, writer.ToString());
        }

        [Test]
        public void DirectoriesOnlyResultTest()
        {
            var writer = new StringWriter();
            Program.PrintDirectoryTree("testData", false, writer);

            Assert.AreEqual(DirectoriesOnlyResult, writer.ToString());
        }
    }
}
