using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MusicStore.MVC.Services
{
  public class DemoEmailSender : IEmailSender
  {
    private readonly EmailSenderOptions emailSenderOptions;

    public DemoEmailSender(
      IOptions<AppSettings> settings)
    {
      this.emailSenderOptions = settings.Value.EmailSenderOptions;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      var client = new SendGridClient(emailSenderOptions.SendGridKey);
      var from = new EmailAddress(emailSenderOptions.SenderEmail, emailSenderOptions.SenderName);
      
      var to = new EmailAddress(email);
      var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
      var response = await client.SendEmailAsync(msg);
    }
  }
}
