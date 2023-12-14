using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get;}

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(EmailMessage email)
        {
            SendGridClient client = new SendGridClient(_emailSettings.ApiKey);
            EmailAddress to = new EmailAddress(email.To);
            EmailAddress from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName,
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            //return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode ==
            //System.Net.HttpStatusCode.Accepted;

            //OR

            return response.IsSuccessStatusCode;
        }
    }
}
