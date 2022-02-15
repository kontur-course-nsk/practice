namespace Model.Todo
{
    /// <summary>
    /// Аспект задачи для сортировки
    /// </summary>
    public enum TodoSortBy
    {
        /// <summary>
        /// Сортировкаи по дате создания
        /// </summary>
        Creation = 0,

        /// <summary>
        /// Сортировка по дате дедлайна
        /// </summary>
        Deadline,
    }
}
