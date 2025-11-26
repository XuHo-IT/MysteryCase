using MediatR;
using MysteryCaseShared;
using MysteryCaseShared.DTOs.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Commands.Auth
{
    public record UserLoginCommand(LoginRequest Request) : IRequest<AuthResponseDto>;
}
