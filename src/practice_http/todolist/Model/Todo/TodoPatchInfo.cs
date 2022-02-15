namespace Model.Todo
{
    using System;

    /// <summary>
    /// Информация для изменения задачи
    /// </summary>
    public class TodoPatchInfo
    {
        /// <summary>
        /// Создает новый экземпляр объекта, описывающего изменение задачи
        /// </summary>
        /// <param name="todoId">Идентификатор задачи, которую нужно изменить</param>
        /// <param name="title">Новый заголовок задачи</param>
        /// <param name="description">Новое описание задачи</param>
        /// <param name="isCompleted">Флаг, указывающий, что задача выполнена</param>
        /// /// <param name="deadline">Новый дедлайн задачи</param>
        public TodoPatchInfo(
            string todoId,
            bool? isCompleted = null,
            string title = null,
            string description = null,
            DateTime? deadline = null)
        {
            this.TodoId = todoId;
            this.IsCompleted = isCompleted;
            this.Title = title;
            this.Description = description;
            this.Deadline = deadline;
        }

        /// <summary>
        /// Идентификатор задачи, которую нужно изменить
        /// </summary>
        public string TodoId { get; }

        /// <summary>
        /// Флаг, указывающий, что задача выполнена
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Новый заголовок задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Новое описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Новый дедлайн задачи
        /// </summary>
        public DateTime? Deadline { get; set; }
    }
}
