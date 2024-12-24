using Lab3.Commands;

namespace Lab3;

public class GitEngineV2 : GitEngine
{
    public GitEngineV2()
    {
        AddNewCommand("log", new LogCommand());
        AddNewCommand("drop", new DropCommand());
        AddNewCommand("exit", new ExitCommand());
    }
}
