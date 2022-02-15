namespace Model.Todo
{
    using System;

    /// <summary>
    /// Информация о задаче
    /// </summary>
    public class TodoInfo
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        /// [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Флаг, указывающий, выполнена ли задача
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        public string Title { get; set; }
    }
}
