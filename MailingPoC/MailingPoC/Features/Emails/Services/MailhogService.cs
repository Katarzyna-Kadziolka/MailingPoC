using MailingPoC.Features.Emails.Requests.SendEmail;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailingPoC.Features.Emails.Services;

public class MailhogService : IEmailService
{
    public async Task<SendEmailResult> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var bodyHtml = await File.ReadAllTextAsync("Features/Emails/Templates/TestEmail.html");
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(email.SenderAddress, email.SenderAddress));
        message.To.Add(new MailboxAddress(email.ToAddresses[0], email.ToAddresses[0]));
        message.Subject = email.Subject;

        message.Body = new TextPart("html")
        {
            Text = bodyHtml
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("localhost", 1025, false, cancellationToken);

        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
        
        return new SendEmailResult()
        {
            IsSent = true
        };
    }
}
