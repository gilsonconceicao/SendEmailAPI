using AutoMapper;
using SendEmail.Application.MappersProfile;
using SendEmail.Domain.Models;

#nullable disable
namespace SendEmail.Application.Emails.Dtos
{
    public class GetEmailListDto : IMapFrom<SendEmailModel>
    {
        public Guid Id { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SendEmailModel, GetEmailListDto>()
                .ForMember(c => c.ToEmail, opt => opt.MapFrom(src => src.ToEmailAddress))
                .ForMember(c => c.FromEmail, opt => opt.MapFrom(src => src.FromEmailAddress));
        }
    }
}