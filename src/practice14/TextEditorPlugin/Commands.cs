using ExtendableTextEditor;

namespace TestEditorPlugin
{
    public static class Commands
    {
        public static CommandResult Backspace(IControllerState state, string[] args)
        {
            if (state.CurrentPosition <= 0)
                return new CommandResult(false, "No symbols on left side");
            state.CurrentPosition--;
            state.Text.Remove(state.CurrentPosition, 1);
            return new CommandResult(true, null);
        }

        public static int RandomMethod()
        {
            return 0;
        }
    }
}