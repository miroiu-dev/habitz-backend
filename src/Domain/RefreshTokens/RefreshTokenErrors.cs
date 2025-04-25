using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.RefreshTokens;

public static class RefreshTokenErrors
{
    public static Error RefreshTokenHasExpired => Error.Failure(
       "Users.RefreshTokenHasExpired",
       "The refresh token has expired.");
}
