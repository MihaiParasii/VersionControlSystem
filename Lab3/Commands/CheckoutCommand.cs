using Lab3.CommandResults;
using Lab3.Repositories;

namespace Lab3.Commands;

public class CheckoutCommand : ICommand
{
    private readonly AppDbContext _dbContext = AppDbContext.GetInstance();
    private readonly ConsoleCommandExecutor _commandExecutor = ConsoleCommandExecutor.GetInstance();


    public CommandResult Execute(string argument)
    {
        Commit? commit = _dbContext.GetCommitByIdAsync(argument).GetAwaiter().GetResult();

        if (commit == null)
        {
            return new BadCommandResult("Commit not found.");
        }

        DeleteFiles();
        WriteCommit(commit);
        _dbContext.ChangeCurrentCommit(commit.Id);

        return new GoodCommandResult($"Successfully checked out commit {argument}");
    }


    private static void WriteCommit(Commit commit)
    {
        foreach (FileRecord commitFile in commit.Files)
        {
            File.WriteAllBytes(commitFile.FilePath, commitFile.Content);
        }
    }

    private void DeleteFiles()
    {
        foreach (string file in _commandExecutor.GetFiles())
        {
            File.Delete(file);
        }
    }
}
