using MailingPoC.Ses;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailingPoC.Services;

public class MailhogService : IEmailService
{
    public async Task<string> SendEmailAsync(SendEmailArgs args)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(args.SenderAddress, args.SenderAddress));
        message.To.Add(new MailboxAddress(args.ToAddresses[0], args.ToAddresses[0]));
        message.Subject = args.Subject;

        message.Body = new TextPart("html")
        {
            Text = args.BodyHtml
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("localhost", 1025, false);

        var messageId = await client.SendAsync(message);
        await client.DisconnectAsync(true);
        
        return messageId;
    }
}
