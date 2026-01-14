namespace MailingPoC.Features.Emails.Options;

public class SmtpOptions
{
    public const string SectionName = "SMTP";
    public bool UseLocalSmtp { get; set; }
}