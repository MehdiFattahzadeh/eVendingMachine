using eVendingMachine.Commands;
using eVendingMachine.Common.Tools;
using eVendingMachine.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    public class PurchaseControllerTest : IntegrationTest
    {

        public PurchaseControllerTest(ApiWebApplicationFactory fixture) : base(fixture) { }

        [Fact]
        public async Task Accept_coins_Customer_should_be_able_to_insert_coins_to_the_vending_machine()
        {
            var command = new InsertCasheToMachinCommand
            {
                CashId = DefaultEntities.Cash_10_cent.Id
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/cash/Insert", postData);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }

        [Fact]
        public async Task Customer_Should_Be_Able_Cancel_Purchase()
        {
            var command = new CancelPurchaseCommand
            {
                PurchaseId = DefaultEntities.purchase2.Id,
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/purchase/Cancel", postData);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_OK_If_the_product_price_is_lower_than_the_amount_inserted_Vending_machine_should_()
        {
            var command = new PickProductCommand
            {
                PurchaseId = DefaultEntities.purchase1.Id,
                ProductId = DefaultEntities.product_Tea.Id
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/purchase", postData);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }


        [Fact]
        public async Task Customer_should_be_able_to_buy_a_product()
        {
            var products = await _client.GetAndDeserialize<List<ProductDto>>("/api/Product");
            Assert.True(products.Count > 0);
        }

        [Fact]
        public async Task Should_Return_BadRequest_If_the_product_price_is_higher_than_the_amount_inserted_Vending_machine_should_()
        {
            var command = new PickProductCommand
            {
                PurchaseId = DefaultEntities.purchase3.Id,
                ProductId = DefaultEntities.product_Chicken_Soup.Id
            };

            var postData = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/purchase", postData);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

    }
}
