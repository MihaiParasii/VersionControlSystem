using System.Text;
using MongoDB.Bson;

namespace Lab3.CommandResults;

public class LogCommandResult(List<Commit> commits, ObjectId currentCommitId)
    : CommandResult(ConvertToString(commits, currentCommitId))
{
    private static string ConvertToString(List<Commit> commits, ObjectId currentCommitId)
    {
        var sb = new StringBuilder();

        foreach (var commit in commits)
        {
            sb.AppendLine(
                $"\t — Commit ID: {commit.Id} {(currentCommitId == commit.Id ? "<<<———|" : "")}\n\t\t Message: {commit.Description}\n\t\t Commit Time: {commit.CreatedTime}\n");
        }

        return sb.ToString();
    }
}
