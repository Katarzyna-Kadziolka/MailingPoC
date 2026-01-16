namespace MailingPoC.Features.Emails.Templates;

public static class TemplatePath
{
    public static readonly string FooterHtml = Path.Combine("Features", "Emails", "Templates", "Footer", "Footer.html");
    public static readonly string HeaderHtml = Path.Combine("Features", "Emails", "Templates", "Header", "Header.html");
    public static readonly string OrderHtml = Path.Combine("Features", "Emails", "Templates", "Order", "OrderEmail.html");
    public static readonly string OrderTxt = Path.Combine("Features", "Emails", "Templates", "Order", "OrderEmail.txt");
}