using Entities.Entities;
using Entities.Entities.DTOs;

namespace ApplicationCore.Interfaces;

public interface IUploadService
{
    public Task<List<Issue>> UploadAll(HttpClient client);

    public Task<List<Issue>> ParseIssues(List<IssueIdData> idList, HttpClient client);

    public Task<List<Issue>> UploadNew(HttpClient client);
}