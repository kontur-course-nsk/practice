using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CmsSystem.Models;
using CSharpTest.Net.Collections;
using CSharpTest.Net.Serialization;

namespace CmsSystem
{
    /// <summary>
    /// Класс для управления сетью магазинов. 
    /// Все магазины вызываают метод Add при осуществлении продажи.
    /// Вы вольны выбирать любой способ хранения этой информации. Но есть важное ограничение, что ваша Cms должна быть стабильна. 
    /// Перезапуск приложения не должен приводить к потере информации. 
    /// </summary>
    public partial class Cms : IDisposable
    {
        private readonly BPlusTree<DateTime, IndexValue>.OptionsV2 options =
            new BPlusTree<DateTime, IndexValue>.OptionsV2(PrimitiveSerializer.DateTime, new IndexValueSerializer())
            {
                FileName = Path.Combine(Directory.GetCurrentDirectory(), "cms.data"),
                CreateFile = CreatePolicy.IfNeeded,
                StoragePerformance = StoragePerformance.CommitToDisk
            }.CalcBTreeOrder(8, 32);

        private readonly BPlusTree<DateTime, IndexValue> bTree;

        public Cms()
        {
            bTree = new BPlusTree<DateTime, IndexValue>(options);
        }

        /// <summary>
        /// Добавляет инфомрацию о продаже в CMS
        /// </summary>
        /// <param name="saleEvent">Событие о продаже</param>
        public void Add(SaleEvent saleEvent)
        {
            if (saleEvent == null)
            {
                throw new ArgumentNullException(nameof(saleEvent));
            }

            bTree.Add(saleEvent.DateTime, new IndexValue() { Article = saleEvent.Article, Store = saleEvent.StoreName, Count = saleEvent.Count });
            bTree.Commit();
        }

        /// <summary>
        /// Добавляет инфомрацию о пачке продаж в CMS
        /// </summary>
        /// <param name="saleEvents">События о продажах</param>
        public void AddRange(IEnumerable<SaleEvent> saleEvents)
        {
            if (saleEvents == null)
            {
                throw new ArgumentNullException(nameof(saleEvents));
            }

            var data = saleEvents.Select(x => new KeyValuePair<DateTime, IndexValue>(x.DateTime, new IndexValue() { Article = x.Article, Store = x.StoreName, Count = x.Count }));
            bTree.AddRange(data);
            bTree.Commit();
        }

        /// <summary>
        /// Удаляет запись за конкретное время и по конкретному артикулу
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="article"></param>
        public void Remove(DateTime dateTime, string article)
        {
            bTree.Remove(dateTime);
            bTree.Commit();
        }

        /// <summary>
        /// Возвращает класс для расчтета статистики
        /// </summary>
        /// <returns></returns>
        public DataAnalyzer GetDataAnalyzer()
        {
            // Таким образом DataAnalyzer получет доступ к внутреннему состоянию вашей Cms
            return new DataAnalyzer(this);
        }

        public void Dispose()
        {
            bTree?.Dispose();
        }
    }
}