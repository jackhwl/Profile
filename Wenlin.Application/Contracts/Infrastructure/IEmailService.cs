using Wenlin.Application.Models.Mail;

namespace Wenlin.Application.Contracts.Infrastructure;
public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
