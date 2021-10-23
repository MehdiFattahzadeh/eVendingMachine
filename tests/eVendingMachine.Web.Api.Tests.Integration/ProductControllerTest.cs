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
    public class ProductControllerTest : IntegrationTest
    {

        public ProductControllerTest(ApiWebApplicationFactory fixture) : base(fixture) { }

        [Fact]
        public async Task POST_Product_Controller()
        {
            var command = new AddProductCommand
            {
                Name = "product_Tea.Name",
                Price = 1.5M,
                Number = 5
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/product", postData);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
    }
}
