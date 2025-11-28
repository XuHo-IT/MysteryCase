using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs.Cases.Request
{
    public class SubmitAnswerRequest
    {
        public Guid CaseId { get; set; }
        public string SubmittedAnswer { get; set; } = string.Empty; 
    }
}
