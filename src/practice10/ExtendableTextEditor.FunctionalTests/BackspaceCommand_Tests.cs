using FluentAssertions;
using NUnit.Framework;
using TextEditor;

namespace ExtendableTextEditor.FunctionalTests
{
    public class BackspaceCommand_Tests : BaseFixture
    {
        protected override void SetupActions()
        {
            // TODO: зарегистрировать команду в контроллере
        }
        
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public void ApplyShouldDeleteChar(int position)
        {
            state.CurrentPosition = position;
            var oldState = new ControllerState(state);
            
            var result = controller.ApplyCommand("backspace");

            result.IsSuccess.Should().BeTrue();
            state.Text.ToString().Should().BeEquivalentTo(oldState.Text.Remove(position - 1, 1).ToString());
            state.CurrentPosition.Should().Be(position - 1);
        }
    }
}