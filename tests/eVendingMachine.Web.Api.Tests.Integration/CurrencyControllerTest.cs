using eVendingMachine.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    public class CurrencyControllerTest : IntegrationTest
    {
        public CurrencyControllerTest(ApiWebApplicationFactory fixture) : base(fixture) { }
       
        [Fact]
        public async Task POST_Currency_Controller()
        {
            var command = new CreateCurrencyCommand
            {
                Name = "Currency1.Name",
                Symbol = "Currency1.Symbol"
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Currency", postData);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
    }
}
