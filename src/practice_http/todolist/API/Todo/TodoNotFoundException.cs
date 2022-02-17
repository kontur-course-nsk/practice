namespace API.Todo
{
    using System;

    /// <summary>
    /// Исключение, которое возникает при попытке получить несуществующую задачу
    /// </summary>
    public class TodoNotFoundException : Exception
    {
        /// <summary>
        /// Создает новый экземпляр исключения о том, что задача не найдена
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемой задачи</param>
        public TodoNotFoundException(string id)
            : base($"Todo with id {id} is not found.")
        {
        }
    }
}
