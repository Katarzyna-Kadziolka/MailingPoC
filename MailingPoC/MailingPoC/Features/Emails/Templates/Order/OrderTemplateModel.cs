namespace MailingPoC.Features.Emails.Templates.Order;

[Paths(htmlPath:"Features/Emails/Templates/Order/OrderEmail.html", textPath:"Features/Emails/Templates/Order/OrderEmail.txt")]
public record OrderTemplateModel(
    string UserName,
    string OrderNumber,
    string OrderDate,
    List<OrderItem> Products,
    decimal ShippingPrice,
    string PaymentLink,
    decimal TotalPrice,
    string OrderUrl) : ITemplateModel;

public record OrderItem(string Name, int Quantity, decimal Price);
