using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Cases;
using MysteryCaseDomain;
using MysteryCaseShared.DTOs.Cases.Request;
using MysteryCaseShared.DTOs.Cases.Response;
using System.Security.Claims;

namespace MysteryCaseAPI.Endpoints.Cases
{
    public class SubmitAnswerEndpoint : Endpoint<SubmitAnswerRequest, SubmitAnswerResponseDto>
    {
        private readonly IMediator _mediator;

        public SubmitAnswerEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Post("/cases/submit");
        }

        public override async Task HandleAsync(SubmitAnswerRequest req, CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var command = new SubmitAnswerCommand(req, userId);
            try
            {
                var result = await _mediator.Send(command, ct);
                await SendOkAsync(result, ct);
            }
            catch (KeyNotFoundException)
            {
                await SendNotFoundAsync(ct);
            }
            catch (Exception)
            {
                await SendErrorsAsync(500, ct);
            }
        }
    }
}
