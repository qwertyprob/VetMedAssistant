using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Medcard.Bl.Abstraction;

namespace Medcard.Bl.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync()
        {
            var mail = "englisharlekino@gmail.com";
            var password = "022430861arsenii"; // Вынеси это в конфиг

            var recipientEmail = "eqspertars@gmail.com";

            var client = new SmtpClient("smtp.gmail.com", 587) // Укажи хост и порт
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            var message = new MailMessage(from: mail, to: recipientEmail, subject: "Subject", body: "Message body");

            await client.SendMailAsync(message); // Ждем асинхронной отправки
        }
    }
}
