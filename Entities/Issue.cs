using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskStorage.Controllers.Entities;

public class Issue
{
    public string Id { get; set; }

    [JsonProperty("summary")] public string? Name { get; set; }

    public string? Description { get; set; }

    [JsonProperty("comments")] public List<Comment>? Comments { get; set; }

    public Assignee Assignee { get; set; }

    [JsonProperty("$type")] public string Type { get; set; }

    public State State { get; set; } = State.ToDo;

    public Priority Priority { get; set; } = Priority.Normal;

    public TimeSpan SpentTime { get; set; }

    public class CommentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Comment);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var comment = new Comment
            {
                Text = (string)obj["text"],
                Author = obj["author"]?["login"]?.ToString()
            };

            return comment;
        }
        
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class AssigneeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Assignee);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var assigneeSection = obj["customFields"]?
                .FirstOrDefault(section => (string)section["name"] == "Assignee");

            var assignee = new Assignee
            {
                Login = assigneeSection?["value"]?["login"]?.ToString()
            };

            return assignee;
        }
        
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

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