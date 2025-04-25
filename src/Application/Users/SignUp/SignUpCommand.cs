using Application.Abstractions.Messaging;
using Application.Users.SignUp;
using Domain.Users;

namespace Application.Users.Register;

public sealed record SignUpCommand(string Password, string Email, string FullName, int Age, ActivityLevel ActivityLevel, Gender Gender, Goal Goal, int? GoalWeight, int Height, decimal WeeklyGoal, int Weight)
    : ICommand<AuthenticationResponse>;
