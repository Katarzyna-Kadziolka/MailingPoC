using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MailingPoC.Ses;

namespace MailingPoC.Services;

public class SesService(IAmazonSimpleEmailService amazonSimpleEmailService) : IEmailService
{

    public async Task<string> SendEmailAsync(SendEmailArgs args)
    {
        await VerifyEmailIdentityAsync(args.SenderAddress);

        var messageId = "";
        try
        {
            var response = await amazonSimpleEmailService.SendEmailAsync(
                new SendEmailRequest
                {
                    Destination = new Destination
                    {
                        BccAddresses = args.BccAddresses,
                        CcAddresses = args.CcAddresses,
                        ToAddresses = args.ToAddresses
                    },
                    Message = new Message
                    {
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = args.BodyHtml
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = args.BodyText
                            }
                        },
                        Subject = new Content
                        {
                            Charset = "UTF-8",
                            Data = args.Subject
                        }
                    },
                    Source = args.SenderAddress
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