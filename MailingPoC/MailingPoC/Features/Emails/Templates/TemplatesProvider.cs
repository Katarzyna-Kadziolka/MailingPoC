using HandlebarsDotNet;
using MailingPoC.Features.Emails.Templates.Order;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider
{
    private readonly IHandlebars _handlebars;

    public TemplatesProvider()
    {
        _handlebars = Handlebars.Create();

        // InitTemplates();
    }

    public string RenderTemplate(string path)
    {
        // _orderTemplate ??= _handlebars.Compile("{{>OrderEmail_Template}}");

        var template = _handlebars.Compile(File.ReadAllText(path));

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

        var text = template(data);
        return text;
    }

    private void InitTemplates()
    {
        // header, footer
        var path = Path.Combine("Features", "Emails", "Templates", "Order", "OrderEmail.html");
        _handlebars.RegisterTemplate("OrderEmail_Template", File.ReadAllText(path));
    }
}