using System;

namespace LINQ
{
    public class Mark
    {
        public Mark(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
