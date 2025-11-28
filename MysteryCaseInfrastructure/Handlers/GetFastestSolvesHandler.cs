using Dapper;
using MediatR;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseInfrastructure.Interface;
using MysteryCaseShared.DTOs.Cases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Handlers
{
    public class GetFastestSolvesHandler : IRequestHandler<GetFastestSolvesQuery, List<FastestSolveDto>>
    {
        private readonly IDapperContext _dapperContext;

        public GetFastestSolvesHandler(IDapperContext dapperContext) => _dapperContext = dapperContext;

        public async Task<List<FastestSolveDto>> Handle(GetFastestSolvesQuery request, CancellationToken ct)
        {
            var sql = @"
                SELECT TOP 10
                    i.CaseId,
                    c.Title AS CaseTitle,
                    u.Username,
                    DATEDIFF(MINUTE, i.StartTime, i.EndTime) AS TimeToSolveMinutes
                FROM 
                    UserCaseInteractions i
                INNER JOIN 
                    Cases c ON i.CaseId = c.Id
                INNER JOIN
                    Users u ON i.UserId = u.Id
                WHERE 
                    i.IsSolved = 1 AND i.EndTime IS NOT NULL
                ORDER BY 
                    TimeToSolveMinutes ASC, i.EndTime ASC";

            using var connection = _dapperContext.CreateConnection();


            var results = await connection.QueryAsync(sql);

            var dtos = results.Select(r => new FastestSolveDto
            {
                CaseId = r.CaseId,
                CaseTitle = r.CaseTitle,
                Username = r.Username,
                TimeToSolve = TimeSpan.FromMinutes(r.TimeToSolveMinutes)
            }).ToList();

            return dtos;
        }
    }
}
