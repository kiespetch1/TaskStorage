using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskStorage.Entities;
using TaskStorage.Services;
using TaskStorage.Utils;

namespace TaskStorage;

public class DatabaseService : IDatabaseContext
{
    private readonly IMongoCollection<Issue> _issuesCollection;

    public DatabaseService(IOptions<ConnectionStrings> connectionStrings)
    {
        var mongoClient = new MongoClient(
            (string)connectionStrings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            connectionStrings.Value.DatabaseName);

        _issuesCollection = mongoDatabase.GetCollection<Issue>(
            connectionStrings.Value.CollectionName);
    }

    public async Task<List<Issue>> GetAsync() =>
        await _issuesCollection.Find(_ => true).ToListAsync();

    public async Task<Issue?> GetAsync(string id) =>
        await _issuesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Issue newIssue) =>
        await _issuesCollection.InsertOneAsync(newIssue);

    public async Task UpdateAsync(Issue updatedIssue)
    {
        var id = updatedIssue.Id;
        await _issuesCollection.ReplaceOneAsync(x => x.Id == id, updatedIssue);
    }

    public async Task RemoveAsync(string id) =>
        await _issuesCollection.DeleteOneAsync(x => x.Id == id);
}