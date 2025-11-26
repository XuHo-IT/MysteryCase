using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseDomain
{
    public class Solution : BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CaseId { get; set; }
        public string CorrectAnswer { get; set; } = string.Empty;
        public string DetailedExplanation { get; set; } = string.Empty;
        public Case Case { get; set; } = null!;
    }
}
