using MailingPoC.Features.Emails.Mappers;
using MailingPoC.Features.Emails.Requests.SendEmail;
using MailingPoC.Features.Emails.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailingPoC.Features.Emails.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService) : ControllerBase
{
    [HttpPost]
    public async Task<SendEmailResult> SendEmail([FromBody]SendEmailRequest request)
    {
        return await emailService.SendEmailAsync(request.ToEmail(), HttpContext.RequestAborted);
    }
}
