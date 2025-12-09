using MailingPoC.Services;
using MailingPoC.Ses;
using Microsoft.AspNetCore.Mvc;

namespace MailingPoC.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService)
{

    [HttpPost]
    public async Task<string> SendEmail([FromBody]Email email)
    {
        return await emailService.SendEmailAsync(email);
    }
}