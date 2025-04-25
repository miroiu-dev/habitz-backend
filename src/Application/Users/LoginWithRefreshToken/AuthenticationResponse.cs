using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.LoginWithRefreshToken;

public sealed record AuthenticationResponse(string AccessToken, string RefreshToken);
