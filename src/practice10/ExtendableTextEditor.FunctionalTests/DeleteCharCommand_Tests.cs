namespace ExtendableTextEditor.Tests
{
    using FluentAssertions;
    using NUnit.Framework;

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
            this.state.CurrentPosition = position;
            var oldState = new ControllerState(this.state);

            var result = this.controller.ApplyCommand("delete");

            result.IsSuccess.Should().BeTrue();
            this.state.Text.ToString().Should().BeEquivalentTo(oldState.Text.Remove(position, 1).ToString());
            this.state.CurrentPosition.Should().Be(position);
        }
    }
}
