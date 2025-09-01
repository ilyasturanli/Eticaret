using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;


namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendEmailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadi.com",587);
            smtpClient.Credentials = new NetworkCredential("info@siteadi.com","mailsifresi");
            smtpClient.EnableSsl = false; // SSL kullanımı isteğe bağlı
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("info@siteadi.com");
            mailMessage.To.Add(new MailAddress("mailnereyegönderilicekse@gmail.com"));
            mailMessage.Subject = "Siteden Mesaj Geldi...";
            mailMessage.Body = $"İsim: {contact.Name} - Soyisim: {contact.Surname} - Email: {contact.Email} - Phone:{contact.Phone} - Mesaj: {contact.Message}";
            mailMessage.IsBodyHtml = true; // HTML içeriği kullanmak için true olarak ayarlanır
            await smtpClient.SendMailAsync(mailMessage);
            smtpClient.Dispose();
        }
    }
}
