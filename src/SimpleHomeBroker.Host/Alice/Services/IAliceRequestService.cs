using System.Threading.Tasks;
using SimpleHomeBroker.Host.Alice.Models;

namespace SimpleHomeBroker.Host.Alice.Services
{
    public interface IAliceRequestService
    {
        Task<string> HandleRequestAsync(AliceRequest request);
    }
}