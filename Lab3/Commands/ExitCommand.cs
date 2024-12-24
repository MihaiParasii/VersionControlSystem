using Lab3.CommandResults;

namespace Lab3.Commands;

public class ExitCommand : ICommand
{
    public CommandResult Execute(string argument = "")
    {
        return new ExitCommandResult("Program is quitting... Bye :)");
    }
}
