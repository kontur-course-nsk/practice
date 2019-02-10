using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ExtendableTextEditor;

namespace TextEditor
{
    /// <summary>
    ///     Контроллер текстового редактора
    /// </summary>
    public class EditController
    {
        private readonly IControllerState state;
        private readonly bool throwExceptionIfCommandNotFound;
        
        public EditController(bool throwExceptionIfCommandNotFound = false) : 
            this(new ControllerState(), throwExceptionIfCommandNotFound) {}
        
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
            get { return state.CurrentPosition; }
        }
        
        /// <summary>
        ///     Редактируемый текст
        /// </summary>
        public string Text
        {
            get { return state.Text.ToString(); }
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
                    return Insert(args);
                case "backspace":
                    return Backspace(args);
                 default:
                     if (throwExceptionIfCommandNotFound)
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
            state.Text.Insert(state.CurrentPosition, args[0]);
            state.CurrentPosition += args[0].Length;
            return (true, null);
        }
        
        private (bool, string) Backspace(params string[] args)
        {
            if(state.CurrentPosition <= 0)
                return (false, "No symbols on left side");
            state.CurrentPosition--;
            state.Text.Remove(state.CurrentPosition, 1);
            return (true, null);
        }
        
    }
}