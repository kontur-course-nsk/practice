namespace View.Todo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Параметры поиска задач
    /// </summary>
    public class TodoInfoSearchQuery
    {
        /// <summary>
        /// Позиция, начиная с которой нужно производить поиск
        /// </summary>
        [Range(0, int.MaxValue)]
        public int? Offset { get; set; } = 0;

        /// <summary>
        /// Максимальеное количество задач, которое нужно вернуть
        /// </summary>
        [Range(1, 100)]
        public int? Limit { get; set; } = 10;

        /// <summary>
        /// Пользователь, которому принадлежит задача
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Минимальная дата создания задачи
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Максимальная дата создания задачи
        /// </summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary>
        /// Минимальная дата дедлайна задачи
        /// </summary>
        public DateTime? DeadLineFrom { get; set; }

        /// <summary>
        /// Максимальная дата дедлайна задачи
        /// </summary>
        public DateTime? DeadLineTo { get; set; }

        /// <summary>
        /// Искать выполненную задачу
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Тип сортировки
        /// </summary>
        public SortType? Sort { get; set; }

        /// <summary>
        /// Аспект задачи, по которому нужно искать
        /// </summary>
        public TodoSortBy? SortBy { get; set; }
    }
}
