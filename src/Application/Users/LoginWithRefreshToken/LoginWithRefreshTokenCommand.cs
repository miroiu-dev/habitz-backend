using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Users.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommand(string RefreshToken) : ICommand<AuthenticationResponse>;
