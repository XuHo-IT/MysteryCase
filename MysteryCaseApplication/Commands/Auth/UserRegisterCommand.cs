using MediatR;
using MysteryCaseShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Auth
{
    public record UserRegisterCommand(RegisterRequest Request) : IRequest<Guid>;
}
