using Newtonsoft.Json;

namespace TaskStorage.Entities;

public class Issue
{
    public string Id { get; set; }

    [JsonProperty("summary")] 
    public string? Name { get; set; }

    public string? Description { get; set; }

    [JsonProperty("comments")] 
    public List<Comment>? Comments { get; set; }

    public Assignee Assignee { get; set; }
    
    [JsonProperty("$type")] 
    public string Type { get; set; }
    
    public State State { get; set; }

    public Priority Priority { get; set; }

    public TimeSpan SpentTime { get; set; }


    public Issue(string id, string? name, string? description, List<Comment>? comments, Assignee assignee, string type,
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