using Application.Abstractions.Authentication;
using Application.BodyMeasurementLogs.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BodyMeasurementLogs;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("body-measurement-logs", async ([FromQuery] DateTime? date, ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            DateTime actualDate = date.HasValue
                  ? DateTime.SpecifyKind(date.Value, DateTimeKind.Utc)
                  : DateTime.UtcNow;

            var command = new GetQuery(userContext.UserId, actualDate);

            Result<GetResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.BodyMeasurementLogs);
    }
}
