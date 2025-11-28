using MediatR;
using MysteryCaseShared.DTOs.Cases.Request;
using MysteryCaseShared.DTOs.Cases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Cases
{
    public record SubmitAnswerCommand(SubmitAnswerRequest Request, Guid UserId) : IRequest<SubmitAnswerResponseDto>;
}
