using MailKit.Net.Smtp;
using MimeKit;

namespace MailingPoC.Features.Emails.Services;

public class MailhogService : IEmailService
{
    public async Task<SendEmailResult> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var message = CreateEmailMessage(email);

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("localhost", 1025, false, cancellationToken);

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
        return new SendEmailResult
        {
            IsSent = true
        };
    }

    private static MimeMessage CreateEmailMessage(Email email)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(email.SenderAddress, email.SenderAddress));
        message.To.Add(new MailboxAddress(email.ToAddresses[0], email.ToAddresses[0]));
        message.Subject = email.Subject;
        
        var builder = new BodyBuilder
        {
            HtmlBody = email.BodyHtml,
            TextBody = email.BodyText
        };

        message.Body = builder.ToMessageBody();
        return message;
    }
}
