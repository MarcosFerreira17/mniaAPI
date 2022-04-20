using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;

namespace mniaAPI.Services
{
    public static class EmailWarning
    {
        public static void sendEmail(string email)

        {
            string emailDestinatario = email;

            string titulo = "Login efetuado na Plataforma Starter.";
            string mensagem = "Olá você acabou de acessar a Plataforma Starter, caso não tenha sido você troque sua senha o quanto antes.";
            string senha = "ktJWP9!83iTvC6f";

            MailMessage mailMessage = new MailMessage("mniaapi@gmail.com", emailDestinatario);

            mailMessage.Subject = $"{titulo}";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<p> {mensagem} </p>";
            mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
            mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("mniaapi@gmail.com", senha);

            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);

            Console.WriteLine("Seu email foi enviado com sucesso! :)");
        }
    }
}