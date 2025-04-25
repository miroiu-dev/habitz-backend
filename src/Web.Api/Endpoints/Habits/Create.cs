using Application.Abstractions.Authentication;
using Application.Habits.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Habits;

public class Create : IEndpoint
{
    public sealed record Request(string Name, string Icon, string Color, TimeOnly? Reminder, List<int> Schedules);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("habits", async (Request request, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var command = new CreateCommand(userContext.UserId, request.Name, request.Icon, request.Color, request.Reminder, request.Schedules);

            Result<CreateResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Habits);
    }
}
