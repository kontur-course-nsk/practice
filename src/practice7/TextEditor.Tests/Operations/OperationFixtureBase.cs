using NUnit.Framework;

namespace TextEditor.Tests
{
    public abstract class OperationFixtureBase
    {
        protected IControllerState state;
        protected ICommand command;

        [SetUp]
        public void SetUp()
        {
            state = new ControllerState();
            state.Text.Append("abcdefg");
            state.CurrentPosition = 3;
        }
    }
}