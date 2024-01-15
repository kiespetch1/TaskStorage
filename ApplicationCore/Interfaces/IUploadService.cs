using Entities.Entities;

namespace ApplicationCore.Interfaces;

public interface IUploadService
{
    public Task<List<Issue>> Upload(HttpClient client);
}