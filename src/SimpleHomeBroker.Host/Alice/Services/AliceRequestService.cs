using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;
using SimpleHomeBroker.Host.Alice.Models;
using SimpleHomeBroker.Host.Alice.Options;

namespace SimpleHomeBroker.Host.Alice.Services
{
    public class AliceRequestService : IAliceRequestService
    {
        private readonly IMediator _mediator;
        private readonly AliceOptions _options;

        public AliceRequestService(IMediator mediator, IOptions<AliceOptions> options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> HandleRequestAsync(AliceRequest request)
        {
            if (request.Session.Application.ApplicationId != _options.ApplicationId)
                return "Извините, к сожалению вам запрещен доступ к навыку";

            var requestText = request.Request.Command.ToLowerInvariant();

            return requestText switch
            {
                "включить мониторы" => await ExecuteComputerCommand(new MonitorsOnCommand(), requestText),
                "выключить мониторы" => await ExecuteComputerCommand(new MonitorsOffCommand(), requestText),
                "выключить компьютер" => await ExecuteComputerCommand(new ComputerShutdownCommand(), requestText),
                "включить компьютер" => await ExecuteComputerCommand(new ComputerWakeCommand(), requestText),
                "перезагрузить компьютер" => await ExecuteComputerCommand(new ComputerRebootCommand(), requestText),
                "заблокировать компьютер" => await ExecuteComputerCommand(new ComputerBlockCommand(), requestText),
                "разлогинить компьютер" => await ExecuteComputerCommand(new ComputerLogoutCommand(), requestText),
                _ => "Извините, данная команда не существует или доступна только владельцу"
            };
        }

        // Сделано для сокращения количества кода и упрощения читаемости.
        // Отправляем объект команды через медиатор и анбоксим результат в нужный ответ.
        private async Task<string> ExecuteComputerCommand(object command, string requestText)
        {
            var commandResult = await _mediator.Send(command);

            var unboxedResult = (ComputerCommandResult) commandResult;

            return unboxedResult.IsSuccess ? "Сделано" : $"Не удалось {requestText}.{unboxedResult.Comment}";
        }
    }
}