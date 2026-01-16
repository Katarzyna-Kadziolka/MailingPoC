using MailingPoC.Features.Emails.Mappers;
using MailingPoC.Features.Emails.Requests.SendEmail;
using MailingPoC.Features.Emails.Requests.SendOrderEmail;
using MailingPoC.Features.Emails.Services;
using MailingPoC.Features.Emails.Templates;
using MailingPoC.Features.Emails.Templates.Order;
using Microsoft.AspNetCore.Mvc;

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
        var data = new OrderTemplateModel
        {
            UserName = "Hubert",
            OrderNumber = "12345",
            OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Products = [
                new OrderItem("Smoczy Jeźdźcy: Prawdy Wybrane + PDF", 1, 249.90m)
            ],
            ShippingPrice = 14.90m,
            PaymentLink = "https://www.w3schools.com/tags/tag_colgroup.asp",
            TotalPrice = 264.80m,
            OrderUrl = "https://www.google.pl/index.html"
        };
        
        TemplatesProvider templatesProvider = new();
        
        var bodyHtml = templatesProvider.RenderTemplate(TemplatePath.OrderHtml, data);
        var bodyText = templatesProvider.RenderTemplate(TemplatePath.OrderTxt, data);

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
