using eVendingMachine.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    
    public class CashControllerTest : IntegrationTest
    {
        public CashControllerTest(ApiWebApplicationFactory fixture) : base(fixture) { }
       

        [Fact]
        public async Task POST_Cash_Controller()
        {
            var command = new CreateCashCommand
            {
                CashType = 0,
                CurrencyId = DefaultEntities.Currency1.Id,
                Number = 20,
                Price = 0.2M,
                Code="2C"
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Cash", postData);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
    }
}
