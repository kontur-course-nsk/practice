using System;

namespace TextEditor
{
    /// <summary>
    ///     Добавляет строку за текущим положением курсора
    /// </summary>
    public class InsertStringCommand : ICommand
    {
        private readonly string additionString;

        public InsertStringCommand(string additionString)
        {
            this.additionString = additionString;
        }
        public bool Apply(IControllerState state)
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }

        public bool Revert(IControllerState state)
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
    }
}