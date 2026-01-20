using System.Reflection;
using HandlebarsDotNet;

namespace MailingPoC.Features.Emails.Templates;

public class TemplatesProvider(IHandlebars handlebars) : ITemplatesProvider
{
    public (string Html, string Text) RenderTemplate(ITemplateModel model)
    {
        var pathsAttribute = model.GetType().GetCustomAttribute<PathsAttribute>();
        if (pathsAttribute == null) throw new InvalidOperationException($"{nameof(PathsAttribute)} not found in {model.GetType().Name}. {nameof(PathsAttribute)} is required for templates");
        
        var templateHtml = handlebars.Compile(File.ReadAllText(pathsAttribute.HtmlPath));
        var html = templateHtml(model);    
        
        var templateText = handlebars.Compile(File.ReadAllText(pathsAttribute.TextPath));
        var text = templateText(model);
        
        return (html,text);
    }
}
