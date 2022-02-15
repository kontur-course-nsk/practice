namespace View.Todo
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Информация для изменения задачи
    /// </summary>
    [DataContract]
    public class TodoPatchInfo
    {
        /// <summary>
        /// Флаг, указывающий, что задача выполнена
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Новый заголовок задачи
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Title { get; set; }

        /// <summary>
        /// Новое описание задачи
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Новый дедлайн задачи
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? Deadline { get; set; }
    }
}
