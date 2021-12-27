using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.WebApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CM.WeeklyTeamReport.WebApp.Tests
{
    public class EmailServiceControllerTests
    {
        [Fact]
        public void ShouldBeAbleToSendEmail()
        {
            EmailSet dataSet = new()
            {
                CompanyName = "company",
                Email = "email",
                InviteLink = "inviteLink",
                UserName = "userName"
            };
            var fixture = new EmailServiceControllerFixture();
            fixture.EmailRepository
                .Setup(x => x.SendEmailAsync(dataSet));
            var controller = fixture.GetReportsFromToController();
            controller.SendInvite(dataSet);
        }
    }
    public class EmailServiceControllerFixture
    {
        public Mock<ISendEmail> EmailRepository { get; set; }
        public EmailServiceControllerFixture()
        {
            EmailRepository = new Mock<ISendEmail>();
        }

        public EmailServiceController GetReportsFromToController()
        {
            return new EmailServiceController(EmailRepository.Object);
        }
    }
}
