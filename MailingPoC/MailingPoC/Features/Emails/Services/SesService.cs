using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using SendEmailRequest = Amazon.SimpleEmail.Model.SendEmailRequest;

namespace MailingPoC.Features.Emails.Services;

public class SesService(IAmazonSimpleEmailService amazonSimpleEmailService) : IEmailService
{

    public async Task<SendEmailResult> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        try
        {
            await VerifyEmailIdentityAsync(email.SenderAddress, cancellationToken);

            var request = CreateSendEmailRequest(email);

            await amazonSimpleEmailService.SendEmailAsync(request, cancellationToken);
            
            return new SendEmailResult { IsSent = true };
        }
        catch (AmazonSimpleEmailServiceException ex)
        {
            return new SendEmailResult 
            { 
                IsSent = false, 
                Exception = new Exception($"SES Error: {ex.ErrorCode} - {ex.Message}", ex) 
            };
        }
        catch (AmazonServiceException ex)
        {
            return new SendEmailResult 
            { 
                IsSent = false, 
                Exception = new Exception($"AWS Service Error: {ex.Message}", ex) 
            };
        }
        catch (Exception ex)
        {
            return new SendEmailResult
            {
                IsSent = false,
                Exception = new Exception($"{nameof(SesService)} Error: {ex.Message}" , ex)
            };
        }
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

    private async Task VerifyEmailIdentityAsync(string recipientEmailAddress, CancellationToken cancellationToken = default)
    {
        await amazonSimpleEmailService.VerifyEmailIdentityAsync(
            new VerifyEmailIdentityRequest
            {
                EmailAddress = recipientEmailAddress
            },
            cancellationToken
        );
    }
}
