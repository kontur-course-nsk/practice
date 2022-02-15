namespace Model.Todo
{
    using System;

    /// <summary>
    /// Информация для создания задачи
    /// </summary>
    public class TodoBuildInfo
    {
        /// <summary>
        /// Создает задачу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, который хочет создать задачу</param>
        /// <param name="title">Заголовок задачи</param>
        /// <param name="description">Текст задачи</param>
        /// <param name="deadline">Дедлайн задачи</param>
        public TodoBuildInfo(string userId, string title, string description, DateTime deadline)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.UserId = userId;
            this.Title = title;
            this.Description = description;
            this.Deadline = deadline;
        }

        /// <summary>
        /// Идентификатор пользователя, который хочет создать задачу
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Заголовок задачи
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        public DateTime Deadline { get; }
    }
}
