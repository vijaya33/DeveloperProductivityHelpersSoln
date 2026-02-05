using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ApiTestHarness
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions DefaultJson = new(JsonSerializerDefaults.Web);

        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string url, CancellationToken ct = default)
        {
            var resp = await client.GetAsync(url, ct);
            resp.EnsureSuccessStatusCode();

            var data = await resp.Content.ReadFromJsonAsync<T>(DefaultJson, ct);
            return data ?? throw new InvalidOperationException("Response JSON was null.");
        }

        public static Task<HttpResponseMessage> PostJsonAsync<TBody>(this HttpClient client, string url, TBody body, CancellationToken ct = default)
            => client.PostAsJsonAsync(url, body, DefaultJson, ct);

        public static Task<HttpResponseMessage> PutJsonAsync<TBody>(this HttpClient client, string url, TBody body, CancellationToken ct = default)
            => client.PutAsJsonAsync(url, body, DefaultJson, ct);
    }

}