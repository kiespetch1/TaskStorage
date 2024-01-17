using Entities.Entities.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApplicationCore.Converters;

public class WorkLogConverter : JsonConverter
{
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var obj = JArray.Load(reader);
        var list = new List<WorkLogInfo>();
        
        foreach (var field in obj)
        {
            var duration = Convert.ToInt32(field?["duration"]?["minutes"].ToString());
            var date = Convert.ToInt64(field?["date"].ToString());
            var text = field?["text"].ToString();
            var author = field?["author"]?["login"].ToString();
            
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(date / 1000).DateTime;
            var dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        
        
            list.Add(new WorkLogInfo
            {
                Duration = duration,
                Date = dateOnly,
                Text = text,
                Author = author
            });
        }
        return list;
    }

    public override bool CanConvert(System.Type objectType)
    {
        return objectType == typeof(List<WorkLogInfo>);
    }
}