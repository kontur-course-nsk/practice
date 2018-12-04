using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser.Generator
{
    public static class DataGenerator
    {
        private static readonly Random Rand = new Random(DateTime.Now.Ticks.GetHashCode());
        private static readonly string[] Names = { "Mark", "David", "Jhon", "Freddie", "Marshall", "Jessica", "Kate" };

        public static Stream Generate(int count)
        {
            return EnumerableStream.Create(GetData(count), t => Encoding.UTF8.GetBytes(t));
        }

        public static Stream GenerateSlow(int count)
        {
            return SlowEnumerableStream.Create(GetData(count), t => Encoding.UTF8.GetBytes(t));
        }

        private static IEnumerable<string> GetData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"{Names[Rand.Next(Names.Length)]} Age: {Rand.Next(18, 43)}" + Environment.NewLine;
                yield return $"--- Amount: {Rand.Next(100, 1000) * 10}" + Environment.NewLine;
            }
        }

    }
}
