using Quartz;
using TaskStorage.Interfaces;

namespace TaskStorage.Jobs;

/// <summary>
/// Выполняет обновление базы данных.
/// </summary>
public class StorageUpdater : IJob
{
    public readonly IStorageService _service;
    
    public StorageUpdater(IStorageService service)
    {
        _service = service;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _service.StoreNewIssues();
    }
}