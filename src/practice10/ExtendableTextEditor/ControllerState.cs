using System.Text;

namespace TextEditor
{
    /// <summary>
    ///     Внутренн состояние контроллера
    /// </summary>
    public class ControllerState : IControllerState
    {       
        /// <summary>
        ///     Редактируемый текст
        /// </summary>
        public StringBuilder Text { get; }
        /// <summary>
        ///     Текущая позиция курсора
        /// </summary>
        public int CurrentPosition { get; set; }

        public ControllerState()
        {
            Text = new StringBuilder();
        }

        public ControllerState(IControllerState source)
        {
            Text = new StringBuilder(source.Text.ToString());
            CurrentPosition = source.CurrentPosition;
        }

        /// <summary>
        ///     Проверяет текщие значения на допустимость
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool IsValid()
        {
            return CurrentPosition >= 0 && CurrentPosition <= Text.Length;
        }

        public override bool Equals(object obj)
        {
            var another = obj as ControllerState;
            if (another == null)
                return false;
            return this.Text.ToString().Equals(another.Text.ToString()) 
                   && this.CurrentPosition == another.CurrentPosition;
        }
    }
}