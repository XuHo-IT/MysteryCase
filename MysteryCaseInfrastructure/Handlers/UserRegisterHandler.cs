using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Commands.Auth;
using MysteryCaseDomain;
using MysteryCaseInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseInfrastructure.Handlers
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Guid>
    {
        private readonly MysteryCaseDbContext _context;

        public UserRegisterHandler(MysteryCaseDbContext context) => _context = context;

        public async Task<Guid> Handle(UserRegisterCommand command, CancellationToken ct)
        {
   
            if (await _context.Users.AnyAsync(u => u.Email == command.Request.Email || u.Username == command.Request.Username, ct))
            {
                throw new Exception("Username or Email already exists."); 
            }


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Request.Password);


            var user = new User
            {
                Username = command.Request.Username,
                Email = command.Request.Email,
                HashedPassword = hashedPassword,
                Role = RoleUser.Player,
                Points = 100 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);

            return user.Id;
        }
    }
}
