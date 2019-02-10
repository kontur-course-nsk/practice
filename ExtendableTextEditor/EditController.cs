using System;
using System.Collections.Generic;
using System.Text;

namespace TextEditor
{
    /// <summary>
    ///     Контроллер текстового редактора
    ///     Хранит внутреннее состояние и оповещает о его изменеии UI (работа с UI не реализована)
    /// </summary>
    public class EditController
    {
        private readonly ControllerState state;
        
        public EditController() : 
            this(new ControllerState()) {}
        
        public EditController(ControllerState state)
        {
            this.state = state;
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