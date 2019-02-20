using System.Collections.Generic;

namespace LINQ
{
    public interface IDbContext
    {
        IReadOnlyList<Mark> Marks { get; }

        IReadOnlyList<Model> Models { get; }

        IReadOnlyList<Part> Parts { get; }
    }
}
