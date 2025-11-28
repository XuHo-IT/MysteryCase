using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.DTOs.Cases.Request
{
    public class CreateClueRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public int UnlockCost { get; set; } = 10;
        public string? ImageUrl { get; set; }
    }
}
