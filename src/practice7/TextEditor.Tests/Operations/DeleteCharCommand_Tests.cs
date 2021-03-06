using FluentAssertions;
using NUnit.Framework;

namespace TextEditor.Tests
{
    [TestFixture]
    public class DeleteCharCommand_Tests : OperationFixtureBase
    {
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void ApplyShouldDeleteChar(int position)
        {
            state.CurrentPosition = position;
            command = new DeleteCharCommand();
            var oldState = new ControllerState(state);
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.Text.ToString().Should().BeEquivalentTo(oldState.Text.Remove(position, 1).ToString());
            state.CurrentPosition.Should().Be(position);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void RevertShouldRestoreState(int position)
        {
            state.CurrentPosition = position;
            command = new DeleteCharCommand();
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();
            state.Should().Be(oldState);
        }
    }
}