using MediatR;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Cases
{
    public record GetCaseDetailCommand(Guid CaseId, Guid UserId) : IRequest<CaseDetailDto>;
}
