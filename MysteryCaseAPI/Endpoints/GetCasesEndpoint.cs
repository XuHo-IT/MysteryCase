using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Querries.Cases;
using MysteryCaseShared.DTOs;

namespace MysteryCaseAPI.Endpoints
{
    public class GetCasesEndpoint : EndpointWithoutRequest<List<CaseListDto>>
    {
        private readonly IMediator _mediator;
        public GetCasesEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/api/cases");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new GetCaseListQuery();
            var result = await _mediator.Send(query, ct);

            await SendOkAsync(result, ct);
        }
    }
}
