using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Commands.Auth;
using MysteryCaseApplication.Interfaces;
using MysteryCaseInfrastructure;
using MysteryCaseShared.DTOs.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, AuthResponseDto>
    {
        private readonly MysteryCaseDbContext _context;
        private readonly IJwtService _jwtService;

        public UserLoginHandler(MysteryCaseDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(UserLoginCommand command, CancellationToken ct)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == command.Request.UsernameOrEmail || u.Username == command.Request.UsernameOrEmail, ct);

            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Request.Password, user.HashedPassword))
            {
                throw new UnauthorizedAccessException("Incorrect username or password.");
            }

            var authResponse = _jwtService.GenerateToken(user);

            return authResponse;
        }
    }
}
