using FluentAssertions;
using NUnit.Framework;

namespace TextEditor.Tests
{
    [TestFixture]
    public class InsertCharCommand_Test : OperationFixtureBase
    {
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        public void ApplyShouldInsertChar(int position)
        {
            state.CurrentPosition = position;
            command = new InsertCharCommand('1');
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.Text[position].Should().Be('1');
            state.CurrentPosition.Should().Be(position + 1);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        public void RevertShouldRestoreState(int position)
        {
            state.CurrentPosition = position;
            command = new InsertCharCommand('1');
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();
            state.Should().Be(oldState);
        }
    }
}