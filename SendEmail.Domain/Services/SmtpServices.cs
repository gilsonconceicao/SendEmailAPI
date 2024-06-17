using System.Net;
using System.Net.Mail;
using SendEmail.Domain.Contracts;

namespace SendEmail.Domain.Services;

public class SmtpServices
{
    public static void SendEmailAsync(ISendEmail EmailData)
    {
        try
        {
            var credentialsSmtp =  new CredentialsSmtp();

            var startSmptClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(credentialsSmtp.EmailSender, credentialsSmtp.PasswordSender),
                EnableSsl = true
            };

            var customMailMessage = new MailMessage
            {
                From = new MailAddress(EmailData.FromEmailAddress),
                Subject = EmailData.SubjectEmail,
                Body = EmailData.BodyEmail,
                IsBodyHtml = true
            };

            customMailMessage.To.Add(EmailData.ToEmailAddress);

            startSmptClient.Send(customMailMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            throw new Exception(errorMessage);
        }

    }
}
