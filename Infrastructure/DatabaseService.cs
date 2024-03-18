using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskStorage.Entities;
using TaskStorage.Interfaces;

namespace TaskStorage;

public class DatabaseService : IDatabaseService
{
    private readonly IMongoCollection<Issue> _issuesCollection;

    public DatabaseService(IOptions<ConnectionStrings> connectionStrings)
    {
        var mongoClient = new MongoClient(connectionStrings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            connectionStrings.Value.DatabaseName);

        _issuesCollection = mongoDatabase.GetCollection<Issue>(
            connectionStrings.Value.CollectionName);
    }

    public async Task CreateOrUpdateAsync(Issue issue)
    {
        var isExist =  await _issuesCollection.FindAsync(x => x.Id == issue.Id).Result.AnyAsync();
        if (isExist)
        {
            await _issuesCollection.ReplaceOneAsync(x => x.Id == issue.Id, issue);
        }
        else
        {
            await _issuesCollection.InsertOneAsync(issue);
        }
    }

    public async Task CreateAsync(Issue newIssue) =>
        await _issuesCollection.InsertOneAsync(newIssue);

    public async Task UpdateAsync(Issue updatedIssue)
    {
        var id = updatedIssue.Id;
        await _issuesCollection.ReplaceOneAsync(x => x.Id == id, updatedIssue);
    }

    public async Task InsertManyAsync(List<Issue> issues)
    {
        await _issuesCollection.InsertManyAsync(issues);
    }
}