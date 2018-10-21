using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnalysisTool.Models;
using CsvHelper;

namespace AnalysisTool
{
    /// <summary>
    /// Этот класс обеспечивает работу с каталогом. 
    /// Поскольку файл маленький, мы можем распарсить его и держать в памяти.
    /// Note: Асимптотику при работе с каталогом определите сами.
    /// </summary>
    public class Catalog : IReadOnlyList<CatalogRecord>
    {
        private readonly List<CatalogRecord> catalog;

        public Catalog(string catalogPath)
        {
            this.catalog = GetCatalog(catalogPath);
        }

        public CatalogRecord GetCatalogRecord(string article)
        {
            return this.catalog.FirstOrDefault(x => x.Article == article);
        }

        private static List<CatalogRecord> GetCatalog(string catalogPath)
        {
            var parser = new CsvParser(File.OpenText(catalogPath));
            parser.Read();
            var catalog = new List<CatalogRecord>();
            while (true)
            {
                var row = parser.Read();
                if (row == null)
                {
                    break;
                }

                catalog.Add(new CatalogRecord()
                {
                    Article = row[0],
                    Name = row[1],
                    Stores = new List<string>(row.Slice(2, row.Length - 3)),
                    Price = decimal.Parse(row[row.Length - 1])
                });
            }

            return catalog;
        }

        public IEnumerator<CatalogRecord> GetEnumerator()
        {
            return this.catalog.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => this.catalog.Count;

        public CatalogRecord this[int index] => this.catalog[index];
    }

    public static class Extensions
    {
        public static T[] Slice<T>(this T[] source, int index, int length)
        {       
            T[] slice = new T[length];
            Array.Copy(source, index, slice, 0, length);
            return slice;
        }
    }
}