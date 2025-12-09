using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace MailingPoC.Features.Emails.Services;

public class SesService(IAmazonSimpleEmailService amazonSimpleEmailService) : IEmailService
{

    public async Task<string> SendEmailAsync(Email email)
    {
        await VerifyEmailIdentityAsync(email.SenderAddress);

        var messageId = "";
        try
        {
            var response = await amazonSimpleEmailService.SendEmailAsync(
                new SendEmailRequest
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
                });
            messageId = response.MessageId;
        }
        catch (Exception ex)
        {
            Console.WriteLine("SendEmailAsync failed with exception: " + ex.Message);
        }

        return messageId;
    }
    private async Task<bool> VerifyEmailIdentityAsync(string recipientEmailAddress)
    {
        var success = false;
        try
        {
            var response = await amazonSimpleEmailService.VerifyEmailIdentityAsync(
                new VerifyEmailIdentityRequest
                {
                    EmailAddress = recipientEmailAddress
                });

            success = response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine("VerifyEmailIdentityAsync failed with exception: " + ex.Message);
        }

        return success;
    }
}