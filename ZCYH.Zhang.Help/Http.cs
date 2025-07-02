using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZCYH.Zhang.Help
{
    public class Http
    {
        public static string Get(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception($"Error fetching data from {url}: {response.ReasonPhrase}");
                }
            }
        }
        public static async Task<T> Post<T>(string url, object body)
        {
            var json = JsonSerializer.Serialize(body);
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = client.PostAsync(url, new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return await JsonSerializer.DeserializeAsync<T>(result);
                }
                else
                {
                    //return
                }
            }
        }
    }
}
