namespace ExtendableTextEditor
{
    using System;

    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string commandName)
            : base($"Command '{commandName}' not found")
        {
        }
    }
}
