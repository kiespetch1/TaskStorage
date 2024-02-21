using TaskStorage.Entities;
using TaskStorage.Interfaces;
using TaskStorage.Jobs;
using TaskStorage.Utils;

namespace TaskStorage.Services;

public class StorageService : IStorageService
{
    private readonly YouTrackHttpClient _client;
    private readonly IDatabaseService _ctx;
    private readonly IConfiguration _configuration;

    public StorageService(IDatabaseService ctx, YouTrackHttpClient client, IConfiguration configuration)
    {
        _ctx = ctx;
        _client = client;
        _configuration = configuration;
    }

    /// <inheritdoc cref="IStorageService.StoreNewIssues()"/>
    public async Task StoreNewIssues()
    {
        var service = new UploadService(_client);
        List<Issue> issues;
        
        IEnumerable<Task> tasks;
        if (GlobalVariables.LastDbUpdateTime == new DateTime(1, 1, 1))
        {
            issues = await service.UploadAll();
            tasks = issues.Select(async entry =>
            {
                _ctx.CreateAsync(entry);
            });
            await UpdateScheduler.Start(_configuration);
        }
        else
        {
            issues = await service.UploadNew();
            tasks = issues.Select(async entry =>
            {
                _ctx.UpdateAsync(entry);
            });
        }
        
        await Task.WhenAll(tasks);
        
        GlobalVariables.LastDbUpdateTime = DateTime.UtcNow;
    }
}