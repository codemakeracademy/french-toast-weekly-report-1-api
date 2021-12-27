using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/invite")]
    public class EmailServiceController : ControllerBase
    {
        private readonly ISendEmail _repository;
        private readonly IConfiguration _configuration;

        [ExcludeFromCodeCoverage]
        [ActivatorUtilitiesConstructor]
        public EmailServiceController(ISendEmail repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public EmailServiceController(ISendEmail repository)
        {
            _repository = repository;
        }
        [ExcludeFromCodeCoverage]
        public EmailServiceController()
        {
        }

        [HttpPost]
        public async void SendInvite([FromBody] EmailSet emailData)
        {
            await _repository.SendEmailAsync(emailData);
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
