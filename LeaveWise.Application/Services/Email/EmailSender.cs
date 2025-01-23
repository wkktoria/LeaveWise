using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace LeaveWise.Application.Services.Email;

public class EmailSender(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpServer = configuration["EmailSettings:Server"];
        var smtpPort = Convert.ToInt32(configuration["EmailSettings:Port"]);
        var fromAddress = configuration["EmailSettings:DefaultEmailAddress"];

        var message = new MailMessage
        {
            From = new MailAddress(fromAddress!),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(email));

        using var client = new SmtpClient(smtpServer, smtpPort);
        await client.SendMailAsync(message);
    }
}