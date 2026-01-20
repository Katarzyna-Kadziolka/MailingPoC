namespace MailingPoC.Features.Emails.Options;

public class SmtpOptions
{
    public const string SectionName = "Smtp";
    public bool UseLocalSmtp { get; init; }
}
