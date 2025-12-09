namespace MailingPoC.Features.Emails.Services;

public interface IEmailService
{
    public Task<string> SendEmailAsync(Email email);
}