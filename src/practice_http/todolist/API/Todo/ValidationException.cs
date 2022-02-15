namespace API.Todo
{
    using System;

    public sealed class ValidationException : Exception
    {
        public ValidationException(string error = "Validation failed") : base(error)
        {
        }
    }
}
