using NUnit.Framework;
using TextEditor;

namespace ExtendableTextEditor.FunctionalTests
{
    public abstract class BaseFixture
    {
        protected GlobalEditorSettings settings;
        protected EditController controller;
        protected IControllerState state;

        [SetUp]
        public void Setup()
        {
            settings = new GlobalEditorSettings
            {
                ThrowExceptionIfCommandNotFound = false
            };
            state = new ControllerState();
            state.Text.Append("abcdefg");
            state.CurrentPosition = 3;
            // TODO: заменить на реализацию "горячих" настроек
            controller = new EditController(state, settings.ThrowExceptionIfCommandNotFound);
            SetupActions();
        }

        /// <summary>
        /// Вызывается последним в Setup()
        /// </summary>
        protected virtual void SetupActions()
        {
            
        }
    }
}