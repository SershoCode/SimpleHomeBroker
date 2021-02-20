using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace SimpleHomeBroker.EndpointClients.HomePC
{
    public class HomePcClient : IHomePcClient
    {
        private readonly HttpClient _httpClient;

        public HomePcClient(IHttpClientFactory httpClientFactory, IOptions<EndpointsOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(options?.Value.HomePc);
            _httpClient.DefaultRequestHeaders.Add("Bearer", options?.Value.BearerToken);
        }

        public Task<HomePcResponse> MonitorsOnAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("monitorson", stoppingToken);

        public Task<HomePcResponse> MonitorsOffAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("monitorsoff", stoppingToken);

        public Task<HomePcResponse> ComputerRebootAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("computerreboot", stoppingToken);

        public Task<HomePcResponse> ComputerBlockAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("computerblock", stoppingToken);

        public Task<HomePcResponse> ComputerLogoutAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("computerlogout", stoppingToken);

        public Task<HomePcResponse> ComputerWakeAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("computerwake", stoppingToken);

        public Task<HomePcResponse> ComputerShutdownAsync(CancellationToken stoppingToken) =>
            GetComputerRequestAsync("computershutdown", stoppingToken);

        private async Task<HomePcResponse> GetComputerRequestAsync(string request, CancellationToken stoppingToken)
        {
            var response = await _httpClient.GetAsync($"{request}", stoppingToken);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<HomePcResponse>(responseContent);

            return deserializedResponse;
        }
    }
}