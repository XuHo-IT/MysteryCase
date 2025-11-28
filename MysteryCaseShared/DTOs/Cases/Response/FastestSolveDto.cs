using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs.Cases.Response
{
    public class FastestSolveDto
    {
        public Guid CaseId { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public TimeSpan TimeToSolve { get; set; } 
    }
}
