using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseDomain
{
    public class UserCaseInteraction : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CaseId { get; set; }
        public string CurrentProgress { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public bool IsSolved { get; set; } = false;
        public int Score { get; set; } = 0;
        public User User { get; set; } = null!;
        public Case Case { get; set; } = null!;

    }
}
