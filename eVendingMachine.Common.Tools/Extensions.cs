using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eVendingMachine.Common.Tools
{
    public static class Extensions
    {
        public async static Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
