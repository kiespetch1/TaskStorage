using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaskStorage.Converters;
using TaskStorage.Entities;
using TaskStorage.Entities.Models;
using TaskStorage.Interfaces;
using TaskStorage.Utils;

namespace TaskStorage.Services;

public class UploadService : IUploadService
{
    private readonly HttpClient _client;

    public UploadService(YouTrackHttpClient youTrackHttpClient)
    {
        _client = youTrackHttpClient.GetClient();
    }
    
    JsonSerializerSettings settings = new()
    {
        Converters = { new CommentConverter(), new CustomFieldsConverter(), new WorkLogConverter() },
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };
    
    /// <inheritdoc cref="IUploadService.UploadAll()"/>
    public async Task<List<Issue>> UploadAll()
    {
        using var response = await _client.GetAsync("issues/");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        var idList = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        return await ParseIssues(idList);
    }

    /// <inheritdoc cref="IUploadService.UploadNew()"/>
    public async Task<List<Issue>> UploadNew()
    {
        using var response = await _client.GetAsync("issues?filter=updated: {date from=" 
                                                   + GlobalVariables.LastDbUpdateTime + "}..{current date}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var idList = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        
        return await ParseIssues(idList);
    }

    /// <inheritdoc cref="IUploadService.ParseIssues(System.Collections.Generic.List{TaskStorage.Entities.Models.IssueIdData})"/>
    public async Task<List<Issue>> ParseIssues(List<IssueIdData> idList)
    {
        var issues = new List<Issue>();
        var customFields = new List<CustomFieldInfo>();
        const string issueQueryUrl =
            "?fields=id,idReadable,summary,description,comments(text,author(login,fullName)),assignee,type,state," +
            "priority,spentTime,customFields(name,value($type,value,login,ordinal(name),minutes))";

        const string workLogQueryUrl = "/timeTracking/workItems?fields=author(login),creator(login)," +
                                       "duration(id,minutes),text,date";

        var tasks = idList.Select(async entry =>
        {
            using var issueResponse = await _client.GetAsync("issues/" + entry.Id + issueQueryUrl);
            var jsonIssueData = await issueResponse.Content.ReadAsStringAsync();

            using var workLogResponse = await _client.GetAsync("issues/" + entry.Id + workLogQueryUrl);
            var jsonWorkLogData = await workLogResponse.Content.ReadAsStringAsync();

            if (jsonIssueData == null) return;

            customFields = JsonConvert.DeserializeObject<List<CustomFieldInfo>>(jsonIssueData, settings);

            if (jsonWorkLogData != "[]")
            {
                var workLog = JsonConvert.DeserializeObject<List<WorkLogInfo>>(jsonWorkLogData, settings);
                issues.Add(JsonConvert.DeserializeObject<Issue>(jsonIssueData, settings)
                    .AddCustomParameters(customFields).AddWorkLogs(workLog));
            }
            else
            {
                issues.Add(JsonConvert.DeserializeObject<Issue>(jsonIssueData, settings)
                    .AddCustomParameters(customFields));
            }
        });

        await Task.WhenAll(tasks);
        
        return issues;
    }
}