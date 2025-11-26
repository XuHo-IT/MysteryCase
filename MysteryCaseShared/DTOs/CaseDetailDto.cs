using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs
{
    public class CaseDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FullNarrative { get; set; } = string.Empty; 
        public string DifficultyLevel { get; set; } = string.Empty;
        public List<ClueDto> Clues { get; set; } = new List<ClueDto>();
        public int UserPoints { get; set; }
        public int CluesFoundCount { get; set; }
        public bool HasBeenSolved { get; set; }
    }
}
