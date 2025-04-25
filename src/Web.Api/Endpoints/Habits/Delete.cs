using Application.Abstractions.Authentication;
using Application.Habits.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Habits;

public class Delete :IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("habits/{habitId}", async (int habitId, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new DeleteCommand(habitId, userContext.UserId);

            Result<DeleteResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Users);
    }
}
