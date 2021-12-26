using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CM.WeeklyTeamReport.WebApp.Email;
using System;
using Newtonsoft.Json;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/invite")]
    public class EmailServiceController : ControllerBase
    {
        [ExcludeFromCodeCoverage]
        [ActivatorUtilitiesConstructor]
        public EmailServiceController()
        {
        }

        [HttpPost]
        public async void SendInvite([FromBody] EmailSet emailData)
        {
            EmailService emailService = new();
            await emailService.SendEmailAsync(emailData);
            //return RedirectToAction("Index");
        }

    }

    public class EmailSet
    {
        public string Email { get; set; }
        public string InviteLink { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
    }
}
