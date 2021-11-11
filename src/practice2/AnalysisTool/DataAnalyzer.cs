using System;

namespace AnalysisTool
{
    public class DataAnalyzer
    {
        private readonly EventLog eventLog;
        private readonly Catalog catalog;

        public DataAnalyzer(EventLog eventLog, Catalog catalog)
        {
            this.eventLog = eventLog ?? throw new ArgumentNullException(nameof(eventLog));
            this.catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
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
            throw new NotImplementedException();
        }
    }
}