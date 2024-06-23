using FluentValidation;
using SendEmail.Application.Emails.Commands;

namespace SendEmail.Application.Validations
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.From).NotNull();
            RuleFor(x => x.To).NotNull();
            RuleFor(x => x.Subject).NotNull();
            RuleFor(x => x.Body).NotNull();
        }
    }
}