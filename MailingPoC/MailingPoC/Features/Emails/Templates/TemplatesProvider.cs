using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider(IHandlebars handlebars) : ITemplatesProvider
{
    public (string Html, string Text) RenderTemplate(ITemplateModel model)
    {
        var pathsAttribute = (PathsAttribute) Attribute.GetCustomAttribute(typeof(ITemplateModel), typeof(PathsAttribute))!;
        
        var templateHtml = handlebars.Compile(File.ReadAllText(pathsAttribute.HtmlPath));
        var html = templateHtml(model);    
        
        var templateText = handlebars.Compile(File.ReadAllText(pathsAttribute.TextPath));
        var text = templateText(model);
        
        return (html,text);
    }
}