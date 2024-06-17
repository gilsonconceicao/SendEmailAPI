using MediatR;
using SendEmail.Domain.Contracts;

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
        public Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}