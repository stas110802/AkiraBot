using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AkiraBot.ExchangeClients;

public class MultipleJsonConverter<T> : JsonConverter
    where T : class
{
    private readonly Dictionary<string, string> _propertyMappings;

    public MultipleJsonConverter()
    {
        _propertyMappings = ExchangeModelsFactory.GetMappingProperties<T>();
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.GetTypeInfo().IsClass;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        var instance = Activator.CreateInstance(objectType);
        var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

        var jo = JObject.Load(reader);
        foreach (var jp in jo.Properties())
        {
            if (!_propertyMappings.TryGetValue(jp.Name, out var name))
                name = jp.Name;

            var prop = props.FirstOrDefault(pi =>
                pi.CanWrite && pi.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == name);

            prop?.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
        }
        
        return instance;
    }
}