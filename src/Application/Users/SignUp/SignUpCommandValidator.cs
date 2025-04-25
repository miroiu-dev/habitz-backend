using Application.Users.Register;
using Domain.Users;
using FluentValidation;

namespace Application.Users.SignUp;

internal sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Please enter your email address.")
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(c => c.Age)
           .NotEmpty()
           .WithMessage("Please enter your age.")
           .GreaterThanOrEqualTo(17)
           .WithMessage("You must be at least 17 years old to use Habitz.")
           .LessThanOrEqualTo(120)
           .WithMessage("Please enter an age less than 120.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Please enter your password.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(20)
            .WithMessage("Password must be at most 20 characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d")
            .WithMessage("Password must contain at least one number.")
            .Matches("[@$!%*?&]")
            .WithMessage("Password must contain at least one special character.");

        RuleFor(c => c.FullName)
            .NotEmpty()
            .WithMessage("Please enter your full name.")
            .Matches("^[a-zA-Z\\s'-]+$")
            .WithMessage("Full name can only contain letters, spaces, apostrophes, or hyphens");

        RuleFor(c => c.ActivityLevel)
            .IsInEnum()
            .WithMessage("Please enter an activity level.");

        RuleFor(c => c.Gender)
            .IsInEnum()
            .WithMessage("Please enter your gender.");

        RuleFor(c => c.Goal)
            .IsInEnum()
            .WithMessage("Please enter your goal.");

        RuleFor(c => c.WeeklyGoal)
            .NotEmpty()
            .WithMessage("Please enter your weekly goal.")
            .Must(value => value == 0.25m || value == 0.50m || value == 0.75m || value == 1m)
            .WithMessage("Weekly goal must be one of the following values: 0.25, 0.50, 0.75, or 1.");

        RuleFor(c => c.Weight)
            .NotEmpty()
            .WithMessage("Please enter your current weight.")
            .GreaterThanOrEqualTo(13)
            .WithMessage("Please enter an accurate current weight.")
            .LessThanOrEqualTo(454)
            .WithMessage("Please enter an accurate current weight.");

        RuleFor(c => c.Height)
            .NotEmpty()
            .WithMessage("Please enter an accurate height.")
            .GreaterThanOrEqualTo(92)
            .WithMessage("lease enter an accurate height.")
            .LessThanOrEqualTo(254)
            .WithMessage("lease enter an accurate height.");

        RuleFor(c => c.GoalWeight)
            .NotEmpty()
            .WithMessage("Please enter an estimated goal weight.")
            .LessThanOrEqualTo(454)
            .WithMessage("Your goal weight must be between your current weight and 454kg.")
             .Must((model, goalWeight) => goalWeight != model.Weight)
           .WithMessage("Your goal weight should be different from your current weight.")
              .Must((model, goalWeight) => goalWeight < model.Weight)
            .WithMessage("Your goal weight is higher than your current weight.")
            .When(c => c.Goal == Goal.Lose)
              .Must((model, goalWeight) => goalWeight > model.Weight)
            .WithMessage("This goal weight is lower than your current weight.")
            .When(c => c.Goal == Goal.Gain);

        RuleFor(x => x)
            .Custom((model, context) =>
            {
                if (model.GoalWeight is not null)
                {
                    decimal heightInMeters = model.Height / 100m;
                    decimal bmi = GetBodyMassIndex(model.GoalWeight.Value, heightInMeters);

                    if (model.Goal == Goal.Lose && IsUnderweight(bmi))
                    {
                        decimal minimumWeight = GetMinimumWeight(heightInMeters);
                        context.AddFailure(nameof(model.GoalWeight), $"This goal weight is considered underweight for your height. Please enter a goal weight of {minimumWeight}kg or higher.");
                    }
                }
            });
    }

    private decimal GetBodyMassIndex(decimal weight, decimal height)
    {
        return weight / (height * height);
    }

    private bool IsUnderweight(decimal bmi)
    {
        return bmi < 18.5m;
    }

    private decimal GetMinimumWeight(decimal height)
    {
        return 18.5m * height * height;
    }
}
