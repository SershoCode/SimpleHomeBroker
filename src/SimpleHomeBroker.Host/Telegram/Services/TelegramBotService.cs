using Microsoft.Extensions.Options;
using SimpleHomeBroker.Host.Telegram.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleHomeBroker.Host.Telegram.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        public TelegramBotClient Client { get; }

        public TelegramBotService(IOptions<TelegramOptions> options)
        {
            Client = new TelegramBotClient(options.Value.Token);
            Client.SetWebhookAsync(options.Value.WebHookUrl);
            Client.SendTextMessageAsync(options.Value.Owner, "Инициализация", ParseMode.Html,
                replyMarkup: new ReplyKeyboardMarkup(options.Value.KeyboardArray));
        }
    }
}