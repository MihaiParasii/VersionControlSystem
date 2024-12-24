using Lab3.CommandResults;
using Lab3.Repositories;

namespace Lab3.Commands;

public class StatusCommand : BaseCommand
{
    private readonly AppDbContext _appDbContext = AppDbContext.GetInstance();

    public override CommandResult Execute(string argument = "")
    {
        Commit? lastCommit = _appDbContext.GetCurrentCommitAsync().GetAwaiter().GetResult();
        Commit currentCommit = ReadCommit();

        if (lastCommit == null)
        {
            return new GoodCommandResult("No previous commit found.");
        }

        var differences = GetDifferences(lastCommit, currentCommit).ToList();

        if (differences.Count == 0)
        {
            return new GoodCommandResult("No differences found.");
        }

        return new StatusCommandResult(differences);
    }

    private static IEnumerable<FileStatus> GetDifferences(Commit prevCommit, Commit currentCommit)
    {
        var prevFiles = prevCommit.Files;
        var currentFiles = currentCommit.Files;


        IList<FileStatus> filesStatuses = [];

        foreach (FileRecord file in currentFiles)
        {
            var prevFile = prevFiles.FirstOrDefault(f => f.FileName == file.FileName);

            if (prevFile == null)
            {
                filesStatuses.Add(new AddedFileStatus(file.FileName));
            }
            else if (!IsContentEqual(prevFile.Content, file.Content))
            {
                filesStatuses.Add(new ModifiedFileStatus(file.FileName));
            }
            else
            {
                filesStatuses.Add(new UnchangedFileStatus(file.FileName));
            }

            if (prevFiles.Count == 0 && currentFiles.Count == 0)
            {
                break;
            }

            prevFiles.Remove(prevFile ?? null!);
        }


        if (prevFiles.Count > 0)
        {
            foreach (FileRecord prevFile in prevFiles)
            {
                filesStatuses.Add(new DeletedFileStatus(prevFile.FileName));
            }
        }

        return filesStatuses;
    }

    private static bool IsContentEqual(IReadOnlyCollection<byte> content1, IReadOnlyList<byte> content2)
    {
        if (content1.Count != content2.Count)
        {
            return false;
        }

        return !content1.Where((t, i) => t != content2[i]).Any();
    }
}
