using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.LoginWithRefreshToken;

internal sealed class LoginWithRefreshTokenHandler(IApplicationDbContext context, ITokenProvider tokenProvider) : ICommandHandler<LoginWithRefreshTokenCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result.Failure<AuthenticationResponse>(RefreshTokenErrors.RefreshTokenHasExpired);
        }

        string accessToken = tokenProvider.Create(refreshToken.User);

        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        await context.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponse(accessToken, refreshToken.Token);
    }
}
