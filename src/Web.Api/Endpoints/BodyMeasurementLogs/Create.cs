using Application.Abstractions.Authentication;
using Application.BodyMeasurementLogs.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BodyMeasurementLogs;

public class Create : IEndpoint
{
    public record Request(decimal Neck, decimal Shoulder, decimal LeftBiceps, decimal RightBiceps, decimal Chest, decimal Waist, decimal Abs, decimal Hip, decimal LeftTight, decimal RightTigh, decimal LeftCalf, decimal RightCalf);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("body-measurement-logs", async (Request request, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var command = new CreateCommand(userContext.UserId, request.Neck, request.Shoulder, request.LeftBiceps, request.RightBiceps, request.Chest, request.Waist, request.Abs, request.Hip, request.LeftTight, request.RightTigh, request.LeftCalf, request.RightCalf);

            Result<CreateResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.BodyMeasurementLogs);
    }
}
