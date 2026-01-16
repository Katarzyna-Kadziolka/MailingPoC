using MailingPoC.Features.Emails.Templates.Order;

namespace MailingPoC.Features.Emails.Templates;

public class TemplateModels
{
    public static TemplateModel<OrderTemplateModel> Order(OrderTemplateModel model) => 
        new("Features/Emails/Templates/Order/OrderEmail.html", "Features/Emails/Templates/Order/OrderEmail.txt", model);
}

public record TemplateModel<T>(string Html, string Text, T Data) where T : ITemplateModel;
