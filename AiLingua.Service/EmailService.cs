using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using AiLingua.Core.Services.Contract;
namespace AiLingua.Service
{
    public class EmailService:IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ailinguahti@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            // ⚠️ This should only be used in development/testing environments!
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("ailinguahti@gmail.com", "hugz embx dsiy fphs");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
