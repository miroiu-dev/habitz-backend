using Application.Abstractions.Authentication;
using Application.BodyCompositionLogs.GetNewest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BodyMeasurementLogs;

public class Newest : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("body-measurement-logs/newest", async (ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var command = new NewestQuery(userContext.UserId);

            Result<NewestResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.BodyMeasurementLogs);
    }
}
