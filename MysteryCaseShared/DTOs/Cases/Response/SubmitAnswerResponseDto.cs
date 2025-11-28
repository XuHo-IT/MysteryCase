using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs.Cases.Response
{
    public class SubmitAnswerResponseDto
    {
        public bool IsCorrect { get; set; }
        public int ScoreEarned { get; set; }
        public string Message { get; set; } = string.Empty; 
        public string? DetailedSolution { get; set; } 
    }
}
