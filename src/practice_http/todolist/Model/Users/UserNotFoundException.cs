namespace Model.Users
{
    using System;

    /// <summary>
    /// Исключение, которое возникает при попытке получить несуществующего пользователя
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Инициализировать экземпляр исключения по логину пользователя
        /// </summary>
        /// <param name="login"></param>
        public UserNotFoundException(string login)
            : base($"A user by login \"{login}\" is not found.")
        {
        }
    }
}
