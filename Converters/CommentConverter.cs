using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskStorage.Controllers.Entities;

namespace TaskStorage.Converters;

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