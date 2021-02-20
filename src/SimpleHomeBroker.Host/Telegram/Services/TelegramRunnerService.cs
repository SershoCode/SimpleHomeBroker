using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SimpleHomeBroker.Host.Telegram.Services
{
    public class TelegramRunnerService : BackgroundService
    {
        public TelegramRunnerService(ITelegramBotService botServiceInitialize)
        {
            // Сервис создан для инициализации экземпляра ITelegramBotService сразу после старта приложения.

            // Мы должны иметь возможность прокидывать экземпляр ITelegramBotService через DI для отправки сообщений.
            // Но при этом, так же, для корректной работы всего Telegram endpoint'a, клиент внутри этого экземпляра должен задать webhook адрес сразу при старте приложения.
            // Следовательно, из-за необходимости инициализировать endpoint, мы не можем ждать первого вызова где либо, чтобы получить "классический" вызов.
            // Судя по разным источникам, различные варианты запуска внутри Startup'a считаются грязным хаком.
            // Поэтому, по сути, это 'хак на хак', что тоже грустно.
            // В итоге, получается такая схема, что данный HostedService выполняется при старте приложения.
            // Который, в свою очередь просит из контейнера зависимость в виде ITelegramBotService, тем самым заставляя его проинициализироваться, но уже сразу после запуска.
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}