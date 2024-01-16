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
            Converters = { new Converters.CommentConverter(), new Converters.CustomFieldsConverter() },
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        var ids = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        var issues = new List<Issue>();
        var customFields = new List<CustomFieldInfo>();
        const string queryUrl =
            "?fields=id,summary,description,comments(text,author(login,fullName)),assignee,type,state,priority," +
            "spentTime,customFields(name,value($type,value,login,ordinal(name),minutes))";
        
        foreach (var a in ids)
        {
            using var issueResponse = await client.GetAsync(a.Id + queryUrl);
            var jsonIssueData = await issueResponse.Content.ReadAsStringAsync();
            
            if (jsonIssueData != null)
            {
                customFields = JsonConvert.DeserializeObject<List<CustomFieldInfo>>(jsonIssueData, settings);
                issues.Add(JsonConvert.DeserializeObject<Issue>(jsonIssueData, settings).AddCustomParameters(customFields));
            }
        }
        
        return issues;
    }
}