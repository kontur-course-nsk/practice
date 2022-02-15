namespace View.Todo
{
    using System.Collections.Generic;

    /// <summary>
    ///  Список c описанием задач
    /// </summary>
    public sealed class TodoList
    {
        /// <summary>
        /// Краткая информация о задачах
        /// </summary>
        public IReadOnlyCollection<TodoInfo> todoItems { get; set; }
    }
}
