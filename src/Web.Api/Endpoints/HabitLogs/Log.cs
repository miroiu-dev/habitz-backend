using Application.Abstractions.Authentication;
using Application.HabitLogs.Log;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.HabitLogs;

public class Log : IEndpoint
{
    public sealed record Request(int HabitLogID, bool IsCompleted);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("habit-logs", async (Request request, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new LogCommand(request.HabitLogID, userContext.UserId, request.IsCompleted);

            Result<LogResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.HabitLogs);
    }
}
