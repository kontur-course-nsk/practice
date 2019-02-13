namespace ExtendableTextEditor.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class SettingsTests : BaseFixture
    {
        [Test]
        public void ShouldBeHot()
        {
            var (isSuccess, errorMessage) = this.controller.ApplyCommand("some_command");
            isSuccess.Should().BeFalse();

            this.settings.ThrowExceptionIfCommandNotFound = true;
            Action action = () => { this.controller.ApplyCommand("some_command"); };
            action.Should().Throw<CommandNotFoundException>();
        }
    }
}
