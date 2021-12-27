using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.WebApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;    

namespace CM.WeeklyTeamReport.WebApp.Tests.ControllersTests
{
    public class ReportsFromToControllerTests
    {
        [Fact]
        public void ShouldBeAbleReadReportsTo()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportTo(5))
                .Returns(new List<string[]>() { new string[] { } });
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.ReadReportTo("5").Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportTo(5), Times.Once);
        }
        [Fact]
        public void ShouldReturnBadRequestWhenReadReportsTo()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportTo(5))
                .Returns(new List<string[]>() { new string[] { } });
            var controller = fixture.GetReportsFromToController();
            var actionResult = (BadRequestObjectResult)controller.ReadReportTo("-5").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportTo(5), Times.Never);
        }
        [Fact]
        public void ShouldReturnNotFoundWhenReadReportsTo()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportTo(5))
                .Returns(new List<string[]>());
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.ReadReportTo("5").Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportTo(5), Times.Once);
        }
        [Fact]
        public void ShouldBeAbleReadReportFrom()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportFrom(5))
                .Returns(new List<string[]>() { new string[] { } });
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.ReadReportFrom("5").Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportFrom(5), Times.Once);
        }
        [Fact]
        public void ShouldReturnBadRequestWhenReadReportFrom()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportFrom(5))
                .Returns(new List<string[]>() { new string[] { } });
            var controller = fixture.GetReportsFromToController();
            var actionResult = (BadRequestObjectResult)controller.ReadReportFrom("-5").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportFrom(5), Times.Never);
        }
        [Fact]
        public void ShouldReturnNotFoundWhenReadReadReportFrom()
        {
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.ReadReportFrom(5))
                .Returns(new List<string[]>());
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.ReadReportFrom("5").Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.ReadReportFrom(5), Times.Once);
        }
        [Fact]
        public void ShouldBeAbleToCreateReportFromTo()
        {
            var reportFromTo = new ReportsFromTo();
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.Create(reportFromTo))
                .Returns(reportFromTo);
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.Create(reportFromTo).Result;
            reportFromTo.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.Create(reportFromTo), Times.Once);
        }
        [Fact]
        public void ShouldReturnBadRequestWhenCreateReportFromTo()
        {
            ReportsFromTo reportFromTo = null;
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.Create(reportFromTo))
                .Returns(reportFromTo);
            var controller = fixture.GetReportsFromToController();
            var actionResult = (BadRequestObjectResult)controller.Create(reportFromTo).Result;
            reportFromTo.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.ReportsFromToRepository.Verify(x => x.Create(reportFromTo), Times.Never);
        }
        [Fact]
        public void ShouldBeAbleToDeleteReportFromTo()
        {
            ReportsFromTo reportFromTo = new();
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.DeleteFromTo(1, 2));
            var controller = fixture.GetReportsFromToController();
            var actionResult = (OkObjectResult)controller.Delete("1","2");
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.ReportsFromToRepository.Verify(x => x.DeleteFromTo(1,2), Times.Once);
        }
        [Fact]
        public void ShouldReturnBadRequestWhenDeleteReportFromToByIncorrectReportToId()
        {
            ReportsFromTo reportFromTo = new();
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.DeleteFromTo(1, 2));
            var controller = fixture.GetReportsFromToController();
            var actionResult = (BadRequestObjectResult)controller.Delete("-1", "2");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.ReportsFromToRepository.Verify(x => x.DeleteFromTo(1, 2), Times.Never);
        }
        [Fact]
        public void ShouldReturnBadRequestWhenDeleteReportFromToByIncorrectReportFromId()
        {
            ReportsFromTo reportFromTo = new();
            var fixture = new ReportsFromToControllerFixture();
            fixture.ReportsFromToRepository
                .Setup(x => x.DeleteFromTo(1, 2));
            var controller = fixture.GetReportsFromToController();
            var actionResult = (BadRequestObjectResult)controller.Delete("1", "-2");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.ReportsFromToRepository.Verify(x => x.DeleteFromTo(1, 2), Times.Never);
        }
    }
    public class ReportsFromToControllerFixture
    {
        public Mock<IReportsFromTo<ReportsFromTo>> ReportsFromToRepository { get; set; }
        public ReportsFromToControllerFixture()
        {
            ReportsFromToRepository = new Mock<IReportsFromTo<ReportsFromTo>>();
        }

        public ReportsFromToController GetReportsFromToController()
        {
            return new ReportsFromToController(ReportsFromToRepository.Object);
        }
    }
}
