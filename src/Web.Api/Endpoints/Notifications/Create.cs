using Application.Abstractions.Authentication;
using Application.Notifications.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Notifications;

public class Create : IEndpoint
{
    public sealed record Request(int HabitId, string Title, string Description);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("notifications", async (Request request, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var command = new CreateCommand(request.HabitId, userContext.UserId, request.Title, request.Description);
            Result<CreateResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Notifications);
    }
}
