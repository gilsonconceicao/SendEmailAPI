namespace SendEmail.Domain.Contracts
{
    public interface ISendEmail
    {
        string FromEmailAddress { get; set; }
        string ToEmailAddress { get; set; }
        string SubjectEmail { get; set; }
        string BodyEmail { get; set; }
    }
}