using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs
{
    public class CaseListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool IsSolved { get; set; } 
    }
}
