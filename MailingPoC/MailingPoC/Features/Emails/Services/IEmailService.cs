namespace MailingPoC.Features.Emails.Services;

public interface IEmailService
{
    Task<SendEmailResult> SendEmailAsync(Email email, CancellationToken cancellationToken = default);
}