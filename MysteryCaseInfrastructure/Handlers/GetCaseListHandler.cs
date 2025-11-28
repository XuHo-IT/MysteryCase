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
using ZiggyCreatures.Caching.Fusion;
using static MysteryCaseDomain.Constants;

namespace MysteryCaseApplication.Querries.Cases
{
    public class GetCaseListHandler : IRequestHandler<GetCaseListCommand, List<CaseListDto>>
    {
        private readonly MysteryCaseDbContext _context;
        private readonly IFusionCache _cache;

        public GetCaseListHandler(MysteryCaseDbContext context, IFusionCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<CaseListDto>> Handle(GetCaseListCommand request, CancellationToken cancellationToken)
        {
            const string cacheKey = "ActiveCaseList";
            const int cacheDurationMinutes = 5; 

            var caseDtos = await _cache.GetOrSetAsync(
                cacheKey,
                async (token) =>
                {
                    var cases = await _context.Cases
                        .Where(c => c.ResolutionStatus == Resolutions.Active)
                        .AsNoTracking() 
                        .ToListAsync(token);

                    var dtos = cases.Adapt<List<CaseListDto>>();

                    foreach (var dto in dtos) { dto.IsSolved = false; }

                    return dtos;

                },
                new FusionCacheEntryOptions(TimeSpan.FromMinutes(cacheDurationMinutes)),
                cancellationToken);

            return caseDtos;
        }
    }
}
