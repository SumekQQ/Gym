using System.Net.Http;
using System.Text;
using Gym.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Gym.Tests.Controllers
{
    public abstract class ControllerTestsTemplate
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        protected ControllerTestsTemplate()
        {
            Server = new TestServer(new WebHostBuilder()
                          .UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}