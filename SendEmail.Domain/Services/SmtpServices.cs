using System.Net;
using System.Net.Mail;
using SendEmail.Domain.Contracts;

namespace SendEmail.Domain.Services;

public class SmtpServices : ISmtpService
{
    public async Task SendEmailAsync(string from, string to, string subject, string body)
    {
        try
        {
            var credentialsSmtp = new CredentialsSmtp();

            var startSmptClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(credentialsSmtp.EmailSender, credentialsSmtp.PasswordSender),
                EnableSsl = true
            };

            var customMailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            customMailMessage.To.Add(to);

            await startSmptClient.SendMailAsync(customMailMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            throw new Exception(errorMessage);
        }
    }
}
