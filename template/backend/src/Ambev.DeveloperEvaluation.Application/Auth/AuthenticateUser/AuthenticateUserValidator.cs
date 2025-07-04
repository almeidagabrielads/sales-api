namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    using FluentValidation;

    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            this.RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
