using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider
{
    private readonly IHandlebars _handlebars;
    private HandlebarsTemplate<object, object>? _testTemplate;

    public TemplatesProvider()
    {
        _handlebars = Handlebars.Create();

        InitTemplates();
    }

    public string RenderTestTemplate()
    {
        _testTemplate ??= _handlebars.Compile("{{>TestMail_Template}}");
        
        var text = _testTemplate("", "");
        return text;
    }

    private void InitTemplates()
    {
        _handlebars.RegisterTemplate("TestMail_Template", File.ReadAllText("TestEmail.html"));
    }
}