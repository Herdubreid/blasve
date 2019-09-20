using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace blasve.Services
{
    public class E1Service : Celin.AIS.Server
    {
        async Task Authenticate()
        {
            try
            {
                AuthRequest.username = "DEMO";
                AuthRequest.password = "DEMO";
                await AuthenticateAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public E1Service(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration["baseUrl"], httpClientFactory.CreateClient())
        {
            //_ = Authenticate();
        }
    }
}
