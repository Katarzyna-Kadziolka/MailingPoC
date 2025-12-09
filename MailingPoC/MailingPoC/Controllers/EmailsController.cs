using MailingPoC.Services;
using MailingPoC.Ses;
using Microsoft.AspNetCore.Mvc;

namespace MailingPoC.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService)
{

    [HttpPost(nameof(SendEmail))]
    public async Task<string> SendEmail([FromBody]SendEmailArgs args)
    {
        return await emailService.SendEmailAsync(args);
    }
}