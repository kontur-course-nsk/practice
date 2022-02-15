namespace Model.Users
{
    using System;

    /// <summary>
    /// Исключение, которое возникает при попытке создать существующего пользователя
    /// </summary>
    public class UserDuplicationException : Exception
    {
        /// <summary>
        /// Инициализировать эксземпляр исключения по логину пользователя
        /// </summary>
        /// <param name="login"></param>
        public UserDuplicationException(string login)
            : base($"A user with login \"{login}\" is already exist.")
        {
        }
    }
}
