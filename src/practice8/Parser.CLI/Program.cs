using System;
using System.IO;

namespace Parser.CLI
{
    using Parser.Generator.Files;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var bigDataFile = new BigDataFile();

            var sr = new StreamReader(bigDataFile.Open);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(sr.ReadLine());
            }

            Console.ReadKey();

            var directory = KonturDirectory.Dicrectory;

            foreach (var file in directory.Files)
            {
                Console.WriteLine(file.FileName);
            }
        }
    }
}
