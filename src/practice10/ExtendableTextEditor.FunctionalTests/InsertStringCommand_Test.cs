using FluentAssertions;
using NUnit.Framework;

namespace ExtendableTextEditor.FunctionalTests
{
    [TestFixture]
    public class InsertStringCommand_Test : BaseFixture
    {
        protected override void SetupActions()
        {
            // TODO: зарегистрировать команду в контроллере
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        public void ApplyShouldInsertStringInDifferentPositions(int position)
        {
            state.CurrentPosition = position;
            
            var result = controller.ApplyCommand("insert", "1");

            result.IsSuccess.Should().BeTrue();
            state.Text[position].Should().Be('1');
            state.CurrentPosition.Should().Be(position + 1);
        }
        
        [TestCase("1")]
        [TestCase("1234")]
        [TestCase("123456789")]
        public void ApplyShouldInsertStringWithDifferentLength(string insertString)
        {
            var insertPosition = 3;
            state.CurrentPosition = insertPosition;
            
            var result = controller.ApplyCommand("insert", insertString);

            result.IsSuccess.Should().BeTrue();
            state.Text.ToString().Substring(insertPosition, insertString.Length).Should().BeEquivalentTo(insertString);
            state.CurrentPosition.Should().Be(insertPosition + insertString.Length);
        }
    }
}