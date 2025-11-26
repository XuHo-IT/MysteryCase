using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Auth;
using MysteryCaseShared;

namespace MysteryCaseAPI.Endpoints.Auth
{
    public class RegisterEndpoint : Endpoint<RegisterRequest, Guid>
    {
        private readonly IMediator _mediator;

        public RegisterEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Post("/api/auth/register");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            var command = new UserRegisterCommand(req);
            var userId = await _mediator.Send(command, ct);

            await SendAsync(userId, 201, ct);
        }
    }
}
