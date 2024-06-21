using System.Net;
using System.Net.Mail;

namespace SendEmail.Domain.Services;

public class SmtpServices
{
    public static void SendEmailBySmtpClient(
        string FromEmailAddress, 
        string ToEmailAddress, 
        string SubjectEmail, 
        string BodyEmail 
    )
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
                From = new MailAddress(FromEmailAddress),
                Subject = SubjectEmail,
                Body = BodyEmail,
                IsBodyHtml = true
            };

            customMailMessage.To.Add(ToEmailAddress);

            startSmptClient.SendAsync(customMailMessage, "");
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            throw new Exception(errorMessage);
        }

    }
}
