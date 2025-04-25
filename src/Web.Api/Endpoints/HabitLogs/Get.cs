using Application.Abstractions.Authentication;
using Application.HabitLogs.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.HabitLogs;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("habit-logs", async (ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new GetQuery(userContext.UserId);

            Result<GetResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.HabitLogs);
    }
}
