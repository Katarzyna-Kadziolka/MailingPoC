using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using SendEmailRequest = Amazon.SimpleEmail.Model.SendEmailRequest;

namespace MailingPoC.Features.Emails.Services;

public class SesService(IAmazonSimpleEmailService amazonSimpleEmailService) : IEmailService
{
    public async Task<SendEmailResult> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var request = CreateSendEmailRequest(email);

        await amazonSimpleEmailService.SendEmailAsync(request, cancellationToken);

        return new SendEmailResult { IsSent = true };
    }

    private static SendEmailRequest CreateSendEmailRequest(Email email)
    {
        return new SendEmailRequest
        {
            Destination = new Destination
            {
                BccAddresses = email.BccAddresses,
                CcAddresses = email.CcAddresses,
                ToAddresses = email.ToAddresses
            },
            Message = new Message
            {
                Body = new Body
                {
                    Html = new Content
                    {
                        Charset = "UTF-8",
                        Data = email.BodyHtml
                    },
                    Text = new Content
                    {
                        Charset = "UTF-8",
                        Data = email.BodyText
                    }
                },
                Subject = new Content
                {
                    Charset = "UTF-8",
                    Data = email.Subject
                }
            },
            Source = email.SenderAddress
        };
    }
}
