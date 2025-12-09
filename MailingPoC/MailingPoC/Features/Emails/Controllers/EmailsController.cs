using MailingPoC.Features.Emails.Mappers;
using MailingPoC.Features.Emails.Requests.SendEmail;
using MailingPoC.Features.Emails.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailingPoC.Features.Emails.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService)
{
    [HttpPost]
    public async Task<string> SendEmail([FromBody]SendEmailRequest request)
    {
        return await emailService.SendEmailAsync(request.ToEmail());
    }
}
