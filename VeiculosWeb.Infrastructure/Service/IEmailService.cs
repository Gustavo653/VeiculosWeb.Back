namespace VeiculosWeb.Infrastructure.Service
{
    public interface IEmailService
    {
        Task SendEmail(string title, string body, string recipient);
        string BuildResetPasswordText(string email, string code);
        string BuildConfirmEmailText(string email, string code);
    }
}