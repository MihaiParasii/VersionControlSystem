using Lab3.CommandResults;

namespace Lab3.Commands;

public interface ICommand
{
    public CommandResult Execute(string argument = "");
}