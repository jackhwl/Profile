using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Models.Mail;
//using SendGrid;
//using SendGrid.Helpers.Mail;

namespace Wenlin.Infrastructure.Mail;
public class EmailService : IEmailService
{
    public EmailSettings _emailSettings;
    public ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmail(Email email)
    {
        //var client = new SendGridClient(_emailSettings.ApiKey);

        //var subject = email.Subject;
        //var to = new EmailAddress(email.To);
        //var emailBody = email.Body;

        //var from = new EmailAddress
        //{
        //    Email = _emailSettings.FromAddress,
        //    Name = _emailSettings.FromName
        //};

        //var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        //var response = await client.SendEmailAsync(sendGridMessage);

        //_logger.LogInformation("Email sent");

        //if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
        //    return true;

        //_logger.LogError("Email sending failed");

        return await Task.FromResult(true);
    }
}

