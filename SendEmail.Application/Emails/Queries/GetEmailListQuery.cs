using MediatR;

namespace SendEmail.Application.Emails.Queries
{
    public class GetEmailListQuery : IRequest
    {

    }

    public class GetEmailListQueryHandler : IRequestHandler<GetEmailListQuery>
    {

        public Task Handle(GetEmailListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}