using MongoDB.Driver;
using TaskStorage.Entities;

namespace TaskStorage.Utils;

public interface IDatabaseContext
{
    public Task<List<Issue>> GetAsync();

    public Task<Issue?> GetAsync(string id);

    public Task CreateAsync(Issue newIssue);

    public Task UpdateAsync(Issue updatedIssue);

    public Task RemoveAsync(string id);
}