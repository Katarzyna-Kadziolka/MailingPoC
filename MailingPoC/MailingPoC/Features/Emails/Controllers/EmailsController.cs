using MailingPoC.Features.Emails.Mappers;
using MailingPoC.Features.Emails.Requests.SendEmail;
using MailingPoC.Features.Emails.Requests.SendOrderEmail;
using MailingPoC.Features.Emails.Services;
using Microsoft.AspNetCore.Mvc;
using SystemFile = System.IO.File;

namespace MailingPoC.Features.Emails.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService) : ControllerBase
{
    [HttpPost(nameof(SendEmail))]
    public async Task<SendEmailResult> SendEmail([FromBody]SendEmailRequest request)
    {
        return await emailService.SendEmailAsync(request.ToEmail(), HttpContext.RequestAborted);
    }

    [HttpPost(nameof(SendOrderEmail))]
    public async Task<SendEmailResult> SendOrderEmail([FromBody]SendOrderEmailRequest request)
    {
        var bodyHtml = await SystemFile.ReadAllTextAsync("Features/Emails/Templates/TestEmail.html", HttpContext.RequestAborted);
        var bodyText = await SystemFile.ReadAllTextAsync("Features/Emails/Templates/TestEmail.txt", HttpContext.RequestAborted);

        var email = new Email
        {
            BodyHtml = bodyHtml,
            BodyText = bodyText,
            Subject = "Order",
            SenderAddress = "noreply@poc.com",
            ToAddresses = ["test@test.com"]
        };
        
        return await emailService.SendEmailAsync(email, HttpContext.RequestAborted);
    }
}
