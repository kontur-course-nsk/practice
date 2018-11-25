using System;

namespace TextEditor
{
    /// <summary>
    ///     Перемещает курсор
    /// </summary>
    public class MoveCursorCommand : ICommand
    {
        private readonly int moveValue;

        /// <summary>
        /// </summary>
        /// <param name="moveValue">Количество символов, на которое нужно сдвинуть курсор: отрицательное - влево, положительное - вправо</param>
        public MoveCursorCommand(int moveValue)
        {
            this.moveValue = moveValue;
        }
        
        public bool Apply(IControllerState state)
        {
            // здесь умышленно нет проверки на допустимость позиции. пусть контроллер сам научится защащаться от команд, ломающих состояние
            state.CurrentPosition += moveValue;
            return true;
        }

        public bool Revert(IControllerState state)
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
    }
}