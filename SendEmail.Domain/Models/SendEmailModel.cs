namespace SendEmail.Domain.Models
{
    public class SendEmailModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EmailAddres { get; set; }
    }
}