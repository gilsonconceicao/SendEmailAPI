using System.Net;
using System.Net.Mail;

namespace SendEmail.Domain.Services;

public class SmtpServices
{
    public static void SendEmailBySmtpClient(SendEmailBySmtpDto EmailData)
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
                From = new MailAddress(EmailData.FromEmailAddress),
                Subject = EmailData.SubjectEmail,
                Body = EmailData.BodyEmail,
                IsBodyHtml = true
            };

            customMailMessage.To.Add(EmailData.ToEmailAddress);

            startSmptClient.SendAsync(customMailMessage, "");
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            throw new Exception(errorMessage);
        }

    }

    public class SendEmailBySmtpDto
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string SubjectEmail { get; set; }
        public string BodyEmail { get; set; }
    }
}
