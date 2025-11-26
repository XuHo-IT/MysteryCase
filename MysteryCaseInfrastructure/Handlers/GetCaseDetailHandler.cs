using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class GetCaseDetailHandler : IRequestHandler<GetCaseDetailQuery, CaseDetailDto>
    {
        private readonly MysteryCaseDbContext _context;

        public GetCaseDetailHandler(MysteryCaseDbContext context) => _context = context;

        public async Task<CaseDetailDto> Handle(GetCaseDetailQuery request, CancellationToken ct)
        {
            var @case = await _context.Cases
                .Include(c => c.Clues) 
                .FirstOrDefaultAsync(c => c.Id == request.CaseId, ct);

            if (@case == null)
            {
                throw new Exception("Vụ án không tồn tại.");
            }

            var unlockedClueIds = await _context.UserClueLogs
                .Where(log => log.UserId == request.UserId && log.Clue.CaseId == request.CaseId)
                .Select(log => log.ClueId)
                .ToListAsync(ct);

            var user = await _context.Users.FindAsync(request.UserId);
            var interaction = await _context.UserCaseInteractions
                .FirstOrDefaultAsync(i => i.UserId == request.UserId && i.CaseId == request.CaseId, ct);

            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            var caseDetailDto = @case.Adapt<CaseDetailDto>();

            caseDetailDto.UserPoints = user.Points;
            caseDetailDto.HasBeenSolved = interaction?.IsSolved ?? false;
            caseDetailDto.CluesFoundCount = unlockedClueIds.Count;

            caseDetailDto.Clues = @case.Clues
                .Select(clue =>
                {
                    var isUnlocked = unlockedClueIds.Contains(clue.Id);

                    return new ClueDto
                    {
                        Id = clue.Id,
                        Title = clue.Title,
                        UnlockCost = (int)clue.UnlockCost,
                        IsUnlocked = isUnlocked,
                        Content = isUnlocked ? clue.Content : "???",
                        ImageUrl = isUnlocked ? clue.ImageUrl : null
                    };
                })
                .OrderBy(c => c.Title) 
                .ToList();

            return caseDetailDto;
        }
    }
}
