namespace View.Todo
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Информация для создания ззадачи
    /// </summary>
    [DataContract]
    public class TodoBuildInfo
    {
        /// <summary>
        /// Заголовок задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime Deadline { get; set; }
    }
}
