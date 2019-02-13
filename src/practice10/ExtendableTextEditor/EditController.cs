namespace ExtendableTextEditor
{
    /// <summary>
    ///     Контроллер текстового редактора
    /// </summary>
    public class EditController
    {
        private readonly IControllerState state;
        private readonly bool throwExceptionIfCommandNotFound;

        public EditController(bool throwExceptionIfCommandNotFound = false) :
            this(new ControllerState(), throwExceptionIfCommandNotFound)
        {
        }

        public EditController(IControllerState state, bool throwExceptionIfCommandNotFound = false)
        {
            this.state = state;
            this.throwExceptionIfCommandNotFound = throwExceptionIfCommandNotFound;
        }

        /// <summary>
        ///     Текущая позиция курсора
        /// </summary>
        public int CurrentPosition
        {
            get { return this.state.CurrentPosition; }
        }

        /// <summary>
        ///     Редактируемый текст
        /// </summary>
        public string Text
        {
            get { return this.state.Text.ToString(); }
        }

        /// <summary>
        ///    Применяет команду к текущему состоянию
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="args">Аргументы для выполнения команды (могут отсуствовать)</param>
        /// <returns>Успешность применения команды и сообщение об ошибке</returns>
        public (bool IsSuccess, string ErrorMessage) ApplyCommand(string command, params string[] args)
        {
            switch (command)
            {
                case "insert":
                    return this.Insert(args);
                case "backspace":
                    return this.Backspace(args);
                default:
                    if (this.throwExceptionIfCommandNotFound)
                        throw new CommandNotFoundException(command);
                    else
                        return (false, $"Command '{command}' not found");
            }
        }

        ////////////////
        /// Commands ///
        ////////////////
        private (bool, string) Insert(params string[] args)
        {
            if (args.Length < 1)
                return (false, "Pass argument for insert");
            this.state.Text.Insert(this.state.CurrentPosition, args[0]);
            this.state.CurrentPosition += args[0].Length;
            return (true, null);
        }

        private (bool, string) Backspace(params string[] args)
        {
            if (this.state.CurrentPosition <= 0)
                return (false, "No symbols on left side");
            this.state.CurrentPosition--;
            this.state.Text.Remove(this.state.CurrentPosition, 1);
            return (true, null);
        }
    }
}
