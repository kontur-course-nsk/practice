using System;

namespace AnalysisTool.Models
{
    public class EventRecord
    {
        public DateTime Date { get; set; }

        public string Article { get; set; }

        public string Store { get; set; }

        public int Count { get; set; }
    }
}