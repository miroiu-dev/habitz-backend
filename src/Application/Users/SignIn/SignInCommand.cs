using Application.Abstractions.Messaging;

namespace Application.Users.SignIn;

public sealed record SignInCommand(string Email, string Password) : ICommand<AuthenticationResponse>;
