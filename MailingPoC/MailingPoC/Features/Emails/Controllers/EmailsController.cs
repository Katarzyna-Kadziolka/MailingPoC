using MailingPoC.Features.Emails.Requests.SendOrderEmail;
using MailingPoC.Features.Emails.Services;
using MailingPoC.Features.Emails.Templates;
using MailingPoC.Features.Emails.Templates.Order;
using Microsoft.AspNetCore.Mvc;

namespace MailingPoC.Features.Emails.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController(IEmailService emailService, ITemplatesProvider templatesProvider) : ControllerBase
{
    [HttpPost(nameof(SendOrderEmail))]
    public async Task<SendOrderEmailResult> SendOrderEmail([FromBody]SendOrderEmailRequest request)
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
        var (html, text) = templatesProvider.RenderTemplate(TemplateModels.Order(data));
        
        var email = new Email
        {
            BodyHtml = html,
            BodyText = text,
            Subject = "Order",
            SenderAddress = "noreply@poc.com",
            ToAddresses = ["test@test.com"]
        };
        
        var sendEmailResult = await emailService.SendEmailAsync(email, HttpContext.RequestAborted);
        
        return new SendOrderEmailResult
        {
            IsSuccess = sendEmailResult.IsSent
        };
    }
}
