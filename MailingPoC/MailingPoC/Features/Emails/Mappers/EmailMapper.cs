using MailingPoC.Features.Emails.Requests.SendEmail;
using MailingPoC.Features.Emails.Services;

namespace MailingPoC.Features.Emails.Mappers;

public static class EmailMapper
{
    extension(SendEmailRequest request)
    {
        public Email ToEmail()
        {
            return new Email
            {
                SenderAddress = request.SenderAddress,
                Subject = request.Subject,
                BodyText = request.BodyText,
                BodyHtml = request.BodyHtml,
                ToAddresses = request.ToAddresses,
                CcAddresses = request.CcAddresses,
                BccAddresses = request.BccAddresses
            };
        }
    }
    
}