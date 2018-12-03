namespace TextEditor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var controller = new EditController(new EditArea());
            controller.ApplyCommand(new InsertCharCommand('a'));
            controller.ApplyCommand(new InsertCharCommand('b'));
            controller.ApplyCommand(new InsertCharCommand('c'));
            controller.ApplyCommand(new MoveCursorCommand(-2));
            controller.ApplyCommand(new InsertCharCommand('d'));
            controller.ApplyCommand(new InsertCharCommand('e'));
            controller.ApplyCommand(new DeleteCharCommand());
            controller.ApplyCommand(new DeleteCharCommand());
        }
    }
}