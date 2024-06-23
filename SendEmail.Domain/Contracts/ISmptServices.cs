namespace SendEmail.Domain.Contracts
{
    public interface ISmtpService
    {
        Task SendEmailAsync(string from, string to, string subject, string body);
    }
}