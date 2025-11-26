using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseDomain
{
    public class Clue : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Location { get; set; }
        public bool IsHidden { get; set; } = true;
        public Cost UnlockCost { get; set; } = Cost.Zero;
        public string? ImageUrl { get; set; }
        public Case Case { get; set; } = null!;
    }
}
