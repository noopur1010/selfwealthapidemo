using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SelfWealthApiDemo;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SelfWealthApiDemo.Test
{
    public class RetrieveUsersControllerTest : IDisposable
    {
        protected TestServer _testServer;

        public RetrieveUsersControllerTest()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);

        }

        public void Dispose()
        {
            _testServer.Dispose();
        }

        [Fact]
        public async Task RetrieveUsersDataFromGithubMethod()
        {
            var response = await _testServer.CreateRequest("/RetrieveUsers?UserNames=noopur1010").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task RetrieveUsersDataFromRedisCacheMethod()
        {
            var response = await _testServer.CreateRequest("/RetrieveUsers?UserNames=noopur1010").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}