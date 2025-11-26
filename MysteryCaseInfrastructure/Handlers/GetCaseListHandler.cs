using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MysteryCaseInfrastructure;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseApplication.Querries.Cases
{
    public class GetCaseListHandler : IRequestHandler<GetCaseListCommand, List<CaseListDto>>
    {
        private readonly MysteryCaseDbContext _context;

        public GetCaseListHandler(MysteryCaseDbContext context)
        {
            _context = context;
        }

        public async Task<List<CaseListDto>> Handle(GetCaseListCommand request, CancellationToken cancellationToken)
        {
            var cases = await _context.Cases
                .Where(c => c.ResolutionStatus == Resolutions.Active) 
                .ToListAsync(cancellationToken);

            var caseDtos = cases.Adapt<List<CaseListDto>>();

            foreach (var dto in caseDtos)
            {
                dto.IsSolved = false;
            }

            return caseDtos;
        }
    }
}
