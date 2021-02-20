using Telegram.Bot;

namespace SimpleHomeBroker.Host.Telegram.Services
{
    public interface ITelegramBotService
    {
        TelegramBotClient Client { get; }
    }
}