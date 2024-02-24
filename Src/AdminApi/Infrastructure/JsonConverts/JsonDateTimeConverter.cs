using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        readonly string _dateFormatString;
        public JsonDateTimeConverter(string dateFormatString)
        {
            _dateFormatString = dateFormatString;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString(), CultureInfo.CurrentCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormatString));
        }
    }

    public class JsonDateTimeNullableConverter : JsonConverter<DateTime?>
    {
        readonly string _dateFormatString;
        
        public JsonDateTimeNullableConverter(string dateFormatString)
        {
            _dateFormatString = dateFormatString;
        }
        
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString(), CultureInfo.CurrentCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(_dateFormatString));
        }
    }
}
