using MysteryCaseDomain;
using MysteryCaseShared.DTOs.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Interfaces
{
    public interface IJwtService
    {
        AuthResponseDto GenerateToken(User user);
    }
}
