using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextEditor.Tests
{
    [TestFixture]
    public class EditController_Tests
    {
        private EditController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new EditController(new EditArea());
        }

        [Test]
        public void ApplyCommandWhenResultStateIsValidShouldApplyCommand()
        {
            var command = new InsertCharCommand('a');

            var result = controller.ApplyCommand(command);

            result.Should().BeTrue();
            controller.Text.Should().BeEquivalentTo("a");
            controller.CurrentPosition.Should().Be(1);
        }
        
        [Test]
        public void ApplyCommandWhenCommandCantBeAppliedShouldReturnFalse()
        {
            var command = new DeleteCharCommand();

            var result = controller.ApplyCommand(command);

            result.Should().BeFalse();
            controller.Text.Should().BeEquivalentTo(string.Empty);
            controller.CurrentPosition.Should().Be(0);
        }
        
        [Test]
        public void ApplyCommandWhenStateInvalidShouldRevertCommand()
        {
            var command = new MoveCursorCommand(-5);

            controller.ApplyCommand(command);

            controller.Text.Should().BeEquivalentTo(string.Empty);
            controller.CurrentPosition.Should().Be(0);
        }
        
        [Test]
        public void ApplyCommandShouldClearUndoneCommands()
        {
            var applyResult = controller.ApplyCommand(new InsertCharCommand('a'));
            applyResult.Should().BeTrue();
            var undoResult = controller.Undo();
            undoResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new InsertCharCommand('b'));
            applyResult.Should().BeTrue();

            var result = controller.Redo();

            result.Should().BeFalse();
            controller.Text.Should().BeEquivalentTo("b");
            controller.CurrentPosition.Should().Be(1);
        }
        
        [Test]
        public void UndoShouldUndoOneCommand()
        {
            var applyResult = controller.ApplyCommand(new InsertCharCommand('a'));
            applyResult.Should().BeTrue();

            var result = controller.Undo();

            result.Should().BeTrue();
            controller.Text.Should().BeEquivalentTo(string.Empty);
            controller.CurrentPosition.Should().Be(0);
        }
        
        [Test]
        public void UndoShouldUndoSeveralCommands()
        {
            var applyResult = controller.ApplyCommand(new InsertCharCommand('a'));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new MoveCursorCommand(-1));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new InsertCharCommand('b'));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new DeleteCharCommand());
            applyResult.Should().BeTrue();

            var result = controller.Undo();
            result.Should().BeTrue();
            result = controller.Undo();
            result.Should().BeTrue();
            result = controller.Undo();
            result.Should().BeTrue();
            result = controller.Undo();
            result.Should().BeTrue();

            controller.Text.Should().BeEquivalentTo(string.Empty);
            controller.CurrentPosition.Should().Be(0);
        }
        
        [Test]
        public void UndoWhenNoCommandsShouldShouldReturnFalse()
        {
            var result = controller.Undo();

            result.Should().BeFalse();
        }
        
        [Test]
        public void RedoShouldRedoOneCommand()
        {
            var applyResult = controller.ApplyCommand(new InsertCharCommand('a'));
            applyResult.Should().BeTrue();
            var undoResult = controller.Undo();
            undoResult.Should().BeTrue();

            var result = controller.Redo();
            
            result.Should().BeTrue();
            controller.Text.Should().BeEquivalentTo("a");
            controller.CurrentPosition.Should().Be(1);
        }
        
        [Test]
        public void RedoShouldRedoSeveralCommands()
        {
            var applyResult = controller.ApplyCommand(new InsertCharCommand('a'));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new MoveCursorCommand(-1));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new InsertCharCommand('b'));
            applyResult.Should().BeTrue();
            applyResult = controller.ApplyCommand(new DeleteCharCommand());
            applyResult.Should().BeTrue();

            var undoResult = controller.Undo();
            undoResult.Should().BeTrue();
            undoResult = controller.Undo();
            undoResult.Should().BeTrue();
            undoResult = controller.Undo();
            undoResult.Should().BeTrue();
            undoResult = controller.Undo();
            undoResult.Should().BeTrue();
            
            
            var result = controller.Redo();
            result.Should().BeTrue();
            result = controller.Redo();
            result.Should().BeTrue();
            result = controller.Redo();
            result.Should().BeTrue();

            controller.Text.Should().BeEquivalentTo("ba");
            controller.CurrentPosition.Should().Be(1);
        }
        
        [Test]
        public void RedoWhenNoCommandsShouldShouldReturnFalse()
        {
            var result = controller.Redo();

            result.Should().BeFalse();
        }
    }
}