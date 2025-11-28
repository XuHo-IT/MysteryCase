using Mapster;
using MediatR;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class CreateCaseHandler : IRequestHandler<CreateCaseCommand, Guid>
    {
        private readonly MysteryCaseDbContext _context;

        public CreateCaseHandler(MysteryCaseDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateCaseCommand command, CancellationToken ct)
        {
            var newCase = command.Request.Adapt<Case>();

            var solution = command.Request.Solution.Adapt<Solution>();
            newCase.Solution = solution;

            newCase.Clues = command.Request.Clues
                .Select(c => c.Adapt<Clue>())
                .ToList();

            _context.Cases.Add(newCase);
            await _context.SaveChangesAsync(ct);

            return newCase.Id;
        }
    }
}
