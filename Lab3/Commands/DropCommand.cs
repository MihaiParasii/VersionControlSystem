using Lab3.CommandResults;
using Lab3.Repositories;

namespace Lab3.Commands;

public class DropCommand : ICommand
{
    private readonly AppDbContext _appDbContext = AppDbContext.GetInstance();

    public CommandResult Execute(string argument = "")
    {
        _appDbContext.DropCommitsAsync().GetAwaiter().GetResult();
        return new GoodCommandResult("Commits dropped successfully!");
    }
}
