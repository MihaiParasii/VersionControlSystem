using System.Diagnostics;
using Lab3.Exceptions;

namespace Lab3;

public class ConsoleCommandExecutor
{
    private static ConsoleCommandExecutor? _instance;

    // private static string _currentDirectory = "/Users/admin/Downloads/UTM/SEM3/OOP/Lab3/";
    // private static string _currentDirectory = "/Users/admin/Downloads/";
    private string _currentDirectory = "/Users/admin/Downloads/test_folder";
    private readonly string _homeDirectory;

    private ConsoleCommandExecutor()
    {
        _homeDirectory = _currentDirectory;
    }

    public static ConsoleCommandExecutor GetInstance()
    {
        return _instance ??= new ConsoleCommandExecutor();
    }

    public string Execute(string command)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = "-c \"cd '" + _currentDirectory + "' && " + command + "\""
        };

        using var process = new Process();

        process.StartInfo = processInfo;
        process.Start();

        string result = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        if (command.StartsWith("cd "))
        {
            ChangeDirectory(command);
        }

        return result;
    }

    private void ChangeDirectory(string command)
    {
        try
        {
            string targetDirectory = command[3..].Trim();

            if (string.IsNullOrWhiteSpace(targetDirectory))
            {
                return;
            }

            if (targetDirectory == "..")
            {
                _currentDirectory = Directory.GetParent(_currentDirectory)?.FullName ?? _currentDirectory;
            }
            else if (Path.IsPathRooted(targetDirectory) || targetDirectory.StartsWith('/'))
            {
                _currentDirectory = targetDirectory;
            }
            else
            {
                _currentDirectory = Path.Combine(_currentDirectory, targetDirectory);
            }

            if (Directory.Exists(_currentDirectory))
            {
                return;
            }

            _currentDirectory = _homeDirectory;
        }
        catch (Exception ex)
        {
            ConsoleExceptionThrower.Throw(ex.Message);
        }
    }

    public IEnumerable<string> GetFiles() => Directory.GetFiles(_currentDirectory);
}
