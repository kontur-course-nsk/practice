using System;
using System.Collections.Generic;

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

            var filteredModels = new List<Model>();
            var mark = GetMark(query.MarkName);
            for (int i = 0; i < context.Models.Count; i++)
            {
                if (context.Models[i].MarkId == mark.Id)
                {
                    filteredModels.Add(context.Models[i]);
                }
            }

            var dictionaryYears = new Dictionary<string, HashSet<int>>();
            var dictionaryTransmission = new Dictionary<string, HashSet<TransmissionType>>();

            for (int i = 0; i < filteredModels.Count; i++)
            {
                if (dictionaryYears.ContainsKey(filteredModels[i].Name))
                {
                    dictionaryYears[filteredModels[i].Name].Add(filteredModels[i].Year);
                }
                else
                {
                    dictionaryYears[filteredModels[i].Name] = new HashSet<int> { filteredModels[i].Year };
                }

                if (dictionaryTransmission.ContainsKey(filteredModels[i].Name))
                {
                    dictionaryTransmission[filteredModels[i].Name].Add(filteredModels[i].TransmissionType);
                }
                else
                {
                    dictionaryTransmission[filteredModels[i].Name] = new HashSet<TransmissionType> { filteredModels[i].TransmissionType };
                }
            }

            var result = new List<SearchModelsResult>();

            for (int i = 0; i < filteredModels.Count; i++)
            {
                if (result.Find(o => o.ModelName == filteredModels[i].Name) == null)
                {
                    var item = new SearchModelsResult
                    {
                        MarkName = GetMark(filteredModels[i].MarkId).Name,
                        ModelName = filteredModels[i].Name,
                        TransmissionTypes = dictionaryTransmission[filteredModels[i].Name].ToArray(),
                        Years = dictionaryYears[filteredModels[i].Name].ToArray()
                    };
                    result.Add(item);
                }
            }

            var resultArray = result.ToArray();

            Array.Sort(resultArray, new SearchModelsResultComparer());

            return resultArray;
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

            var filteredMarks = new List<Mark>(context.Marks);
            var filteredModels = new List<Model>(context.Models);
            var filteredParts = new List<Part>(context.Parts);

            if (query.MarkName != null)
            {
                filteredMarks = FilterByMarkName(query, filteredMarks);
            }

            filteredModels = FilterByMarks(filteredModels, filteredMarks);

            if (query.ModelName != null)
            {
                filteredModels = FilterByModelName(query, filteredModels);
            }

            if (query.Year != null)
            {
                filteredModels = FilterByYear(query, filteredModels);
            }

            if (query.TransmissionType != null)
            {
                filteredModels = FilterByTransmissionType(query, filteredModels);
            }

            filteredParts = FilterByModels(filteredParts, filteredModels);

            if (query.PartCode != null)
            {
                filteredParts = FilterByPartCode(query, filteredParts);
            }

            if (query.PartName != null)
            {
                filteredParts = FilterByPartName(query, filteredParts);
            }

            var result = new List<SearchPartsResult>();

            for (int i = 0; i < filteredParts.Count; i++)
            {
                var model = GetModel(filteredParts[i].ModelId);
                var mark = GetMark(model.MarkId);

                var item = new SearchPartsResult
                {
                    PartCode = filteredParts[i].Code,
                    CrossCodes = filteredParts[i].CrossCodes.ToArray(),
                    PartName = filteredParts[i].Name,
                    PartId = filteredParts[i].Id,
                    MarkName = mark.Name,
                    ModelName = model.Name,
                    TransmissionType = model.TransmissionType,
                    Year = model.Year
                };

                result.Add(item);
            }

            return result.ToArray();
        }

        private Mark GetMark(Guid markId)
        {
            for (int i = 0; i < context.Marks.Count; i++)
            {
                if (context.Marks[i].Id == markId)
                {
                    return context.Marks[i];
                }
            }

            throw new InvalidOperationException();
        }

        private Mark GetMark(string markName)
        {
            for (int i = 0; i < context.Marks.Count; i++)
            {
                if (context.Marks[i].Name.Equals(markName, StringComparison.OrdinalIgnoreCase))
                {
                    return context.Marks[i];
                }
            }

            throw new InvalidOperationException();
        }

        private Model GetModel(Guid modelId)
        {
            for (int i = 0; i < context.Models.Count; i++)
            {
                if (context.Models[i].Id == modelId)
                {
                    return context.Models[i];
                }
            }

            throw new InvalidOperationException();
        }

        private List<Part> FilterByPartName(SearchPartsQuery partsQuery, List<Part> parts)
        {
            var tempParts = new List<Part>();

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].Name.Equals(partsQuery.PartName, StringComparison.OrdinalIgnoreCase))
                {
                    tempParts.Add(parts[i]);
                }
            }

            return tempParts;
        }

        private static List<Part> FilterByModels(List<Part> parts, List<Model> models)
        {
            var tempParts = new List<Part>();

            for (int i = 0; i < parts.Count; i++)
            {
                if (models.Exists(o => o.Id == parts[i].ModelId))
                {
                    tempParts.Add(parts[i]);
                }
            }

            return tempParts;
        }

        private List<Part> FilterByPartCode(SearchPartsQuery partsQuery, List<Part> parts)
        {
            var tempParts = new List<Part>();

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].Code.Equals(partsQuery.PartCode, StringComparison.OrdinalIgnoreCase))
                {
                    tempParts.Add(parts[i]);
                }
                else if (parts[i].CrossCodes.Contains(partsQuery.PartCode))
                {
                    tempParts.Add(parts[i]);
                }
            }

            return tempParts;
        }

        private static List<Model> FilterByMarks(List<Model> models, List<Mark> marks)
        {
            var tempModels = new List<Model>();

            for (int i = 0; i < models.Count; i++)
            {
                if (marks.Exists(o => o.Id == models[i].MarkId))
                {
                    tempModels.Add(models[i]);
                }
            }

            return tempModels;
        }

        private static List<Model> FilterByTransmissionType(SearchPartsQuery partsQuery, List<Model> filteredModels)
        {
            var tempModels = new List<Model>();

            for (int i = 0; i < filteredModels.Count; i++)
            {
                if (filteredModels[i].TransmissionType == partsQuery.TransmissionType.Value)
                {
                    tempModels.Add(filteredModels[i]);
                }
            }

            return tempModels;
        }

        private static List<Model> FilterByYear(SearchPartsQuery partsQuery, List<Model> filteredModels)
        {
            var tempModels = new List<Model>();

            for (int i = 0; i < filteredModels.Count; i++)
            {
                if (filteredModels[i].Year == partsQuery.Year.Value)
                {
                    tempModels.Add(filteredModels[i]);
                }
            }

            return tempModels;
        }

        private static List<Model> FilterByModelName(SearchPartsQuery partsQuery, List<Model> filteredModels)
        {
            var tempModels = new List<Model>();

            for (int i = 0; i < filteredModels.Count; i++)
            {
                if (filteredModels[i].Name.Equals(partsQuery.ModelName, StringComparison.OrdinalIgnoreCase))
                {
                    tempModels.Add(filteredModels[i]);
                }
            }

            return tempModels;
        }

        private static List<Mark> FilterByMarkName(SearchPartsQuery partsQuery, List<Mark> filteredMarks)
        {
            var tempMarks = new List<Mark>();

            for (int i = 0; i < filteredMarks.Count; i++)
            {
                if (filteredMarks[i].Name.Equals(partsQuery.MarkName, StringComparison.OrdinalIgnoreCase))
                {
                    tempMarks.Add(filteredMarks[i]);
                }
            }

            return tempMarks;
        }

        public StatisticResult[] GetPartsStatistic()
        {
            var result = context.Parts.GroupBy(o => o.Name).Select(o => new
            {
                Name = o.Key,
                Models = o.Select(p => context.Models.First(m => m.Id == p.ModelId)).GroupBy(p => p.Name)
                        .Select(p => new
                        {
                            ModelName = p.Key,
                            MarkId = p.Select(s => s.MarkId).First(),
                            SuitableForAuto = p.Select(t => t.TransmissionType).Any(t => t == TransmissionType.Auto)
                        }).Distinct().ToArray()
            })
            .Select(o => new
            {
                o.Name,
                Models = o.Models.Select(m => m.ModelName).Distinct().ToArray(),
                Marks = context.Marks.Where(m => o.Models.Select(s => s.MarkId).Contains(m.Id)).Select(t => t.Name).Distinct().ToArray(),
                SuitableForAuto = o.Models.Any(m => m.SuitableForAuto)
            })
            .Select(o => new StatisticResult
            {
                PartName = o.Name,
                Models = o.Models,
                Marks = o.Marks,
                SuitableForAuto = o.SuitableForAuto
            })
            .ToArray();

            return result;
        }
    }
}
