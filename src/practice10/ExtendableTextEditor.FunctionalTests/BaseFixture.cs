namespace ExtendableTextEditor.Tests
{
    using NUnit.Framework;

    public abstract class BaseFixture
    {
        protected GlobalEditorSettings settings;
        protected EditController controller;
        protected IControllerState state;

        [SetUp]
        public void Setup()
        {
            this.settings = new GlobalEditorSettings
            {
                ThrowExceptionIfCommandNotFound = false
            };
            this.state = new ControllerState();
            this.state.Text.Append("abcdefg");
            this.state.CurrentPosition = 3;
            // TODO: заменить на реализацию "горячих" настроек
            this.controller = new EditController(this.state, this.settings.ThrowExceptionIfCommandNotFound);
            this.SetupActions();
        }

        /// <summary>
        /// Вызывается последним в Setup()
        /// </summary>
        protected virtual void SetupActions()
        {
        }
    }
}
