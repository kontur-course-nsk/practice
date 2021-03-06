﻿namespace ExtendableTextEditor
{
    using System;
    using System.Linq;

    internal class Program
    {
        private static EditController controller;
        private static GlobalEditorSettings settings;

        public static void Main(string[] args)
        {
            settings = new GlobalEditorSettings
            {
                ThrowExceptionIfCommandNotFound = false,
                Theme = "darkula"
            };
            controller = new EditController(settings.ThrowExceptionIfCommandNotFound);
            InteractiveMode();
        }

        private static void InteractiveMode()
        {
            Console.WriteLine("Enter command. 'exit' for close, 'state' for currant state.");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                    continue;
                switch (input)
                {
                    case "exit":
                        return;
                    case "state":
                        Console.WriteLine($"Text: $'{controller.Text}', position: {controller.CurrentPosition}");
                        break;
                    default:
                        var command = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        var (isSuccess, errorMessage) = controller.ApplyCommand(command[0], command.Skip(1).ToArray());
                        if (!isSuccess)
                            Console.WriteLine($"Can't apply command: {errorMessage ?? string.Empty}");
                        break;
                }
            }
        }

        private static void Simulate()
        {
            controller.ApplyCommand("insert", "hello");
            controller.ApplyCommand("backspace");
            controller.ApplyCommand("backspace");
        }
    }
}
