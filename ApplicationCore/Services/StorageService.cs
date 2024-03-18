using TaskStorage.Entities;
using TaskStorage.Interfaces;
using TaskStorage.Jobs;
using TaskStorage.Utils;

namespace TaskStorage.Services;

public class StorageService : IStorageService
{
    private readonly IDatabaseService _ctx;
    private readonly IConfiguration _configuration;
    private readonly IUploadService _service;

    public StorageService(IDatabaseService ctx, IConfiguration configuration, IUploadService service)
    {
        _ctx = ctx;
        _configuration = configuration;
        _service = service;
    }

    /// <inheritdoc cref="IStorageService.StoreNewIssues()"/>
    public async Task StoreNewIssues()
    {
        List<Issue> issues;

        if (GlobalVariables.LastDbUpdateTime == new DateTime(1, 1, 1))
        {
            issues = await _service.UploadNew();
            await _ctx.InsertManyAsync(issues);
            await UpdateScheduler.Start(_configuration);
        }
        else
        {
            issues = await _service.UploadNew();
            var tasks = issues.Select(async entry => { await _ctx.CreateOrUpdateAsync(entry); });
            await Task.WhenAll(tasks);
        }
        GlobalVariables.LastDbUpdateTime = DateTime.UtcNow;
    }
}