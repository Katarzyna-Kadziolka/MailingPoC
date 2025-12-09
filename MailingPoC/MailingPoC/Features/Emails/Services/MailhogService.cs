using MailKit.Net.Smtp;
using MimeKit;

namespace MailingPoC.Features.Emails.Services;

public class MailhogService : IEmailService
{
    public async Task<string> SendEmailAsync(Email email)
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
        await client.ConnectAsync("localhost", 1025, false);

        var messageId = await client.SendAsync(message);
        await client.DisconnectAsync(true);
        
        return messageId;
    }
}
