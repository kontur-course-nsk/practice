using ExtendableTextEditor;

namespace TestEditorPlugin
{
    public static class Commands
    {
        public static (bool, string) Backspace(IControllerState state, string[] args)
        {
            if (state.CurrentPosition <= 0)
                return (false, "No symbols on left side");
            state.CurrentPosition--;
            state.Text.Remove(state.CurrentPosition, 1);
            return (true, null);
        }
    }
}