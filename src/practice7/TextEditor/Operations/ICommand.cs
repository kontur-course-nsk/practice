namespace TextEditor
{
    /// <summary>
    ///     Команда для работы с текстом
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Применяет команду к состоянию
        /// </summary>
        /// <param name="state">Состояние контроллера</param>
        bool Apply(IControllerState state);
        /// <summary>
        ///     Отменяет изменения
        /// </summary>
        /// <param name="state">Состояние контроллера</param>
        bool Revert(IControllerState state);
    }
}