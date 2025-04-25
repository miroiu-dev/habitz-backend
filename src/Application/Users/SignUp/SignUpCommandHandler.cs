using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.Register;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.SignUp;

internal sealed class SignUpCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
    : ICommandHandler<SignUpCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<AuthenticationResponse>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Email = command.Email,
            Age = command.Age,
            ActivityLevel = command.ActivityLevel,
            FullName = command.FullName,
            Gender = command.Gender,
            Goal = command.Goal,
            GoalWeight = command.GoalWeight,
            Height = command.Height,
            WeeklyGoal = command.Goal == Goal.Maintain ? null :  command.WeeklyGoal,
            Weight = command.Weight,
            Habits = [],
            PasswordHash = passwordHasher.Hash(command.Password)
        };

        context.Users.Add(user);

        var refreshToken = new RefreshToken
        {
            User = user,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };

        context.RefreshTokens.Add(refreshToken);

        await context.SaveChangesAsync(cancellationToken);
        
        string token = tokenProvider.Create(user);

        return new AuthenticationResponse { AccessToken = token, RefreshToken = refreshToken.Token };
    }
}
