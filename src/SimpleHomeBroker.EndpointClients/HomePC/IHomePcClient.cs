using System.Threading;
using System.Threading.Tasks;

namespace SimpleHomeBroker.EndpointClients.HomePC
{
    public interface IHomePcClient
    {
        Task<HomePcResponse> ComputerBlockAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> ComputerLogoutAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> ComputerWakeAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> ComputerRebootAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> ComputerShutdownAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> MonitorsOffAsync(CancellationToken stoppingToken);
        Task<HomePcResponse> MonitorsOnAsync(CancellationToken stoppingToken);
    }
}