using System.Collections.Generic;

namespace AnalysisTool.Models
{
    public class CatalogRecord
    {
        public string Article { get; set; }

        public string Name { get; set; }

        public IList<string> Stores { get; set; } = new List<string>();

        public decimal Price { get; set; }
    }
}