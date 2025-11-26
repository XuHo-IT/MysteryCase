using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseDomain
{
    public class UserClueLog : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ClueId { get; set; }
        public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
        public Unlock UnlockMethod { get; set; } = Unlock.Granted;
        public User User { get; set; } = null!;
        public Clue Clue { get; set; } = null!;
    }
}
