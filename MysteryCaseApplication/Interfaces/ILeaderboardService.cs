using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Interfaces
{
    public interface ILeaderboardService
    {
        Task CalculateLeaderboardAsync(CancellationToken ct = default);
    }
}
