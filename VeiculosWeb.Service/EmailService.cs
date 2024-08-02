using System.Net;
using System.Net.Mail;
using VeiculosWeb.Infrastructure.Service;

namespace VeiculosWeb.Service
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpUser = Environment.GetEnvironmentVariable("SmtpUser") ?? throw new ArgumentNullException("SmtpUser variável não encontrada.");
            _smtpPassword = Environment.GetEnvironmentVariable("SmtpPassword") ?? throw new ArgumentNullException("SmtpPassword variável não encontrada.");

            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
                EnableSsl = true,
            };
        }

        public string BuildConfirmEmailText(string email, string code)
        {
            return $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}

                    .container {{
                        width: 80%;
                        max-width: 600px;
                        margin: 50px auto;
                        background-color: #fff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        text-align: center;
                    }}

                    h2 {{
                        margin-top: 0;
                    }}

                    p {{
                        font-size: 18px;
                        line-height: 1.6;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h2>Confirmar Email</h2>
                    <p>Recebemos sua solicitação para confirmar o email: {email}</p>
                    <p>Informe o seguinte código junto com sua nova senha: {code}</p>
                    <p>Lembrando que você só conseguirá fazer login após confirmar o email</p>
                </div>
            </body>
            </html>";
        }

        public string BuildResetPasswordText(string email, string code)
        {
            return $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}

                    .container {{
                        width: 80%;
                        max-width: 600px;
                        margin: 50px auto;
                        background-color: #fff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        text-align: center;
                    }}

                    h2 {{
                        margin-top: 0;
                    }}

                    p {{
                        font-size: 18px;
                        line-height: 1.6;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h2>Redefinir Senha</h2>
                    <p>Recebemos sua solicitação para redefinir a senha do email: {email}</p>
                    <p>Informe o seguinte código junto com sua nova senha: {code}</p>
                </div>
            </body>
            </html>";
        }


        public async Task SendEmail(string title, string body, string recipient)
        {
            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = title,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recipient);
            mailMessage.To.Add("gustavohs2004@gmail.com");
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
