using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;
using SimpleHomeBroker.Host.Telegram.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleHomeBroker.Host.Telegram.Services
{
    public class TelegramRequestService : ITelegramRequestService
    {
        private readonly IMediator _mediator;
        private readonly TelegramOptions _options;

        public TelegramRequestService(IMediator mediator, IOptions<TelegramOptions> options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> HandleRequestAsync(Update update)
        {
            if (update.Message.Type != MessageType.Text)
                return "Я могу принимать только текстовые команды";

            if (!_options.Family.Contains(update.Message.From.Id))
                return "К сожалению вам запрещено отправлять мне команды";

            var sender = update.Message.From.Id;
            var message = update.Message.Text;

            // В зависимости от сообщения выполняем mediator команду и отдаем ответ в виде сообщения.
            return message switch
            {
                "Вкл. мониторы" => await ExecuteComputerCommand(new MonitorsOnCommand(), message),
                "Выкл. мониторы" => await ExecuteComputerCommand(new MonitorsOffCommand(), message),
                "Выключить PC" when IsSenderOwner(sender) => await ExecuteComputerCommand(new ComputerShutdownCommand(),
                    message),
                "Включить PC" when IsSenderOwner(sender) => await ExecuteComputerCommand(new ComputerWakeCommand(),
                    message),
                "Перезагрузить PC" when IsSenderOwner(sender) => await ExecuteComputerCommand(
                    new ComputerRebootCommand(), message),
                "Заблокировать PC" when IsSenderOwner(sender) => await ExecuteComputerCommand(
                    new ComputerBlockCommand(), message),
                "Разлогинить PC" when IsSenderOwner(sender) => await ExecuteComputerCommand(new ComputerLogoutCommand(),
                    message),
                _ => "Извините, данная команда не существует или доступна только владельцу"
            };
        }

        // Сделано для сокращения количества кода и упрощения читаемости.
        // Отправляем объект команды через медиатор и анбоксим результат в нужный ответ.
        private async Task<string> ExecuteComputerCommand(object command, string message)
        {
            var commandResult = await _mediator.Send(command);

            var unboxedResult = (ComputerCommandResult) commandResult;

            return unboxedResult.IsSuccess ? "Готово" : $"Не удалось {message}: <code>{unboxedResult.Comment}</code>";
        }

        private bool IsSenderOwner(int from) => from == _options.Owner;
    }
}