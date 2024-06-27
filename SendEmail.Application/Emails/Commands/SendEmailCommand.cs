using AutoMapper;
using FluentValidation;
using MediatR;
using SendEmail.Application.Emails.Dtos;
using SendEmail.Domain.Contracts;
using SendEmail.Domain.Models;
using SendEmail.Infrastructure.Contexts;

namespace SendEmail.Application.Emails.Commands
{
    public class SendEmailCommand : IRequest<bool>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IValidator<SendEmailCommand> _validator;
        private readonly ISmtpService _SendEmailService;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public SendEmailCommandHandler(
            Context context,
            IMapper mapper,
            IValidator<SendEmailCommand> validator,
            ISmtpService smtpService
        )
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _SendEmailService = smtpService;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                await _SendEmailService.SendEmailAsync(request.From, request.To, request.Subject, request.Body);

                SendEmailCommandDto emailDto = new SendEmailCommandDto()
                {
                    FromEmailAddress = request.From,
                    ToEmailAddress = request.To,
                    Subject = request.Subject,
                    Body = request.Body
                };

                var emailModel = _mapper.Map<SendEmailModel>(emailDto);
                _context.SendEmails.Add(emailModel);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}