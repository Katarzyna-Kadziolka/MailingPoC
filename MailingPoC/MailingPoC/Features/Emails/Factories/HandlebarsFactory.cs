using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Factories;

public static class HandlebarsFactory
{
    public static IHandlebars Create()
    {
        var handlebars = Handlebars.Create();
        handlebars.RegisterTemplate("Footer", File.ReadAllText(Path.Combine("Features", "Emails", "Templates", "Footer", "Footer.html")));
        handlebars.RegisterTemplate("Header", File.ReadAllText(Path.Combine("Features", "Emails", "Templates", "Header", "Header.html")));

        return handlebars;
    }
}
