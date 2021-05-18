using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MutantCoreTest.FunctionalTest.Extensions
{
    internal static class HttpClientExtensions
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        internal static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T model)
        {
            var json = JsonSerializer.Serialize<T>(model, SerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return client.PostAsync(requestUri, content);
        }

    }
}
