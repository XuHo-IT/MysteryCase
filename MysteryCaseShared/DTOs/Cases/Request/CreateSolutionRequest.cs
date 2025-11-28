using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs.Cases.Request
{
    public class CreateSolutionRequest
    {
        [Required]
        public string CorrectAnswer { get; set; } = string.Empty;
        [Required]
        public string DetailedExplanation { get; set; } = string.Empty;
    }
}
