using System;

namespace TextEditor
{
    /// <summary>
    ///     Добавляет символ за текущем положением курсора
    /// </summary>
    public class InsertCharCommand : ICommand
    {
        private readonly char additionChar;

        public InsertCharCommand(char additionChar)
        {
            this.additionChar = additionChar;
        }
        public bool Apply(IControllerState state)
        {
            state.Text.Insert(state.CurrentPosition, additionChar);
            state.CurrentPosition++;
            return true;
        }

        public bool Revert(IControllerState state)
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
    }
}