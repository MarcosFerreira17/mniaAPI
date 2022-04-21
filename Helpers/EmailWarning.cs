using System.Net;
using System.Net.Mail;
using System.Text;

namespace mniaAPI.Helpers
{
    public static class EmailWarning
    {
        public static void sendEmail(string email)
        {
            string emailDestinatario = email;

            string tittle = "Login efetuado na Plataforma Starter.";
            string mensagem = "Olá você acabou de acessar a Plataforma Starter, caso não tenha sido você troque sua senha o quanto antes.";
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
        }
    }
}