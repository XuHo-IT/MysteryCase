using MediatR;
using MysteryCaseShared.DTOs.Cases.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Cases
{
    public record CreateCaseCommand(CreateCaseRequest Request) : IRequest<Guid>;
}
