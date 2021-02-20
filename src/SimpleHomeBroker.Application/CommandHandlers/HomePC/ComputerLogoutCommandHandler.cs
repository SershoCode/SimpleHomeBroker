using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;
using SimpleHomeBroker.EndpointClients.HomePC;

namespace SimpleHomeBroker.Application.CommandHandlers.HomePC
{
    public class ComputerLogoutCommandHandler : IRequestHandler<ComputerLogoutCommand, ComputerCommandResult>
    {
        private readonly IHomePcClient _homePcClient;

        public ComputerLogoutCommandHandler(IHomePcClient homePcClient)
        {
            _homePcClient = homePcClient ?? throw new ArgumentNullException(nameof(homePcClient));
        }

        public async Task<ComputerCommandResult> Handle(ComputerLogoutCommand request,
            CancellationToken cancellationToken)
        {
            var apiResponse = await _homePcClient.ComputerLogoutAsync(cancellationToken);

            return new ComputerCommandResult(apiResponse.IsSuccess, apiResponse.Comment);
        }
    }
}