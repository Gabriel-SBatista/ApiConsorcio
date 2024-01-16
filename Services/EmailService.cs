using System.Net.Mail;
using System.Net;

namespace ApiConsorcio.Services;

public class EmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(string addressee)
    {
        string contentHtml = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index.html"));

        using (SmtpClient clientSmtp = new SmtpClient(_smtpServer))
        {
            clientSmtp.Port = _smtpPort;
            clientSmtp.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            clientSmtp.EnableSsl = true;

            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(_smtpUsername);

                message.To.Add(addressee);

                message.Subject = "Teste de Email Html";
                message.Body = contentHtml;
                message.IsBodyHtml = true;

                await clientSmtp.SendMailAsync(message);
            }
        }
    }
}
