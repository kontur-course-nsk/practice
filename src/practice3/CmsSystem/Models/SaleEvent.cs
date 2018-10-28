using System;

namespace CmsSystem.Models
{
    public class SaleEvent
    {
        public DateTime DateTime { get; set; }

        public string StoreName { get; set; }

        public string Article { get; set; }

        public int Count { get; set; }
    }
}