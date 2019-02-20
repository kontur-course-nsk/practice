using System;

namespace LINQ
{
    using System.Linq;

    public class SearchEngine
    {
        private readonly IDbContext context;

        public SearchEngine(IDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Список наименований моделей со всеми годами производства и типами трансмиссий. Модели упорядочены по дате возрастания первого года производства.
        /// </summary>
        public SearchModelsResult[] SearchModels(SearchModelsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var marks = context.Marks.ToDictionary(o => o.Id);
            var mark = context.Marks.First(o => o.Name == query.MarkName);

            var models = context.Models
                .Where(o => o.MarkId == mark.Id)
                .GroupBy(o => o.Name)
                .Select(o => new SearchModelsResult
                {
                    ModelName = o.Key,
                    MarkName = marks[o.Select(m => m.MarkId).First()].Name,
                    TransmissionTypes = o.Select(m => m.TransmissionType).Distinct().ToArray(),
                    Years = o.Select(m => m.Year).Distinct().ToArray()
                })
                .OrderBy(o => o.Years.Min())
                .ToArray();

            return models;
        }

        /// <summary>
        /// Поиск запчастей по множеству фильтров. Все фильтры работают через "и"
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SearchPartsResult[] SearchParts(SearchPartsQuery query)
        {
            if (query == null)
            {
                throw new System.ArgumentNullException(nameof(query));
            }

            var queryable = from mark in context.Marks
                            join model in context.Models on mark.Id equals model.MarkId
                            join part in context.Parts on model.Id equals part.ModelId
                            select new
                            {
                                Mark = mark,
                                Model = model,
                                Part = part
                            };

            if (query.MarkName != null)
            {
                queryable = queryable.Where(o => o.Mark.Name.Equals(query.MarkName, StringComparison.OrdinalIgnoreCase));
            }

            if (query.ModelName != null)
            {
                queryable = queryable.Where(o => o.Model.Name.Equals(query.ModelName, StringComparison.OrdinalIgnoreCase));
            }

            if (query.Year != null)
            {
                queryable = queryable.Where(o => o.Model.Year == query.Year.Value);
            }

            if (query.TransmissionType != null)
            {
                queryable = queryable.Where(o => o.Model.TransmissionType == query.TransmissionType.Value);
            }

            if (query.PartCode != null)
            {
                queryable = queryable.Where(o => o.Part.Code.Equals(query.PartCode, StringComparison.OrdinalIgnoreCase)
                || o.Part.CrossCodes.Any(c => c.Equals(query.PartCode, StringComparison.OrdinalIgnoreCase)));
            }

            if (query.PartName != null)
            {
                queryable = queryable.Where(o => o.Part.Name.Equals(query.PartName, StringComparison.OrdinalIgnoreCase));
            }

            return queryable.Select(o => new SearchPartsResult
            {
                CrossCodes = o.Part.CrossCodes.ToArray(),
                MarkName = o.Mark.Name,
                ModelName = o.Model.Name,
                PartCode = o.Part.Code,
                PartId = o.Part.Id,
                PartName = o.Part.Name,
                TransmissionType = o.Model.TransmissionType,
                Year = o.Model.Year
            }).ToArray();
        }
        public StatisticResult[] GetPartsStatistic()
        {
            var models = context.Models.ToDictionary(o => o.Id);
            var marks = context.Marks.ToDictionary(o => o.Id);

            var result = context.Parts.GroupBy(o => o.Name)
                .Select(o => new
                {
                    Name = o.Key,
                    Models = o.Select(m => m.ModelId).Distinct().Select(m => models[m])
                })
                .Select(o => new
                {
                    o.Name,
                    o.Models,
                    Marks = o.Models.Select(m => m.MarkId).Distinct().Select(m => marks[m])
                })
                .Select(o => new StatisticResult
                {
                    PartName = o.Name,
                    Models = o.Models.Select(m => m.Name).Distinct().ToArray(),
                    Marks = o.Marks.Select(m => m.Name).Distinct().ToArray(),
                    SuitableForAuto = o.Models.Any(m => m.TransmissionType == TransmissionType.Auto)
                }).ToArray();

            return result;
        }
    }
}
