using System.Text;
using Lab3.Commands;

namespace Lab3.Exceptions;

public static class InvalidGitCommandError
{
    public static string GetError(Dictionary<string, ICommand> commands)
    {
        var sb = new StringBuilder();
        sb.Append("Invalid command. Supported commands:\n");

        foreach (string command in commands.Keys)
        {
            sb.Append($"\t\t{command}\n");
        }

        return sb.ToString();
    }
}
