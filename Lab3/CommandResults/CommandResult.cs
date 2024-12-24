namespace Lab3.CommandResults;

public abstract class CommandResult(string message = "", ResultStatus status = ResultStatus.Ok)
{
    public readonly ResultStatus Status = status;
    public readonly string Message = message;
}

public sealed class BadCommandResult(string message = "") : CommandResult(message, ResultStatus.Bad);

public class ExitCommandResult(string message = "") : CommandResult(message, ResultStatus.FileNotFound);

public sealed class GoodCommandResult(string message = "") : CommandResult(message);
