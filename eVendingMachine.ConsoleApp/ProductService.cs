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
    public class ProductService
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }
        private HttpClient _client;
        public ProductService(ILogger<CasheService> logger,
            IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _client = _httpFactory.CreateClient();
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            return await _client.GetAndDeserialize<List<ProductDto>>("https://localhost:5001/api/Product");
        }

        public async Task<List<CashDto>> BuyProducts(PickProductCommand command)
        {
            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("https://localhost:5001/api/purchase", postData);
            var responsecontent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<List<CashDto>>(responsecontent);
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new ApiException("Insufficient amount");
            }
            throw new Exception();

        }
    }
}
