using MediatR;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseApplication.Querries.Cases
{
    public record GetCaseListQuery : IRequest<List<CaseListDto>>;
}
