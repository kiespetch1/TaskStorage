using System.Runtime.CompilerServices;
using TaskStorage.Interfaces;
using TaskStorage.Utils;

namespace TaskStorage.Services;

public class StorageService : IStorageService
{
    private readonly YouTrackHttpClient _client;
    private readonly IDatabaseContext _ctx;

    public StorageService(IDatabaseContext ctx, YouTrackHttpClient client)
    {
        _ctx = ctx;
        _client = client;
    }

    public async Task StoreNewIssues()
    {
        var service = new UploadService(_client);
        var issues = await service.UploadNew();
        
        IEnumerable<Task> tasks;
        if (GlobalVariables.LastDbUpdateTime == new DateTime(1, 1, 1))
        {
            tasks = issues.Select(async entry =>
            {
                _ctx.CreateAsync(entry);
            });
        }
        else
        {
            tasks = issues.Select(async entry =>
            {
                _ctx.UpdateAsync(entry);
            });
        }
        
        await Task.WhenAll(tasks);
        
        GlobalVariables.LastDbUpdateTime = DateTime.UtcNow;
    }
}