using FluentAssertions;
using NUnit.Framework;

namespace ExtendableTextEditor.FunctionalTests
{
    [TestFixture]
    public class MoveCursorCommand_Tests : BaseFixture
    {
        protected override void SetupActions()
        {
            // TODO: зарегистрировать команду в контроллере
        }
        
        [TestCase(-3)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(+1)]
        [TestCase(+4)]
        public void ApplyShouldMoveCursorToDifferentDistances(int targetPosition)
        {
            var oldPosition = state.CurrentPosition;
            
            var result = controller.ApplyCommand("movecursor", targetPosition.ToString());

            result.IsSuccess.Should().BeTrue();
            state.CurrentPosition.Should().Be(oldPosition + targetPosition);
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(6)]
        public void ApplyShouldMoveCursorFromDifferentPositions(int startPosition)
        {
            state.CurrentPosition = startPosition;
            
            var result = controller.ApplyCommand("movecursor", 1.ToString());

            result.IsSuccess.Should().BeTrue();
            state.CurrentPosition.Should().Be(startPosition + 1);
        }
    }
}