using MailingPoC.Ses;

namespace MailingPoC.Services;

public interface IEmailService
{
    public Task<string> SendEmailAsync(Email email);
}