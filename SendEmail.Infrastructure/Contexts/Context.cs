using Microsoft.EntityFrameworkCore;
using SendEmail.Domain.Models;
using SendEmail.Infrastructure.Configurations;

namespace SendEmail.Infrastructure.Contexts;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }
    public DbSet<SendEmailModel> SendEmails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SendEmailsConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}