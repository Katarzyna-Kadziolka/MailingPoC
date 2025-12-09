namespace MailingPoC.Features.Emails.Requests.SendEmail;

public class SendEmailRequest
{
    public List<string> ToAddresses { get; set; } = new();
    public List<string> CcAddresses { get; set; } = new();
    public List<string> BccAddresses { get; set; } = new();
    public required string BodyHtml { get; set; }
    public required string BodyText { get; set; }
    public required string Subject { get; set; }
    public required string SenderAddress { get; set; }
}