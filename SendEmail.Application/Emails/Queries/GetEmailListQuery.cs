using MediatR;
using Microsoft.EntityFrameworkCore;
using SendEmail.Domain.Models;
using SendEmail.Infrastructure.Contexts;

namespace SendEmail.Application.Emails.Queries
{
    public class GetEmailListQuery : IRequest<List<SendEmailModel>>
    {
        
    }

    public class GetEmailListQueryHandler : IRequestHandler<GetEmailListQuery, List<SendEmailModel>>
    {
        private readonly Context _context;
        public GetEmailListQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<List<SendEmailModel>> Handle(GetEmailListQuery request, CancellationToken cancellationToken)
        {
            return await _context.SendEmails.ToListAsync();
        }
    }
}