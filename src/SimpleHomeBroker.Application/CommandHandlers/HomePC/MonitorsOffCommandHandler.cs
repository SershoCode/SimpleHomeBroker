using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;
using SimpleHomeBroker.EndpointClients.HomePC;

namespace SimpleHomeBroker.Application.CommandHandlers.HomePC
{
    public class MonitorsOffCommandHandler : IRequestHandler<MonitorsOffCommand, ComputerCommandResult>
    {
        private readonly IHomePcClient _homePcClient;

        public MonitorsOffCommandHandler(IHomePcClient homePcClient)
        {
            _homePcClient = homePcClient ?? throw new ArgumentNullException(nameof(homePcClient));
        }

        public async Task<ComputerCommandResult> Handle(MonitorsOffCommand request, CancellationToken cancellationToken)
        {
            // Выключение мониторов не успевает отработать до таймаута Яндекс навыка, поэтому приходится запускать в отдельном потоке и сразу отдавать ответ

            _ = Task.Run(async () => { await _homePcClient.MonitorsOffAsync(cancellationToken); }, cancellationToken);

            return new ComputerCommandResult(true, "");
        }
    }
}