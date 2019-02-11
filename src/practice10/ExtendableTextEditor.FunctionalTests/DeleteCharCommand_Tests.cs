using FluentAssertions;
using NUnit.Framework;
using TextEditor;

namespace ExtendableTextEditor.FunctionalTests
{
    [TestFixture]
    public class DeleteCharCommand_Tests : BaseFixture
    {
        protected override void SetupActions()
        {
            // TODO: зарегистрировать команду в контроллере
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void ApplyShouldDeleteChar(int position)
        {
            state.CurrentPosition = position;
            var oldState = new ControllerState(state);
            
            var result = controller.ApplyCommand("delete");

            result.IsSuccess.Should().BeTrue();
            state.Text.ToString().Should().BeEquivalentTo(oldState.Text.Remove(position, 1).ToString());
            state.CurrentPosition.Should().Be(position);
        }
    }
}