using FluentValidation;

namespace WorkOMat.Application.Features.Auth.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("DisplayName.Required")
            .MaximumLength(50).WithMessage("DisplayName.TooLong");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email.Required")
            .EmailAddress().WithMessage("Email.InvalidFormat");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password.Required")
            .MinimumLength(8).WithMessage("Password.TooShort")
            .Matches(@"[a-z]").WithMessage("Password.NeedsLowercase")
            .Matches(@"[A-Z]").WithMessage("Password.NeedsUppercase")
            .Matches(@"[0-9]").WithMessage("Password.NeedsNumber")
            .Matches(@"[!@#$%^&*()-+]").WithMessage("Password.NeedsSpecialCharacter");
    }
}