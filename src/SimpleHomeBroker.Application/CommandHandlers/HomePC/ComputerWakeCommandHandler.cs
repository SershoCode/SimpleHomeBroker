using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleHomeBroker.Application.CommandResults.HomePC;
using SimpleHomeBroker.Application.Commands.HomePC;

namespace SimpleHomeBroker.Application.CommandHandlers.HomePC
{
    public class ComputerWakeCommandHandler : IRequestHandler<ComputerWakeCommand, ComputerCommandResult>
    {
        public ComputerWakeCommandHandler()
        {
        }

        public Task<ComputerCommandResult> Handle(ComputerWakeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}