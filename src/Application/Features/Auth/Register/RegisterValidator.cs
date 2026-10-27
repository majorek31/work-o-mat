using FluentValidation;

namespace WorkOMat.Application.Features.Auth.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty()
                .WithErrorCode("DisplayName.Required")
                .WithMessage("DisplayName is required.")
            .MaximumLength(50)
                .WithErrorCode("DisplayName.TooLong")
                .WithMessage("DisplayName is too long. Maximum length is 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode("Email.Required")
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode("Email.InvalidFormat")
                .WithMessage("Email is in an invalid format.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithErrorCode("Password.Required")
                .WithMessage("Password is required.")
            .MinimumLength(8)
                .WithErrorCode("Password.TooShort")
                .WithMessage("Password is too short. Minimum length is 8 characters.")
            .Matches(@"[a-z]")
                .WithErrorCode("Password.NeedsLowercase")
                .WithMessage("Password needs at least one lowercase letter.")
            .Matches(@"[A-Z]")
                .WithErrorCode("Password.NeedsUppercase")
                .WithMessage("Password needs at least one uppercase letter.")
            .Matches(@"[0-9]")
                .WithErrorCode("Password.NeedsNumber")
                .WithMessage("Password needs at least one number.")
            .Matches(@"[!@#$%^&*()-+]")
                .WithErrorCode("Password.NeedsSpecialCharacter")
                .WithMessage("Password needs at least one special character.");
    }
}