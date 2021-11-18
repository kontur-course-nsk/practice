using System;

namespace CmsSystem
{
    public partial class Cms
    {
        public class DataAnalyzer
        {
            private readonly Cms cms;
            private readonly Catalog catalog;

            public DataAnalyzer(Cms cms)
            {
                this.cms = cms ?? throw new ArgumentNullException(nameof(cms));
                this.catalog = new Catalog("catalog.csv");
            }

            /// <summary>
            /// Функция считает полную выручку в указанный промежуток времени.
            /// Алгоритм расчета выручки: 
            /// 1) Взять все события из файла с событиями
            /// 2) умножить количество проданного товара на его цену из каталога
            /// 3) PROFIT!
            /// </summary>
            /// <param name="startDate">Начиная с какой даты и премени считать (включительно)</param>
            /// <param name="endDate">До какой даты и времени считать (включительно)</param>
            /// <returns></returns>
            public decimal GetRevenue(DateTime startDate, DateTime endDate)
            {
                decimal result = 0;
                var dataFound = false;
                foreach (var item in this.cms.bTree.EnumerateRange(startDate, endDate))
                {
                    if (!dataFound)
                    {
                        dataFound = true;
                    }

                    result += decimal.Multiply(
                        item.Value.Count,
                        this.catalog.GetCatalogRecord(item.Value.Article).Price);
                }

                if (!dataFound)
                {
                    return -1;
                }

                return result;
            }

            /// <summary>
            /// Функция возвращает топ 5 самых продаваемых товаров за указанный промежуток времени с указанной аггрегацией по времени.
            /// Можно выбрать распределение за каждый день/неделю/месяц
            /// </summary>
            /// <param name="aggregation"></param>
            /// <param name="startDate">Дата начала поиска</param>
            /// <param name="endDate">Дата окончания поиска</param>
            /// <returns></returns>
            public TopSellingArticlesResult[] GetTopSellingArticles(
                Aggregation aggregation,
                DateTime startDate,
                DateTime endDate)
            {
                throw new NotImplementedException();
            }

            public class TopSellingArticlesResult
            {
                public DateTime Date { get; set; }

                public string[] Top5SellingArticles { get; set; }
            }

            public enum Aggregation
            {
                Day,
                Month,
                Year,
            }
        }
    }
}
