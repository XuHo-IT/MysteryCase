using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Clues;
using MysteryCaseShared.DTOs;
using System.Security.Claims;

namespace MysteryCaseAPI.Endpoints.Clues
{
    public class UnlockClueEndpoint : EndpointWithoutRequest<ClueDto>
    {
        private readonly IMediator _mediator;

        public UnlockClueEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Post("/clues/{ClueId}/unlock");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var clueId = Route<Guid>("ClueId");
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var command = new UnlockClueCommand(userId, clueId);
            try
            {
                var result = await _mediator.Send(command, ct);
                await SendOkAsync(result, ct);
            }
            catch (InvalidOperationException ex)
            {
                AddError(ex.Message);
                await SendErrorsAsync(400, ct);
            }
            catch (KeyNotFoundException)
            {
                await SendNotFoundAsync(ct);
            }
        }
    }
}
