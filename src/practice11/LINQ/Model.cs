using System;

namespace LINQ
{
    public class Model
    {
        public Model(Guid id, Guid markId, string name, TransmissionType transmissionType, int year)
        {
            Id = id;
            MarkId = markId;
            Name = name;
            TransmissionType = transmissionType;
            Year = year;
        }

        public Guid Id { get; }

        public Guid MarkId { get; }

        public string Name { get; }

        public TransmissionType TransmissionType { get; }

        public int Year { get; }
    }
}
