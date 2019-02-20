using System;

namespace LINQ
{
    public class SearchPartsResult
    {
        public string MarkName { get; set; }

        public string ModelName { get; set; }

        public int Year { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public string PartName { get; set; }

        public string PartCode { get; set; }

        public string[] CrossCodes { get; set; }

        public Guid PartId { get; set; }

        public override string ToString()
        {
            return $"{MarkName} {ModelName} {Year} {TransmissionType}, {PartCode} - {PartName}";
        }
    }
}
