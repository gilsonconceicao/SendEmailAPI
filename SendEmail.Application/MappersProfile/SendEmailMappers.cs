using AutoMapper;
using SendEmail.Application.Emails.Dtos;
using SendEmail.Domain.Models;

namespace SendEmail.Application.MappersProfile
{
    public class SendEmailMappers : Profile
    {
        public SendEmailMappers()
        {
            // commands 
            CreateMap<SendEmailCommandDto, SendEmailModel>(); 
        }
    }
}