using ApplicationCore.Converters;
using ApplicationCore.Interfaces;
using ApplicationCore.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Entities.Entities;
using Entities.Entities.DTOs;

namespace ApplicationCore.Services;

public class UploadService : IUploadService
{
    JsonSerializerSettings settings = new()
    {
        Converters = { new CommentConverter(), new CustomFieldsConverter(), new WorkLogConverter() },
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };
    public async Task<List<Issue>> UploadAll(HttpClient client)
    {
        using var response = await client.GetAsync("");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        var idList = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        return await ParseIssues(idList, client);
    }

    public async Task<List<Issue>> UploadNew(HttpClient client)
    {
        using var response = await client.GetAsync("?filter=updated: {date from=" 
                                                   + GlobalVariables.LastDbUpdateTime + "}..{current date}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var idList = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        
        return await ParseIssues(idList, client);
    }

    public async Task<List<Issue>> ParseIssues(List<IssueIdData> idList, HttpClient client)
    {
        var issues = new List<Issue>();
        var customFields = new List<CustomFieldInfo>();
        const string issueQueryUrl =
            "?fields=id,idReadable,summary,description,comments(text,author(login,fullName)),assignee,type,state," +
            "priority,spentTime,customFields(name,value($type,value,login,ordinal(name),minutes))";

        const string workLogQueryUrl = "/timeTracking/workItems?fields=author(login),creator(login)," +
                                       "duration(id,minutes),text,date";

        foreach (var entry in idList)
        {
            using var issueResponse = await client.GetAsync(entry.Id + issueQueryUrl);
            var jsonIssueData = await issueResponse.Content.ReadAsStringAsync();

            using var workLogResponse = await client.GetAsync(entry.Id + workLogQueryUrl);
            var jsonWorkLogData = await workLogResponse.Content.ReadAsStringAsync();

            if (jsonIssueData == null) continue;
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
        }
        
        return issues;
    }
}