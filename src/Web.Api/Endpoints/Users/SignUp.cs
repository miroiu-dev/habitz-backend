using Application.Users.Register;
using Application.Users.SignUp;
using Domain.Users;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class SignUp : IEndpoint
{
    public sealed record Request(string Password, string Email, string FullName, int Age, ActivityLevel ActivityLevel, Gender Gender, Goal Goal, int? GoalWeight, int Height, decimal WeeklyGoal, int Weight);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/sign-up", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new SignUpCommand(
                request.Password,
                request.Email,
                request.FullName,
                request.Age,
                request.ActivityLevel,
                request.Gender,
                request.Goal,
                request.GoalWeight,
                request.Height,
                request.WeeklyGoal,
                request.Weight
                );

            Result<AuthenticationResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
