using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Commands.Clues;
using MysteryCaseDomain;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class UnlockClueHandler : IRequestHandler<UnlockClueCommand, ClueDto>
    {
        private readonly MysteryCaseDbContext _context;

        public UnlockClueHandler(MysteryCaseDbContext context) => _context = context;

        public async Task<ClueDto> Handle(UnlockClueCommand command, CancellationToken ct)
        {
            var user = await _context.Users.FindAsync(command.UserId);
            var clue = await _context.Clues.FindAsync(command.ClueId);

            if (user == null || clue == null)
            {
                throw new KeyNotFoundException("User or Clue does not exist.");
            }

            var isClueUnlocked = await _context.UserClueLogs
                .AnyAsync(log => log.UserId == command.UserId && log.ClueId == command.ClueId, ct);

            if (isClueUnlocked)
            {

                var existingDto = clue.Adapt<ClueDto>();
                existingDto.IsUnlocked = true;
                existingDto.Content = clue.Content;
                existingDto.ImageUrl = clue.ImageUrl;
                return existingDto;
            }

            if (user.Points < (int)clue.UnlockCost)
            {
                throw new InvalidOperationException($"Not enough points. Need {clue.UnlockCost} points.");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                user.Points -= (int)clue.UnlockCost;

                var log = new UserClueLog
                {
                    UserId = command.UserId,
                    ClueId = command.ClueId,
                    UnlockMethod = Constants.Unlock.Purchased,
                };

                _context.UserClueLogs.Add(log);
                clue.IsHidden = false; 

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                var unlockedDto = clue.Adapt<ClueDto>();
                unlockedDto.IsUnlocked = true;
                unlockedDto.Content = clue.Content;
                unlockedDto.ImageUrl = clue.ImageUrl;
                return unlockedDto;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
