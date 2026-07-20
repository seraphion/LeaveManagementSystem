using Microsoft.AspNetCore.Identity.UI.Services;

using System.Net.Mail;

namespace LeaveManagementSystem.Web.Services;

public class EmailSender(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromAddress = configuration["EmailSettings:DefaultEmailAddress"] ?? throw new InvalidOperationException("Default email address is not configured");
        var smtpServer = configuration["EmailSettings:Server"] ?? throw new InvalidOperationException("SMTP server is not configured");
        var smtpPort = int.TryParse(configuration["EmailSettings:Port"], out var port) ? port : throw new InvalidOperationException("SMTP port is not configured or invalid");
        var message = new MailMessage
        {
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(email));

        using var smtpClient = new SmtpClient(smtpServer, smtpPort);
        await smtpClient.SendMailAsync(message);
    }
}
