using System.Net.Mime;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.SignIn;

internal sealed class SignInCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<SignInCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<AuthenticationResponse>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<AuthenticationResponse>(UserErrors.IncorrectPassword);
        }

        string token = tokenProvider.Create(user);

        var refreshToken = new RefreshToken
        {
            UserID = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };

        context.RefreshTokens.Add(refreshToken);

        await context.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponse {AccessToken = token, RefreshToken = refreshToken.Token };
    }
}
