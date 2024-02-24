using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRazor.Infrastructure
{
    public interface IJsonSerializer
    {
        T DeserializeObject<T>(string json);
        T[] DeserializeArray<T>(string json);
        string Serialize(object obj);
    }
    public class MyJsonSerializer : IJsonSerializer
    {
        readonly JsonOptions _jsonOptions;

        public MyJsonSerializer(IOptions<JsonOptions> jsonOptions)
        {
            _jsonOptions = jsonOptions.Value;
        }
        public string Serialize(object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, _jsonOptions.JsonSerializerOptions);
        }
        public T DeserializeObject<T>(string json)
        {
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(json, _jsonOptions.JsonSerializerOptions);
            }
            catch (Exception)
            {

                return default;
            }
        }

        public T[] DeserializeArray<T>(string json)
        {
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<T[]>(json, _jsonOptions.JsonSerializerOptions);
            }
            catch (Exception)
            {
                return Array.Empty<T>();
            }
        }
    }
}
