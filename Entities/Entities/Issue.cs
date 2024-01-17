using Entities.Entities.DTOs;
using Newtonsoft.Json;
using Type = Entities.Entities.DTOs.Type;

namespace Entities.Entities;

public class Issue
{
    public string Id { get; set; }

    [JsonProperty("idReadable")]
    public string Key { get; set; }

    [JsonProperty("summary")] 
    public string? Name { get; set; }

    public string? Description { get; set; }

    [JsonProperty("comments")] 
    public List<Comment>? Comments { get; set; }

    public Assignee Assignee { get; set; }
    
    public Type Type { get; set; }
    
    public State State { get; set; }

    public Priority Priority { get; set; }

    public TimeSpan SpentTime { get; set; }

    public List<WorkLogInfo> WorkLogs { get; set; }

    public Issue(string id, string? name, string? description, List<Comment>? comments, Assignee assignee, Type type,
        State state, Priority priority, TimeSpan spentTime)
    {
        Id = id;
        Name = name;
        Description = description;
        Comments = comments;
        Assignee = assignee;
        Type = type;
        State = state;
        Priority = priority;
        SpentTime = spentTime;
    }

    public Issue(string id, string? name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Issue(string id)
    {
        Id = id;
    }

    public Issue()
    {
    }
}