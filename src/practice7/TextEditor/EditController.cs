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
        private readonly IEditArea area;
        private readonly ControllerState state;
        
        public EditController(IEditArea area)
        {
            this.area = area;
            state = new ControllerState();
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
        /// <returns>Успешность применения команды</returns>
        public bool ApplyCommand(ICommand command)
        {
            if (!command.Apply(state))
                return false;
            if (state.IsValid())
                return true;
            command.Revert(state);
            return false;
        }
        
        /// <summary>
        ///     Отменяет последнюю команду
        /// </summary>
        /// <returns>Успешность отмены</returns>
        public bool Undo()
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
        
        /// <summary>
        ///     Применяет обратно последнюю отменённую операцию
        /// </summary>
        /// <returns>Успешность применения</returns>
        public bool Redo()
        {
            throw new NotImplementedException("Реализуй самостоятельно");
        }
    }
}