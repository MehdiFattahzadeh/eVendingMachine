using eVendingMachine.Commands;
using eVendingMachine.Common.Tools;
using eVendingMachine.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eVendingMachine.ConsoleApp
{
    public class CasheService
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }
        public CasheService(ILogger<CasheService> logger,
            IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public async Task<List<CashDto>> GetCoins()
        {
            var client = _httpFactory.CreateClient();
            return await client.GetAndDeserialize<List<CashDto>>("https://localhost:5001/api/Cash");
        }
        public async Task<Guid> InsertCash(InsertCasheToMachinCommand command)
        {
            var client = _httpFactory.CreateClient();
            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:5001/api/cash/Insert", postData);
            return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }
    }
}
