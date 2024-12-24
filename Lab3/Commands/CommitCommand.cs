using Lab3.CommandResults;
using Lab3.Repositories;

namespace Lab3.Commands;

public class CommitCommand : BaseCommand
{
    private readonly AppDbContext _appDbContext = AppDbContext.GetInstance();

    public override CommandResult Execute(string argument = "")
    {
        try
        {
            var commit = ReadCommit();
            commit.Description = argument;

            _appDbContext.AddCommitAsync(commit).GetAwaiter().GetResult();

            return new GoodCommandResult();
        }
        catch (Exception ex)
        {
            return new BadCommandResult(ex.Message);
        }
    }
}
