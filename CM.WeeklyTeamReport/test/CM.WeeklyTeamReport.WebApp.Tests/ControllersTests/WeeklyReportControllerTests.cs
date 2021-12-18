using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.WebApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.WebApp.Tests
{
    public class WeeklyReportControllerTests
    {
        [Fact]
        public void ShouldReturnAllWeeklyReports()
        {
            /*var weeklyReportController = new WeeklyReportController();
            var actionResult = (OkObjectResult)weeklyReportController.ReadAll("1").Result;
            var weeklyReports = (List<WeeklyReport>)actionResult.Value;
            weeklyReports.Should().NotBeNull();
            weeklyReports.Should().HaveCount(3);
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            */
        }

        [Fact]
        public void ShouldReturnWeeklyReportById()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(1))
                .Returns(new WeeklyReport());
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (OkObjectResult)controller.Read("48", "1", "1").Result;
            var weeklyReport = (WeeklyReport)actionResult.Value;
            weeklyReport.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.WeeklyReportRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNoWeeklyReportById()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(48123123))
                .Returns((WeeklyReport)null);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (NotFoundObjectResult)controller.Read("48", "1", "48123123").Result;
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.WeeklyReportRepository.Verify(x => x.Read(48123123), Times.Once);
        }


        [Fact]
        public void ShouldReturnBadRequestReadWeeklyReportByIncorrectCompanyId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(1));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Read("-48", "1", "1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Read(1), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestReadWeeklyReportByIncorrectTeamMemberId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(-1));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Read("48", "-1", "1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Read(-1), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestReadWeeklyReportByIncorrectWeeklyReportId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(-1));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Read("48", "1", "-1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Read(-1), Times.Never);
        }

        [Fact]
        public void ShouldCreateWeeklyReport()
        {
            var weeklyReport = new WeeklyReport();
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Create(weeklyReport))
                .Returns(weeklyReport);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (CreatedResult)controller.Create(weeklyReport, "48").Result;
            weeklyReport = (WeeklyReport)actionResult.Value;
            weeklyReport.Should().NotBeNull();
            actionResult.Should().BeOfType<CreatedResult>();
            actionResult.StatusCode.Should().Be(201);
            fixture.WeeklyReportRepository.Verify(x => x.Create(weeklyReport), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestCreateIncorrectWeeklyReport()
        {
            var weeklyReport = (WeeklyReport)null;
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Create(weeklyReport))
                .Returns(weeklyReport);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Create(weeklyReport, "48").Result;
            weeklyReport.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Create(weeklyReport), Times.Never);
        }

        [Fact]
        public void ShouldUpdateWeeklyReport()
        {
            var weeklyReport = new WeeklyReport();
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Update(weeklyReport))
                .Returns(weeklyReport);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (OkObjectResult)controller.Update(weeklyReport).Result;
            weeklyReport = (WeeklyReport)actionResult.Value;
            weeklyReport.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.WeeklyReportRepository.Verify(x => x.Update(weeklyReport), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestUpdateIncorrectWeeklyReport()
        {
            var weeklyReport = (WeeklyReport)null;
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Update(weeklyReport))
                .Returns(weeklyReport);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Update(weeklyReport).Result;
            weeklyReport.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Update(weeklyReport), Times.Never);
        }

        [Fact]
        public void ShouldDeleteWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(1));
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(1))
                .Returns(new WeeklyReport());
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (OkObjectResult)controller.Delete("48", "1", "1");
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.WeeklyReportRepository.Verify(x => x.Delete(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundDeleteIncorrectWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(45646546));
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(45646546))
                .Returns((WeeklyReport)null);
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (NotFoundObjectResult)controller.Delete("48", "45646546", "1");
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.WeeklyReportRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectCompanyId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(48));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Delete("-48", "1", "1");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectTeamMemberId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(-48));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Delete("48", "-1", "1");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectWeeklyReportId()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(-48));
            var controller = fixture.GetWeeklyReportController();
            var actionResult = (BadRequestObjectResult)controller.Delete("48", "1", "-1");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.WeeklyReportRepository.Verify(x => x.Delete(48), Times.Never);
        }


    }

    public class WeeklyReportControllerFixture
    {
        public Mock<IRepository<WeeklyReport>> WeeklyReportRepository { get; set; }

        public WeeklyReportControllerFixture()
        {
            WeeklyReportRepository = new Mock<IRepository<WeeklyReport>>();
        }

        public WeeklyReportController GetWeeklyReportController()
        {
            return new WeeklyReportController(WeeklyReportRepository.Object);
        }
    }
}