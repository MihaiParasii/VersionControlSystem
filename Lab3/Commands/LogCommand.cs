using Lab3.CommandResults;
using Lab3.Repositories;
using MongoDB.Bson;

namespace Lab3.Commands;

public class LogCommand : ICommand
{
    private readonly AppDbContext _dbContext = AppDbContext.GetInstance();

    public CommandResult Execute(string argument = "")
    {
        var commits = _dbContext.GetCommitsAsync().GetAwaiter().GetResult();
        
        return new LogCommandResult(commits, _dbContext.CurrentCommitId ?? new ObjectId());
    }
}
