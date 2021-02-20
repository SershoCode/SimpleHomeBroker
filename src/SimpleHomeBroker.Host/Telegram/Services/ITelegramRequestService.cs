using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SimpleHomeBroker.Host.Telegram.Services
{
    public interface ITelegramRequestService
    {
        Task<string> HandleRequestAsync(Update update);
    }
}