using System;

namespace TextEditor
{
    /// <summary>
    ///     Удаляет символ за текущим положением курсора
    /// </summary>
    public class DeleteCharCommand : ICommand
    {
        private char? deletedChar;
        public bool Apply(IControllerState state)
        {
            if (state.Text.Length <= state.CurrentPosition)
                return false;
            deletedChar = state.Text[state.CurrentPosition];
            state.Text.Remove(state.CurrentPosition, 1);
            return true;
        }

        public bool Revert(IControllerState state)
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
    }
}