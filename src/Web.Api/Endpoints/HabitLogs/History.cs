using Application.Abstractions.Authentication;
using Application.HabitLogs.History;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.HabitLogs;

public class History: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("habit-logs/history", async (ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new HistoryQuery(userContext.UserId);

            Result<HistoryResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.HabitLogs);
    }
}
