using FluentAssertions;
using NUnit.Framework;

namespace TextEditor.Tests
{
    [TestFixture]
    public class InsertStringCommand_Test : OperationFixtureBase
    {
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        public void ApplyShouldInsertStringInDifferentPositions(int position)
        {
            state.CurrentPosition = position;
            command = new InsertStringCommand("1");
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.Text[position].Should().Be('1');
            state.CurrentPosition.Should().Be(position + 1);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        public void RevertShouldRestoreStateInDifferentPositions(int position)
        {
            state.CurrentPosition = position;
            command = new InsertStringCommand("1");
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();
            state.Should().Be(oldState);
        }
        
        [TestCase("1")]
        [TestCase("1234")]
        [TestCase("123456789")]
        public void ApplyShouldInsertStringWithDifferentLength(string insertString)
        {
            var insertPosition = 3;
            state.CurrentPosition = insertPosition;
            command = new InsertStringCommand(insertString);
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.Text.ToString().Substring(insertPosition, insertString.Length).Should().BeEquivalentTo(insertString);
            state.CurrentPosition.Should().Be(insertPosition + insertString.Length);
        }
        
        [TestCase("1")]
        [TestCase("1234")]
        [TestCase("123456789")]
        public void RevertShouldRestoreStateWithDifferentLength(string insertString)
        {
            var insertPosition = 3;
            state.CurrentPosition = insertPosition;
            command = new InsertStringCommand(insertString);
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();
            state.Should().Be(oldState);
        }
    }
}