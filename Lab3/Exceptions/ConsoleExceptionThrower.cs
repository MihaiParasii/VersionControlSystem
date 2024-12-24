namespace Lab3.Exceptions;

public static class ConsoleExceptionThrower
{
    public static void Throw(string message = "")
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(
            $"Error: Unable to execute command.\n{(message != string.Empty ? $"Error message:\n\t{message}" : "")}");
        Console.ResetColor();
        Console.WriteLine();
    }
}
