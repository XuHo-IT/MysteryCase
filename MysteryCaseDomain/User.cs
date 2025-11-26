using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseDomain
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public int Points { get; set; } = 100;
        public RoleUser Role { get; set; } = RoleUser.Player;
        public ICollection<UserCaseInteraction> Interactions { get; set; } = new List<UserCaseInteraction>();
        public ICollection<UserClueLog> ClueLogs { get; set; } = new List<UserClueLog>();
    }
}
