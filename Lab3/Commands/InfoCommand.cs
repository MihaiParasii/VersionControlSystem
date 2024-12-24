using Lab3.CommandResults;
using Lab3.Exceptions;

namespace Lab3.Commands;

public class InfoCommand : ICommand
{
    private readonly ConsoleCommandExecutor _commandExecutor = ConsoleCommandExecutor.GetInstance();

    public CommandResult Execute(string argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            ConsoleExceptionThrower.Throw(EmptyArgumentError.GetError());
        }
        
        string command = $"stat \"\"{argument}\"\"";

        string result = _commandExecutor.Execute(command);

        if (string.IsNullOrWhiteSpace(result))
        {
            return new BadCommandResult($"No file or directory found at '{argument}'");
        }

        return new GoodCommandResult(result);
    }
}
