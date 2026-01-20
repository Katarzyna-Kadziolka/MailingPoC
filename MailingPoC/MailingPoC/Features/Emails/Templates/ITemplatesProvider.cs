namespace MailingPoC.Features.Emails.Templates;

public interface ITemplatesProvider
{
    (string Html, string Text) RenderTemplate(ITemplateModel model);
}
