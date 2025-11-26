using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs
{
    public class ClueDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; 
        public int UnlockCost { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsUnlocked { get; set; } 
    }
}
