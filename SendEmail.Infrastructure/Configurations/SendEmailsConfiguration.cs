using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendEmail.Domain.Models;

namespace SendEmail.Infrastructure.Configurations; 

public class SendEmailsConfiguration : IEntityTypeConfiguration<SendEmailModel>
{
    public void Configure(EntityTypeBuilder<SendEmailModel> builder)
    {

    }
}