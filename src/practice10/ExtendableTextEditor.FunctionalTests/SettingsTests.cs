using System;
using FluentAssertions;
using NUnit.Framework;
using TextEditor;

namespace ExtendableTextEditor.FunctionalTests
{
    [TestFixture]
    public class SettingsTests : BaseFixture
    {
        protected override void SetupActions()
        {
            // TODO: зарегистрировать команду в контроллере
        }
        
        [Test]
        public void ShouldBeHot()
        {
            var (isSuccess, errorMessage) = controller.ApplyCommand("some_command");
            isSuccess.Should().BeFalse();

            settings.ThrowExceptionIfCommandNotFound = true;
            Action action = () => { controller.ApplyCommand("some_command"); };
            action.Should().Throw<CommandNotFoundException>();


        }
    }
}