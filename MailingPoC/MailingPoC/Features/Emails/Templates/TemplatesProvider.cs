using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider
{
    private readonly IHandlebars _handlebars;
    private HandlebarsTemplate<object, object>? _headerTemplate;
    private HandlebarsTemplate<object, object>? _footerTemplate;

    public TemplatesProvider()
    {
        _handlebars = Handlebars.Create();

        InitTemplates();
    }

    public string RenderTemplate(string path, object data)
    {
        _headerTemplate ??= _handlebars.Compile("{{>Header}}");
        _footerTemplate ??= _handlebars.Compile("{{>Footer}}");

        var template = _handlebars.Compile(File.ReadAllText(path));

        var text = template(data);
        return text;
    }

    private void InitTemplates()
    {
        _handlebars.RegisterTemplate("Header", File.ReadAllText(TemplatePath.HeaderHtml));
        _handlebars.RegisterTemplate("Footer", File.ReadAllText(TemplatePath.FooterHtml));
    }
}