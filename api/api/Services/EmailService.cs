using System.Net;
using System.Net.Mail;

namespace api.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(string email, string subject, string message)
    {
        var smtpClient = new SmtpClient(_configuration["SmtpSettings:SmtpServer"])
        {
            Port = 587,
            Credentials = new NetworkCredential(_configuration["SmtpSettings:Email"], _configuration["SmtpSettings:Password"]),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["SmtpSettings:Email"]!),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
