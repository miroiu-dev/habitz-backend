using Application.Abstractions.Authentication;
using Application.BodyMeasurementLogs.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BodyMeasurementLogs;

public class Delete: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("body-measurement-logs/{bodyCompositionLogId}", async (int bodyCompositionLogId, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var query = new DeleteCommand(bodyCompositionLogId, userContext.UserId);

            Result<DeleteResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.BodyMeasurementLogs);
    }
}
