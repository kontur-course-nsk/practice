namespace ExtendableTextEditor
{
    using System.Text;

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
            this.Text = new StringBuilder();
        }

        public ControllerState(IControllerState source)
        {
            this.Text = new StringBuilder(source.Text.ToString());
            this.CurrentPosition = source.CurrentPosition;
        }

        /// <summary>
        ///     Проверяет текщие значения на допустимость
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool IsValid()
        {
            return this.CurrentPosition >= 0 && this.CurrentPosition <= this.Text.Length;
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
