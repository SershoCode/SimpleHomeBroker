﻿using MediatR;
using SimpleHomeBroker.Application.CommandResults.HomePC;

namespace SimpleHomeBroker.Application.Commands.HomePC
{
    public class ComputerBlockCommand : IRequest<ComputerCommandResult>
    {
    }
}