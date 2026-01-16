using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider
{
    private readonly IHandlebars _handlebars;

    public TemplatesProvider()
    {
        _handlebars = Handlebars.Create();

        // InitTemplates();
    }

    public string RenderTemplate(string path, object data)
    {
        // _orderTemplate ??= _handlebars.Compile("{{>OrderEmail_Template}}");

        var template = _handlebars.Compile(File.ReadAllText(path));

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