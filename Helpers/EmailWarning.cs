using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace mniaAPI.Helpers
{
    public static class EmailWarning
    {
        public static string sendEmail(string email, string mensagem)
        {

            try
            {
                string emailDestinatario = email;

                string tittle = "Login efetuado na Plataforma Starter.";
                string senha = "ktJWP9!83iTvC6f";

                MailMessage mailMessage = new MailMessage("mniaapi@gmail.com", emailDestinatario);

                mailMessage.Subject = $"{tittle}";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = $"<p> {mensagem} </p>";
                mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("mniaapi@gmail.com", senha);

                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
                return "E-mail enviado com sucesso";
            }
            catch (Exception)
            {
                return "Não foi possivel enviar um e-mail de verificação";
            }

        }
    }
}