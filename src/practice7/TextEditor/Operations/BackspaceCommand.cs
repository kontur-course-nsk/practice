using System;

namespace TextEditor
{
    /// <summary>
    ///     Удаляет символ перед текущим положением курсора
    /// </summary>
    public class BackspaceCommand : ICommand
    {
        private char? deletedChar;
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