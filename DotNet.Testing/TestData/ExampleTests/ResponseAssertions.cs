using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;

namespace ApiTestHarness
{
    public static class ResponseAssertions
    {
        private static readonly JsonSerializerOptions DefaultJson = new(JsonSerializerDefaults.Web);

        public static async Task<T> ShouldBeOkAndReadJsonAsync<T>(this HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException($"Expected 200 OK but got {(int)response.StatusCode} {response.ReasonPhrase}");

            var data = await response.Content.ReadFromJsonAsync<T>(DefaultJson);
            return data ?? throw new InvalidOperationException("Response JSON was null.");
        }

        public static void ShouldHaveStatus(this HttpResponseMessage response, HttpStatusCode status)
        {
            if (response.StatusCode != status)
                throw new InvalidOperationException($"Expected {(int)status} but got {(int)response.StatusCode} {response.ReasonPhrase}");
        }
    }
}

