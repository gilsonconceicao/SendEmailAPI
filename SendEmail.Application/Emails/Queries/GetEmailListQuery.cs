using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendEmail.Application.Emails.Dtos;
using SendEmail.Domain.Models;
using SendEmail.Infrastructure.Contexts;

namespace SendEmail.Application.Emails.Queries
{
    public class GetEmailListQuery : IRequest<List<GetEmailListDto>>
    {
        
    }

    public class GetEmailListQueryHandler : IRequestHandler<GetEmailListQuery, List<GetEmailListDto>>
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public GetEmailListQueryHandler(Context context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetEmailListDto>> Handle(GetEmailListQuery request, CancellationToken cancellationToken)
        {
            var queryData = await _context.SendEmails.ToListAsync();
            return _mapper.Map<List<GetEmailListDto>>(queryData);
        }
    }
}