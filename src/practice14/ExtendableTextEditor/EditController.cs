using System;
using System.Collections.Generic;

namespace ExtendableTextEditor
{
    /// <summary>
    ///     Контроллер текстового редактора
    /// </summary>
    public class EditController
    {
        private readonly IControllerState state;
        private readonly Dictionary<string, Func<IControllerState, string[], CommandResult>> commands;

        public EditController(IControllerState state = null)
        {
            this.state = state ?? new ControllerState();
            commands = new Dictionary<string, Func<IControllerState, string[], CommandResult>>
            {
                {"insert", InsertCommand}
            };
        }

        /// <summary>
        ///     Текущая позиция курсора
        /// </summary>
        public int CurrentPosition => state.CurrentPosition;

        /// <summary>
        ///     Редактируемый текст
        /// </summary>
        public string Text => state.Text.ToString();

        /// <summary>
        ///    Применяет команду к текущему состоянию
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="args">Аргументы для выполнения команды (могут отсуствовать)</param>
        /// <returns>Успешность применения команды и сообщение об ошибке</returns>
        public CommandResult ApplyCommand(string command, params string[] args)
        {
            return commands.TryGetValue(command, out var commandFunc) 
                ? commandFunc(state, args) 
                : new CommandResult(false, $"Command '{command}' not found");
        }

        /// <summary>
        ///     Регистрирует команды из плагинов, находящихся в папке с приложением
        /// </summary>
        public void RegisterPlugins()
        {
            throw new NotImplementedException();
        }

        ////////////////
        /// Commands ///
        ////////////////
        private static CommandResult InsertCommand(IControllerState state, string[] args)
        {
            if (args.Length < 1)
                return new CommandResult(false, "Pass argument for insert");
            state.Text.Insert(state.CurrentPosition, args[0]);
            state.CurrentPosition += args[0].Length;
            return new CommandResult(true, null);
        }
    }
}
