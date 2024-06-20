using FluentValidation;
using MediatR;
using SendEmail.Domain.Services;
using SendEmail.Infrastructure.Contexts;

namespace SendEmail.Application.Emails.Commands
{
    public class SendEmailCommand : IRequest<bool>
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string SubjectEmail { get; set; }
        public string BodyEmail { get; set; }

        public SendEmailCommand(string fromEmailAddress, string toEmailAddress, string subjectEmail, string bodyEmail)
        {
            this.FromEmailAddress = fromEmailAddress;
            this.ToEmailAddress = toEmailAddress;
            this.SubjectEmail = subjectEmail;
            this.BodyEmail = bodyEmail;
        }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly Context _context;
        private readonly IValidator<SendEmailCommand> _validator;

        public SendEmailCommandHandler(
            Context context, 
            IValidator<SendEmailCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            try
            {
                var sendEmailObject = new SmtpServices.SendEmailBySmtpDto 
                {
                    BodyEmail = request.BodyEmail,
                    FromEmailAddress = request.FromEmailAddress,
                    ToEmailAddress = request.ToEmailAddress,
                    SubjectEmail = request.SubjectEmail
                }; 

                SmtpServices.SendEmailBySmtpClient(sendEmailObject);

                return true; 
            }
            catch (Exception ex)
            {
                throw new ValidationException($"Não foi possível enviar o e-mail: {ex.Message}");
            }
        }
    }

    public class ValidationsSendEmailCommand : AbstractValidator<SendEmailCommand>
    {
        public ValidationsSendEmailCommand()
        {
            RuleFor(x => x.FromEmailAddress).NotNull();
            RuleFor(x => x.ToEmailAddress).NotNull();
            RuleFor(x => x.SubjectEmail).NotNull();
            RuleFor(x => x.BodyEmail).NotNull();
        }
    }
}