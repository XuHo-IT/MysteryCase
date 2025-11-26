using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseDomain;
using MysteryCaseShared.DTOs;
using System.Security.Claims;

namespace MysteryCaseAPI.Endpoints
{
    public class GetCaseDetailEndpoint : EndpointWithoutRequest<CaseDetailDto>
    {
        private readonly IMediator _mediator;

        public GetCaseDetailEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Get("/cases/{CaseId}");
            // FastEndpoints auto check JWT
        }

        public override async Task HandleAsync(CancellationToken ct)
        {

            var caseId = Route<Guid>("CaseId");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var query = new GetCaseDetailQuery(caseId, userId);
            try
            {
                var result = await _mediator.Send(query, ct);
                await SendOkAsync(result, ct);
            }
            catch (Exception ex)
            {
                await SendNotFoundAsync(ct);
            }
        }
    }
}
