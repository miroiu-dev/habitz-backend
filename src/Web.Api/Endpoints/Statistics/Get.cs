using Application.Abstractions.Authentication;
using Application.Statistics.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Statistics;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("statistics", async (ISender sender, CancellationToken cancellationToken, IUserContext userContext) =>
        {
            var command = new GetQuery(userContext.UserId);
            Result<GetResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Notifications);
    }
}
