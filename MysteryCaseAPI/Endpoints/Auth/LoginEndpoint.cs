using FastEndpoints;
using MediatR;
using MysteryCaseApplication.Commands.Auth;
using MysteryCaseShared;
using MysteryCaseShared.DTOs.Auth.Response;

namespace MysteryCaseAPI.Endpoints.Auth
{
    public class LoginEndpoint : Endpoint<LoginRequest, AuthResponseDto>
    {
        private readonly IMediator _mediator;

        public LoginEndpoint(IMediator mediator) => _mediator = mediator;

        public override void Configure()
        {
            Post("/api/auth/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var command = new UserLoginCommand(req);
            try
            {
                var response = await _mediator.Send(command, ct);
                await SendOkAsync(response, ct);
            }
            catch (UnauthorizedAccessException ex)
            {
                await SendUnauthorizedAsync(ct);
            }
            catch (Exception ex)
            {
                await SendErrorsAsync(cancellation: ct);
            }
        }
    }
}
