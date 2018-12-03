using Parser.Generator;
using System;
using System.IO;

namespace Parser.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(DataGenerator.Generate(10));

            string s = null;
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }
    }
}
