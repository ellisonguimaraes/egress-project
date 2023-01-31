using AuthApp.Domain.Email;
using MimeKit;

namespace AuthApp.Application.Utils.EmailSender;

public interface IEmailSender
{
    MimeMessage CreateEmailMessage(Message message);

    Task SendAsync(MimeMessage mailMessage);
}
