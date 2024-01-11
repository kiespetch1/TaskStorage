using TaskStorage.Controllers.Entities;

namespace TaskStorage.Interfaces;

public interface IUploadService
{
    public Task<List<Issue>> Upload(HttpClient client);
}