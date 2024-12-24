namespace Lab3;

public class FileRecord
{
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public DateTime CreationTime { get; set; }
    public byte[] Content { get; set; } = [];


    public bool Equals(FileRecord? record)
    {
        if (record is null)
        {
            return false;
        }

        return ReferenceEquals(this, record) || EqualityComparer<FileRecord>.Default.Equals(this, record);
    }
}

public class ImageFileRecord : FileRecord
{
    public int Width { get; set; }
    public int Height { get; set; }
}

public class TextFileRecord : FileRecord
{
    public int LineCount { get; set; }
    public int WordsCount { get; set; }
    public int CharsCount { get; set; }
}

public class ProgramFileRecord : FileRecord
{
    public string Language { get; set; } = string.Empty;
    public int LinesCount { get; set; }
    public int ClassesCount { get; set; }
    public int MethodsCount { get; set; }
}
