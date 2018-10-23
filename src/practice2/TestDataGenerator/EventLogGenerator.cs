using System;
using System.IO;
using AnalysisTool;

namespace TestDataGenerator
{
    public static class EventLogGenerator
    {
        private static readonly Random Rnd = new Random(1123);

        public static void Generate(string catalogPath)
        {
            var catalog = new Catalog(catalogPath);
            var stores = new[] { "M1\0", "M2\0", "M3\0", "M4\0", "M5\0", "M6\0", "M6\0", "M7\0", "M8\0", "M9\0", "M10", "M11", "M12", "M13", "M14", "M15", "M16", "M17", "M18", "M19", "M20", "M21", "M22", "M23", "M24", "M25", "M26", "M27" };
            using (var writer = new StreamWriter("events.csv"))
            {
                for (var i = 6; i < 11; i++)
                {

                    for (var j = 1; j < 30; j++)
                    {
                        if ((i == 2 && j == 29) || (i == 10 && j >= 22))
                        {
                            continue;
                        }

                        for (var k = 8; k < 20; k++) //часы
                        {
                            for (var l = 0; l < 60; l++) // минуты
                            {
                                for (var m = 0; m < 60; m++) // секунды
                                {
                                    // если хотите получить многогигабайтный файл, то просто воткните еще один вложенный цикл от 0 до 100
                                    var str = new DateTime(2018, i, j, k, l, m).ToString("s");
                                    str += $";{catalog[Rnd.Next(0, catalog.Count - 1)].Article.PadLeft(10, '0')}";
                                    str += $";{stores[Rnd.Next(0, 26)]}";
                                    str += $";{Rnd.Next(1, 100):D3}";
                                    writer.Write($"{str}\r\n");
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}
