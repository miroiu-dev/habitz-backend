using Application.Users.LoginWithRefreshToken;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
namespace Web.Api.Endpoints.Users;

internal sealed class LoginWithRefreshToken : IEndpoint
{
    public sealed record Request(string RefreshToken);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh-token", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginWithRefreshTokenCommand(request.RefreshToken);

            Result<AuthenticationResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
