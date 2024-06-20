namespace SendEmail.Domain.Models
{
    public class SendEmailModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}