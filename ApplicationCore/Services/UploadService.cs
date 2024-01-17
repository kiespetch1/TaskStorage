using ApplicationCore.Converters;
using ApplicationCore.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Entities.Entities;
using Entities.Entities.DTOs;
using static ApplicationCore.Utils.FieldsMapping;

namespace ApplicationCore.Services;

public class UploadService : IUploadService
{
    public async Task<List<Issue>> Upload(HttpClient client)
    {
        using var response = await client.GetAsync("");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var settings = new JsonSerializerSettings
        {
            Converters = { new CommentConverter(), new CustomFieldsConverter(), new WorkLogConverter() },
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        var ids = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        var issues = new List<Issue>();
        var customFields = new List<CustomFieldInfo>();
        const string issueQueryUrl =
            "?fields=id,idReadable,summary,description,comments(text,author(login,fullName)),assignee,type,state," +
            "priority,spentTime,customFields(name,value($type,value,login,ordinal(name),minutes))";

        const string workLogQueryUrl = "/timeTracking/workItems?fields=author(login),creator(login)," +
                                       "duration(id,minutes),text,date";

        foreach (var a in ids)
        {
            using var issueResponse = await client.GetAsync(a.Id + issueQueryUrl);
            var jsonIssueData = await issueResponse.Content.ReadAsStringAsync();

            using var workLogResponse = await client.GetAsync(a.Id + workLogQueryUrl);
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