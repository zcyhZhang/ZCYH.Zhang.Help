using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZCYH.Zhang.Help
{
    public class Http
    {
        public static async Task<string> GetAsync(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Error fetching data from {url}: {response.ReasonPhrase}");
                }
            }
        }

        public static async Task<T> PostAsync<T>(string url, object body)
        {
            try
            {
                var bytes = JsonSerializer.SerializeToUtf8Bytes(body);
                using (var client = new System.Net.Http.HttpClient())
                {
                    var content = new ByteArrayContent(bytes)
                    {
                        Headers = { ContentType = MediaTypeHeaderValue.Parse("application/json") }
                    };
                    using var resp = await client.PostAsync(url, content);
                    resp.EnsureSuccessStatusCode();
                    var str = await resp.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(str);
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
