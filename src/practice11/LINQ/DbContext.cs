using System;
using System.Collections.Generic;

namespace LINQ
{
    using System.Linq;

    public class DbContext : IDbContext
    {
        public IReadOnlyList<Mark> Marks { get; }
        public IReadOnlyList<Model> Models { get; }
        public IReadOnlyList<Part> Parts { get; }

        public DbContext()
        {
            Marks = new[]
            {
                new Mark(Guid.NewGuid(), "BMW"),
                new Mark(Guid.NewGuid(), "Toyota"),
                new Mark(Guid.NewGuid(), "VW"),
                new Mark(Guid.NewGuid(), "Audi"),
                new Mark(Guid.NewGuid(), "Kia"),
                new Mark(Guid.NewGuid(), "Hyndai")
            };

            Models = new[]
            {
                new Model(Guid.NewGuid(),Marks[0].Id, "X5", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[0].Id, "X6", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[0].Id, "X5", TransmissionType.Auto, 2017),
                new Model(Guid.NewGuid(),Marks[0].Id, "X5", TransmissionType.Auto, 2016),
                new Model(Guid.NewGuid(),Marks[0].Id, "X6", TransmissionType.Auto, 2019),
                new Model(Guid.NewGuid(),Marks[0].Id, "M3", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[0].Id, "M3", TransmissionType.Auto, 2016),
                new Model(Guid.NewGuid(),Marks[0].Id, "M3", TransmissionType.Manual, 2016),
                new Model(Guid.NewGuid(),Marks[0].Id, "M3", TransmissionType.Manual, 2015),
                new Model(Guid.NewGuid(),Marks[0].Id, "M3", TransmissionType.Auto, 2010),
                new Model(Guid.NewGuid(),Marks[0].Id, "M5", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[0].Id, "M5", TransmissionType.Auto, 2016),
                new Model(Guid.NewGuid(),Marks[0].Id, "M5", TransmissionType.Manual, 2015),
                new Model(Guid.NewGuid(),Marks[0].Id, "M5", TransmissionType.Manual, 2016),

                new Model(Guid.NewGuid(),Marks[1].Id, "RAV4", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[1].Id, "RAV4", TransmissionType.Auto, 2014),
                new Model(Guid.NewGuid(),Marks[1].Id, "RAV4", TransmissionType.Auto, 2013),
                new Model(Guid.NewGuid(),Marks[1].Id, "RAV4", TransmissionType.Auto, 2016),
                new Model(Guid.NewGuid(),Marks[1].Id, "RAV4", TransmissionType.Auto, 2017),
                new Model(Guid.NewGuid(),Marks[1].Id, "Camry", TransmissionType.Manual, 1990),
                new Model(Guid.NewGuid(),Marks[1].Id, "Camry", TransmissionType.Manual, 1991),
                new Model(Guid.NewGuid(),Marks[1].Id, "Camry", TransmissionType.Manual, 1994),
                new Model(Guid.NewGuid(),Marks[1].Id, "Camry", TransmissionType.Auto, 2000),
                new Model(Guid.NewGuid(),Marks[1].Id, "Camry", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Auto, 2017),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Auto, 2016),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Auto, 2014),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Manual, 2014),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Manual, 1995),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Manual, 1996),
                new Model(Guid.NewGuid(),Marks[1].Id, "Corolla", TransmissionType.Manual, 1997),

                new Model(Guid.NewGuid(),Marks[2].Id, "Polo", TransmissionType.Auto, 2014),
                new Model(Guid.NewGuid(),Marks[2].Id, "Polo", TransmissionType.Auto, 2012),
                new Model(Guid.NewGuid(),Marks[2].Id, "Polo", TransmissionType.Auto, 2011),
                new Model(Guid.NewGuid(),Marks[2].Id, "Polo", TransmissionType.Manual, 2014),

                new Model(Guid.NewGuid(),Marks[3].Id, "A6", TransmissionType.Auto, 2014),
                new Model(Guid.NewGuid(),Marks[3].Id, "A7", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[3].Id, "A8", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[3].Id, "A8", TransmissionType.Manual, 2017),

                new Model(Guid.NewGuid(),Marks[4].Id, "Rio", TransmissionType.Auto, 2017),
                new Model(Guid.NewGuid(),Marks[4].Id, "Rio", TransmissionType.Manual, 2017),
                new Model(Guid.NewGuid(),Marks[4].Id, "Ceed", TransmissionType.Auto, 2017),
                new Model(Guid.NewGuid(),Marks[4].Id, "Ceed", TransmissionType.Auto, 2018),
                new Model(Guid.NewGuid(),Marks[4].Id, "Ceed", TransmissionType.Auto, 2019),

                new Model(Guid.NewGuid(),Marks[5].Id, "Solaris", TransmissionType.Auto, 2012),
                new Model(Guid.NewGuid(),Marks[5].Id, "Solaris", TransmissionType.Auto, 2013),
                new Model(Guid.NewGuid(),Marks[5].Id, "Solaris", TransmissionType.Auto, 2014),
                new Model(Guid.NewGuid(),Marks[5].Id, "Solaris", TransmissionType.Auto, 2015),
                new Model(Guid.NewGuid(),Marks[5].Id, "Solaris", TransmissionType.Manual, 2015)

            };

            var parts = new List<Part>();

            foreach (var mark in Marks)
            {
                foreach (var model in Models)
                {
                    if (mark.Id == model.MarkId)
                    {
                        for (int i = 0; i < 500; i++)
                        {
                            var crossCodes = Enumerable.Range(0, i / 10 + 1).Select(o => "cross-code-" + o).ToList();
                            parts.Add(new Part(Guid.NewGuid(), "part-code-" + i, "запчасть#" + i, model.Id, crossCodes));
                        }
                    }
                }
            }

            Parts = parts.AsReadOnly();

        }
    }
}