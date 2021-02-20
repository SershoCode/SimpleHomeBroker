using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;
using SimpleHomeBroker.EndpointClients.HomePC;

namespace SimpleHomeBroker.Application.CommandHandlers.HomePC
{
    public class MonitorsOnCommandHandler : IRequestHandler<MonitorsOnCommand, ComputerCommandResult>
    {
        private readonly IHomePcClient _homePcClient;

        public MonitorsOnCommandHandler(IHomePcClient homePcClient)
        {
            _homePcClient = homePcClient ?? throw new ArgumentNullException(nameof(homePcClient));
        }

        public async Task<ComputerCommandResult> Handle(MonitorsOnCommand request, CancellationToken cancellationToken)
        {
            var apiResponse = await _homePcClient.MonitorsOnAsync(cancellationToken);

            return new ComputerCommandResult(apiResponse.IsSuccess, apiResponse.Comment);
        }
    }
}