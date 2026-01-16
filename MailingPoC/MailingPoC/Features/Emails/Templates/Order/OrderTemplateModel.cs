namespace MailingPoC.Features.Emails.Templates.Order;

// Ścieżki tutaj jako atrybuty i wtedy rozwal całą architekturę i ją uprościsz
public record OrderTemplateModel : ITemplateModel
{
    public string UserName { get; set; }
    public string OrderNumber { get; set; }
    public string OrderDate { get; set; }
    public List<OrderItem> Products { get; set; }
    public decimal ShippingPrice { get; set; }
    public string PaymentLink { get; set; }
    
    public decimal TotalPrice { get; set;}
    public string OrderUrl { get; set; }
}

public record OrderItem(string Name, int Quantity, decimal Price);