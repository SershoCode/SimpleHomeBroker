namespace SimpleHomeBroker.Application.CommandResults.HomePC
{
    public class ComputerCommandResult
    {
        public bool IsSuccess { get; }
        public string Comment { get; }

        public ComputerCommandResult(bool isSuccess, string comment = default)
        {
            IsSuccess = isSuccess;
            Comment = comment;
        }
    }
}