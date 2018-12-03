using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser.Generator
{
    public static class DataGenerator
    {
        public static Stream Generate(int count)
        {
            return EnumerableStream.Create(GetData(count), t => Encoding.UTF8.GetBytes(t));
        }

        private static Random rand = new Random(DateTime.Now.Ticks.GetHashCode());

        private static readonly string[] names = { "Mark", "David", "Jhon", "Freddie", "Marshall", "Jessica", "Kate" };

        private static IEnumerable<string> GetData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"{names[rand.Next(names.Length)]} Age: {rand.Next(18, 43)}" + Environment.NewLine;
                yield return $"--- Amount: {rand.Next(100, 1000) * 10}" + Environment.NewLine;
            }
        }

    }
}
