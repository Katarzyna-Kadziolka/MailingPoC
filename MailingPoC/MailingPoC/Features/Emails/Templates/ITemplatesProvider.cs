namespace MailingPoC.Features.Emails.Templates;

public interface ITemplatesProvider
{
    (string Html, string Text) RenderTemplate<T>(TemplateModel<T> model) where T : ITemplateModel;
}
