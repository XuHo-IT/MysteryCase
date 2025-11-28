using MediatR;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Clues
{
    public record UnlockClueCommand(Guid UserId, Guid ClueId) : IRequest<ClueDto>;
}
