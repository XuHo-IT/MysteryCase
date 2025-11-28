using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseShared.DTOs.Cases.Request;

namespace MysteryCaseAPI.Endpoints.Cases
{
    public class CreateCaseEndpoint : Endpoint<CreateCaseRequest, Guid>
    {
        private readonly IMediator _mediator;

        public CreateCaseEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Post("/admin/cases");
            Roles("Admin");
        }

        public override async Task HandleAsync(CreateCaseRequest req, CancellationToken ct)
        {
            var command = new CreateCaseCommand(req);
            var newCaseId = await _mediator.Send(command, ct);

            await SendAsync(newCaseId, 201, ct);
        }
    }
}
