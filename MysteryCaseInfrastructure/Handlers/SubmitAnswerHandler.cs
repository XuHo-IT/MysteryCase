using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseDomain;
using MysteryCaseShared.DTOs.Cases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class SubmitAnswerHandler : IRequestHandler<SubmitAnswerCommand, SubmitAnswerResponseDto>
    {
        private readonly MysteryCaseDbContext _context;
        private const int CorrectAnswerPoints = 50; 

        public SubmitAnswerHandler(MysteryCaseDbContext context) => _context = context;

        public async Task<SubmitAnswerResponseDto> Handle(SubmitAnswerCommand command, CancellationToken ct)
        {
            var @case = await _context.Cases
                .Include(c => c.Solution)
                .FirstOrDefaultAsync(c => c.Id == command.Request.CaseId, ct);

            var user = await _context.Users.FindAsync(command.UserId);
            var interaction = await _context.UserCaseInteractions
                .FirstOrDefaultAsync(i => i.UserId == command.UserId && i.CaseId == command.Request.CaseId, ct);

            if (@case == null || @case.Solution == null || user == null)
            {
                throw new KeyNotFoundException("Invalid data.");
            }

            if (interaction?.IsSolved == true)
            {
                return new SubmitAnswerResponseDto
                {
                    IsCorrect = true,
                    ScoreEarned = 0,
                    Message = "You solved this case.",
                    DetailedSolution = @case.Solution.DetailedExplanation
                };
            }
            bool isCorrect = string.Equals(
                command.Request.SubmittedAnswer.Trim(),
                @case.Solution.CorrectAnswer.Trim(),
                StringComparison.OrdinalIgnoreCase
            );

            await using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                if (isCorrect)
                {
                    user.Points += CorrectAnswerPoints;

                    if (interaction == null)
                    {
                        interaction = new UserCaseInteraction { UserId = command.UserId, CaseId = command.Request.CaseId };
                        _context.UserCaseInteractions.Add(interaction);
                    }
                    interaction.IsSolved = true;
                    interaction.EndTime = DateTime.UtcNow;
                    interaction.Score = CorrectAnswerPoints;

                    await _context.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);

                    return new SubmitAnswerResponseDto
                    {
                        IsCorrect = true,
                        ScoreEarned = CorrectAnswerPoints,
                        Message = $"Congratulations! You have successfully solved the case and earned {CorrectAnswerPoints} points!",
                        DetailedSolution = @case.Solution.DetailedExplanation
                    };
                }
                else
                {
                    return new SubmitAnswerResponseDto
                    {
                        IsCorrect = false,
                        ScoreEarned = 0,
                        Message = "The answer is incorrect. Look for more clues!"
                    };
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
