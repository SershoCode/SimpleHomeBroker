using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleHomeBroker.Host.Telegram.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleHomeBroker.Host.Telegram.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramRequestService _telegramUpdateService;
        private readonly ITelegramBotService _telegramBotService;
        private readonly ILogger<TelegramController> _logger;

        public TelegramController(ITelegramRequestService telegramUpdateService,
            ITelegramBotService telegramBotService,
            ILogger<TelegramController> logger)
        {
            _telegramUpdateService =
                telegramUpdateService ?? throw new ArgumentNullException(nameof(telegramUpdateService));
            _telegramBotService = telegramBotService ?? throw new ArgumentNullException(nameof(telegramBotService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<OkResult> WebHook([FromBody] Update update)
        {
            if (update?.Message == null)
                return Ok();

            var messageSender = update.Message.From.Id;

            try
            {
                var responseMessage = await _telegramUpdateService.HandleRequestAsync(update);

                await _telegramBotService.Client.SendTextMessageAsync(messageSender, responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в процессе обработки Telegram команды");

                await _telegramBotService.Client.SendTextMessageAsync(messageSender, BuildErrorMessage(update, ex),
                    ParseMode.Html);
            }

            return Ok();
        }

        private string BuildErrorMessage(Update update, Exception ex)
        {
            var stringBuilder = new StringBuilder("<b>SimpleHomeBroker</b>")
                .Append("\nОшибка в процессе обработки команды\n")
                .Append("<code>------------------\n")
                .Append($"{ex.Message}\n")
                .Append("------------------\n")
                .Append($"Пользователь: @{update.Message.From.Username} | {update.Message.From.Id}\n")
                .Append($"Текст: {update.Message.Text}</code>");

            return stringBuilder.ToString();
        }
    }
}