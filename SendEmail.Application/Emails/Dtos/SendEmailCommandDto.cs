namespace SendEmail.Application.Emails.Dtos
{
    public class SendEmailCommandDto
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}