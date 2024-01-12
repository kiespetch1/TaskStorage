using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskStorage.Controllers.Entities;

namespace TaskStorage.Converters;

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