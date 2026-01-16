using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider(IHandlebars handlebars) : ITemplatesProvider
{
    public (string Html, string Text) RenderTemplate<T>(TemplateModel<T> model) where T : ITemplateModel
    {
        var templateHtml = handlebars.Compile(File.ReadAllText(model.Html));
        var html = templateHtml(model.Data);    
        
        var templateText = handlebars.Compile(File.ReadAllText(model.Text));
        var text = templateText(model.Data);
        
        return (html,text);
    }
}