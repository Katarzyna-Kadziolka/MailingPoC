namespace MailingPoC.Features.Emails.Templates;

[AttributeUsage(AttributeTargets.Class)]
public class PathsAttribute(string htmlPath, string textPath) : Attribute
{
    public string HtmlPath { get; } = htmlPath;
    public string TextPath { get; } = textPath;
}
