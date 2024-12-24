using Lab3.CommandResults;

namespace Lab3.Commands;

public abstract class BaseCommand : ICommand
{
    private readonly ConsoleCommandExecutor _commandExecutor = ConsoleCommandExecutor.GetInstance();
    public abstract CommandResult Execute(string argument = "");

    protected Commit ReadCommit()
    {
        var commit = new Commit
        {
            CreatedTime = DateTime.UtcNow
        };

        foreach (string filePath in _commandExecutor.GetFiles())
        {
            string name = Path.GetFileName(filePath);
            byte[] content = File.ReadAllBytes(filePath);
            DateTime creationTime = File.GetCreationTime(filePath);

            commit.Files.Add(new FileRecord
            {
                FileName = name,
                FilePath = filePath,
                CreationTime = creationTime,
                Content = content
            });
        }

        return commit;
    }
}
