namespace ExtendableTextEditor.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

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
            this.state.CurrentPosition = position;

            var result = this.controller.ApplyCommand("insert", "1");

            result.IsSuccess.Should().BeTrue();
            this.state.Text[position].Should().Be('1');
            this.state.CurrentPosition.Should().Be(position + 1);
        }

        [TestCase("1")]
        [TestCase("1234")]
        [TestCase("123456789")]
        public void ApplyShouldInsertStringWithDifferentLength(string insertString)
        {
            var insertPosition = 3;
            this.state.CurrentPosition = insertPosition;

            var result = this.controller.ApplyCommand("insert", insertString);

            result.IsSuccess.Should().BeTrue();
            this.state.Text.ToString().Substring(insertPosition, insertString.Length).Should()
                .BeEquivalentTo(insertString);
            this.state.CurrentPosition.Should().Be(insertPosition + insertString.Length);
        }
    }
}
