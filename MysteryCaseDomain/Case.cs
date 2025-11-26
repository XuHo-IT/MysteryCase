using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseDomain
{
    public class Case : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FullNarrative { get; set; } = string.Empty;
        public Difficulty DifficultyLevel { get; set; } = Difficulty.Easy;
        public Resolutions ResolutionStatus { get; set; } = Resolutions.Active;
        public ICollection<Clue> Clues { get; set; } = new List<Clue>();
        public Solution? Solution { get; set; }
        public ICollection<UserCaseInteraction> Interactions { get; set; } = new List<UserCaseInteraction>();
    }
}
