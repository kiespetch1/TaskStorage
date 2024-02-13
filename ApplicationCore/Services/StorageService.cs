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

        if (GlobalVariables.LastDbUpdateTime == new DateTime(1, 1, 1))
        {
            await _ctx.Issues.AddRangeAsync(issues);
            await _ctx.SaveChangesAsync();
        }
        else
        {
            _ctx.Issues.UpdateRange(issues);
            await _ctx.SaveChangesAsync();
        }


        GlobalVariables.LastDbUpdateTime = DateTime.UtcNow;
    }
}