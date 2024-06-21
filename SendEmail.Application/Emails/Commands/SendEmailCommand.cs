using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SendEmail.Application.Emails.Dtos;
using SendEmail.Domain.Models;
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
        private readonly IMapper _mapper;

        public SendEmailCommandHandler(
            Context context, 
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper; 
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            ValidationsSendEmailCommand validator = new ValidationsSendEmailCommand();
            ValidationResult result = validator.Validate(request);

            if (!result.IsValid)
            {
                throw new InvalidOperationException(String.Join(", ",result.Errors));
            }

            try
            {
                SmtpServices.SendEmailBySmtpClient(request.FromEmailAddress, request.ToEmailAddress, request.SubjectEmail, request.BodyEmail);
                
                SendEmailCommandDto EmailDto = new SendEmailCommandDto()
                {
                    FromEmailAddress = request.FromEmailAddress,
                    ToEmailAddress = request.ToEmailAddress,
                    Subject = request.SubjectEmail,
                    Body = request.BodyEmail
                }; 

                _context.SendEmails.Add(_mapper.Map<SendEmailModel>(EmailDto)); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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