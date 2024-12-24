using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lab3;

public class Commit
{
    [BsonId] public ObjectId Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedTime { get; init; }
    public List<FileRecord> Files { get; set; } = [];

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("-----------------------------------\n");
        sb.Append($"Commit ID: {Id}\n");
        sb.Append($"Description: {Description}\n");
        sb.Append($"Timestamp: {CreatedTime}\n");
        sb.Append($"Files in commit: {Files.Count}\n");
        sb.Append("-----------------------------------\n");

        return sb.ToString();
    }
}
