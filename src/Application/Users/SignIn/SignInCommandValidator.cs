using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Users.Register;
using FluentValidation;

namespace Application.Users.SignIn;
internal sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(c => c.Email)
          .NotEmpty()
          .WithMessage("Please enter your email address.")
          .EmailAddress()
          .WithMessage("Please enter a valid email address.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Please enter your password.");
    }
}
