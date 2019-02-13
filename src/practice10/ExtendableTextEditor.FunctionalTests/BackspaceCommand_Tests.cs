namespace ExtendableTextEditor.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

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
            this.state.CurrentPosition = position;
            var oldState = new ControllerState(this.state);

            var result = this.controller.ApplyCommand("backspace");

            result.IsSuccess.Should().BeTrue();
            this.state.Text.ToString().Should().BeEquivalentTo(oldState.Text.Remove(position - 1, 1).ToString());
            this.state.CurrentPosition.Should().Be(position - 1);
        }
    }
}
