using eVendingMachine.Data.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Net.Http;
using Xunit;
using static eVendingMachine.Web.Api.Tests.Integration.ApiWebApplicationFactory;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    [Collection("Non-Parallel Collection")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
        }
    }
}
