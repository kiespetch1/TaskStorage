using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskStorage.Controllers.Entities;

public class Issue
{
    public string Id { get; set; }

    [JsonProperty("summary")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [JsonProperty("comments")]
    public List<Comment>? Comments { get; set; }

    public class CommentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Comment);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var comment = new Comment
            {
                Text = (string)obj["text"],
                Author = obj["author"]?["login"]?.ToString()
            };

            return comment;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public Issue(string id, string name, string description, List<Comment> comment)
    {
        Id = id;
        Name = name;
        Description = description;
        Comments = comment;
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