namespace Lab3;

public abstract class FileStatus
{
    public abstract string FileName { get; set; }
    public abstract string Status { get; set; }
    public abstract ConsoleColor TextColor { get; set; }
}

public class ModifiedFileStatus(string fileName) : FileStatus
{
    public override string FileName { get; set; } = fileName;
    public override string Status { get; set; } = "Modified";
    public override ConsoleColor TextColor { get; set; } = ConsoleColor.Blue;
}

public class AddedFileStatus(string fileName) : FileStatus
{
    public override string FileName { get; set; } = fileName;
    public override string Status { get; set; } = "Added";
    public override ConsoleColor TextColor { get; set; } = ConsoleColor.Green;
}

public class DeletedFileStatus(string fileName) : FileStatus
{
    public override string FileName { get; set; } = fileName;
    public override string Status { get; set; } = "Deleted";
    public override ConsoleColor TextColor { get; set; } = ConsoleColor.Red;
}

public class UnchangedFileStatus(string fileName) : FileStatus
{
    public override string FileName { get; set; } = fileName;
    public override string Status { get; set; } = "Unchanged";
    public override ConsoleColor TextColor { get; set; } = ConsoleColor.White;
}
