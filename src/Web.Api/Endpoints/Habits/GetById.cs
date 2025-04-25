using Application.Abstractions.Authentication;
using Application.Habits.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Habits;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("habits/{habitId}", async (int habitId, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new GetByIdQuery(habitId, userContext.UserId);

            Result<HabitResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Users);
    }
}
