using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab3.Repositories;

public class AppDbContext // TODO Remove this piece of unbelievably Indianly codish
{
    private static AppDbContext? _dbContext;
    private readonly IMongoCollection<Commit> _collection;
    private readonly IMongoDatabase _database;
    private const string ConnectionString = "mongodb://localhost";
    private const string DatabaseName = "FileStorage";
    private const string CollectionName = "Commits";
    // TODO creeate new collection where will be last accessed commit 
    public ObjectId? CurrentCommitId { get; private set; }

    private AppDbContext()
    {
        var mongoClient = new MongoClient(ConnectionString);
        _database = mongoClient.GetDatabase(DatabaseName);
        _collection = _database.GetCollection<Commit>(CollectionName);
        CurrentCommitId = GetLastCommitAsync().GetAwaiter().GetResult()?.Id;
    }

    public static AppDbContext GetInstance() => _dbContext ??= new AppDbContext();

    public async Task<Commit?> GetLastCommitAsync()
    {
        var commits = await GetCommitsAsync();
        return commits.Count == 0 ? null : commits[^1];
    }

    public async Task<List<Commit>> GetCommitsAsync()
    {
        return (await _collection.FindAsync(FilterDefinition<Commit>.Empty)).ToList();
    }

    public async Task<Commit?> GetCurrentCommitAsync()
    {
        return (await _collection.FindAsync(c => c.Id == CurrentCommitId)).FirstOrDefault();
    }

    public async Task<Commit?> GetCommitByIdAsync(string id)
    {
        return (await _collection.FindAsync(c => c.Id.ToString() == id)).FirstOrDefault();
    }

    public void ChangeCurrentCommit(ObjectId id)
    {
        CurrentCommitId = id;
    }

    public async Task AddCommitAsync(Commit commit)
    {
        await _collection.InsertOneAsync(commit);
        CurrentCommitId = commit.Id;
    }

    public async Task DropCommitsAsync() => await _database.DropCollectionAsync(CollectionName);
}
