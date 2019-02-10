using System;

namespace ExtendableTextEditor
{
    public class CommandNotFoundException : Exception
    {

        public CommandNotFoundException(string commandName)
            : base($"Command '{commandName}' not found")
        {
        }
    }
}