using System;
using System.IO;

namespace Tree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1 && args.Length != 2)
            {
                Console.WriteLine("Usage tree.exe {path} [-f]");
                return;
            }

            var path = args[0];
            var printFiles = args.Length == 2 && args[1] == "-f";
            PrintDirectoryTree(path, printFiles, Console.Out);
        }

        public static void PrintDirectoryTree(string path, bool printFiles, TextWriter writer)
        {
            // todo: реализовать этот метод.
            // Результат записывать как writer.Write() или writer.WriteLine()
        }
    }
}
