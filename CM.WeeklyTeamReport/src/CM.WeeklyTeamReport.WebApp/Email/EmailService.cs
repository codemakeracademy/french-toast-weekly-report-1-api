using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using CM.WeeklyTeamReport.WebApp.Controllers;

namespace CM.WeeklyTeamReport.WebApp.Email
{
    public class EmailService
    {
        public async Task SendEmailAsync(EmailSet emailData)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("noreply@anko", "noreply.testanko@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", emailData.Email));
            emailMessage.Subject = "Invitation to " + emailData.CompanyName;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Hello, <b>" + emailData.UserName + "!</b> Join us by clicking the following link:<br>" + emailData.InviteLink + "!"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("noreply.testanko@gmail.com", "hjltryhsaskjperu");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
