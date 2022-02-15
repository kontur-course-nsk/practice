namespace Model.Todo
{
    using System;

    /// <summary>
    /// Параметры поиска задач
    /// </summary>
    public class TodoInfoSearchQuery
    {
        /// <summary>
        /// Позиция, начиная с которой нужно производить поиск
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Максимальеное количество задач, которое нужно вернуть
        /// </summary>
        public int? Limit { get; set; }

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
        /// Искать по параметру "выполнено"
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
