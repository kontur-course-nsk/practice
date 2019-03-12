namespace ExtendableTextEditor
{
    public class CommandResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public CommandResult()
        {
              
        }

        public CommandResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}