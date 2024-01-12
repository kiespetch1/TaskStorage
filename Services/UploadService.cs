using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaskStorage.Controllers.Entities;
using TaskStorage.Interfaces;

namespace TaskStorage.Services;

public class UploadService : IUploadService
{
    
    public async Task<List<Issue>> Upload(HttpClient client)
    {
        using var response = await client.GetAsync("");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var settings = new JsonSerializerSettings
        {
            Converters = { new Issue.CommentConverter(), new Issue.AssigneeConverter() },
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        var ids = JsonConvert.DeserializeObject<List<IssueIdData>>(jsonResponse, settings);
        var issues = new List<Issue>();
        const string queryUrl =
            "?fields=id,summary,description,comments(text,author(login,fullName)),assignee(l),type,state,priority,spentTime,customFields(name,value($type,value,login,ordinal(name)))";
        
        foreach (var a in ids)
        {
            using var issueResponse = await client.GetAsync(a.Id + queryUrl);
            var issueJson = await issueResponse.Content.ReadAsStringAsync();
            
            if (issueJson != null)
            {
                issues.Add(JsonConvert.DeserializeObject<Issue>(issueJson, settings));
            }
        }
        
        return issues;
    }
}