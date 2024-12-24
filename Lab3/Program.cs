using Lab3.CommandResults;

namespace Lab3;

internal static class Program
{
    private static bool _isRunning = true;
    private static readonly ConsoleCommandExecutor ConsoleCommandExecutor = ConsoleCommandExecutor.GetInstance();
    private static readonly GitEngine GitEngine = new GitEngineV2();

    public static void Main()
    {
        while (_isRunning)
        {
            Console.Write("Enter command: ");
            string input = Console.ReadLine()!;

            if (GitEngine.IsGitCommand(input))
            {
                GitInputResult gitInputResult = GitEngine.ParseCommand(input);


                CommandResult result = GitEngine.ExecuteCommand(gitInputResult);
                Console.WriteLine(result.Message);

                if (result is ExitCommandResult)
                {
                    _isRunning = false;
                    break;
                }

                continue;
                // ConsoleExceptionThrower.Throw();
            }

            Console.WriteLine(ConsoleCommandExecutor.Execute(input));
        }
    }
}
