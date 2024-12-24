using System.Text;

namespace Lab3.CommandResults;

public class StatusCommandResult(IEnumerable<FileStatus> statusResults) : CommandResult(ConvertToString(statusResults))
{
    private static string ConvertToString(IEnumerable<FileStatus> statusResults)
    {
        var sb = new StringBuilder();

        foreach (FileStatus fileStatus in statusResults)
        {
            sb.AppendLine($"\t — {fileStatus.FileName} — {fileStatus.Status}");
        }

        return sb.ToString();
    }
}
