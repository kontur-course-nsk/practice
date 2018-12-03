using FluentAssertions;
using NUnit.Framework;

namespace TextEditor.Tests
{
    [TestFixture]
    public class MoveCursorCommand_Tests : OperationFixtureBase
    {
        [TestCase(-3)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(+1)]
        [TestCase(+4)]
        public void ApplyShouldMoveCursorToDifferentDistances(int targetPosition)
        {
            var oldPosition = state.CurrentPosition;
            command = new MoveCursorCommand(targetPosition);
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.CurrentPosition.Should().Be(oldPosition + targetPosition);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void ApplyShouldMoveCursorFromDifferentPositions(int startPosition)
        {
            state.CurrentPosition = startPosition;
            command = new MoveCursorCommand(1);
            
            var result = command.Apply(state);

            result.Should().BeTrue();
            state.CurrentPosition.Should().Be(startPosition + 1);
        }
        
        [TestCase(-3)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(+1)]
        [TestCase(+4)]
        public void RevertShouldRestoreStateFromDifferentDistances(int targetPosition)
        {
            command = new MoveCursorCommand(targetPosition);
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();
            state.Should().Be(oldState);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void RevertShouldRestoreStateFromDifferentPositions(int startPosition)
        {
            state.CurrentPosition = startPosition;
            command = new MoveCursorCommand(1);
            var oldState = new ControllerState(state);
            var applyResult = command.Apply(state);
            applyResult.Should().BeTrue();
            
            var result = command.Revert(state);

            result.Should().BeTrue();

            state.Should().Be(oldState);
        }
    }
}