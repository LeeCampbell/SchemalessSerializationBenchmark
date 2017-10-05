using System;
using System.Reflection;
using Newtonsoft.Json;
using SchemalessSerializationBenchmark.Serialization.Contracts;

namespace SchemalessSerializationBenchmark.Serialization.NewtonSoftJsonInfra
{
    public sealed class DateJsonSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (Date?)value;

            if (date == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(date.Value.ToString("yyyy-MM-dd"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var serialisedDate = reader.Value as string;

            if (serialisedDate == null)
                return null;
            return Date.ParseExact(serialisedDate, "yyyy-MM-dd");
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Date?).GetTypeInfo().IsAssignableFrom(objectType);
        }
    }
}
