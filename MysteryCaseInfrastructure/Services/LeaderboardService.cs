using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MysteryCaseApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly MysteryCaseDbContext _context;
        private readonly ILogger<LeaderboardService> _logger;

        public LeaderboardService(MysteryCaseDbContext context, ILogger<LeaderboardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CalculateLeaderboardAsync(CancellationToken ct = default)
        {

            var topUsers = await _context.Users
                .OrderByDescending(u => u.Points) 
                .Take(10)
                .Select(u => new { u.Username, u.Points })
                .ToListAsync(ct);

            _logger.LogInformation($"[Hangfire Job] Leaderboard calculated. Top user: {topUsers.FirstOrDefault()?.Username} with {topUsers.FirstOrDefault()?.Points} points.");
        }
    }
}
