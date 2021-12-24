using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.WebApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.WebApp.Tests
{
    public class TeamMemberControllerTests
    {
        [Fact]
        public void ShouldReturnAllTeamMembersFromCompany()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadAllById(1))
                .Returns(new List<TeamMember>() { new TeamMember()});
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.ReadAllById("1").Result;
            var teamMember = (List<TeamMember>)actionResult.Value;
            teamMember.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.ReadAllById(1), Times.Once);
        }
        [Fact]
        public void ShouldReturnBadRequestObjectResultbyReadingAllMembersFromCompany()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadAllById(1))
                .Returns(new List<TeamMember>() { new TeamMember() });
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.ReadAllById("-1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.ReadAllById(1), Times.Never);
        }
        [Fact]
        public void ShouldReturnAllMemebrs()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<TeamMember>() { new TeamMember() });
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.ReadAll().Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.ReadAll(), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundObjectResultByReadingAllMemebrs()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<TeamMember>());
            var controller = fixture.GetTeamMemberController();
            var actionResult = (NotFoundObjectResult)controller.ReadAll().Result;
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.TeamMemberRepository.Verify(x => x.ReadAll(), Times.Once);
            
        }
        [Fact]
        public void ShouldReturnMemebrBySubject()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadMemberBySub("his sub"))
                .Returns(new TeamMember());
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.ReadMemberBySub("his sub").Result;
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.ReadMemberBySub("his sub"), Times.Once);
        }
        [Fact]
        public void ShouldReturnNoContentResultBySubject()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadMemberBySub("his sub"))
                .Returns((TeamMember)null);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (NoContentResult)controller.ReadMemberBySub("his sub").Result;
            actionResult.Should().BeOfType<NoContentResult>();
            actionResult.StatusCode.Should().Be(204);
            fixture.TeamMemberRepository.Verify(x => x.ReadMemberBySub("his sub"), Times.Once);
        }
        [Fact]
        public void ShouldReturnNoContentResultByReadingAllMembersFromCompany()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.ReadAllById(1))
                .Returns(new List<TeamMember>());
            var controller = fixture.GetTeamMemberController();
            var actionResult = (NoContentResult)controller.ReadAllById("1").Result;
            actionResult.Should().BeOfType<NoContentResult>();
            actionResult.StatusCode.Should().Be(204);
            fixture.TeamMemberRepository.Verify(x => x.ReadAllById(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnTeamMemberById()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Read(1))
                .Returns(new TeamMember());
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.Read("48", "1").Result;
            var teamMember = (TeamMember)actionResult.Value;
            teamMember.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNoTeamMemberById()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Read(48123123))
                .Returns((TeamMember)null);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (NotFoundObjectResult)controller.Read("48", "48123123").Result;
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.TeamMemberRepository.Verify(x => x.Read(48123123), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestReadTeamMemberByIncorrectTeamMemberId()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Read(-1));
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Read("48", "-1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Read(-1), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestReadTeamMemberByIncorrectCompanyId()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Read(1));
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Read("-48", "1").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Read(1), Times.Never);
        }

        [Fact]
        public void ShouldCreateTeamMember()
        {
            var teamMember = new TeamMember();
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Create(teamMember))
                .Returns(teamMember);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (CreatedResult)controller.Create(teamMember).Result;
            teamMember = (TeamMember)actionResult.Value;
            teamMember.Should().NotBeNull();
            actionResult.Should().BeOfType<CreatedResult>();
            actionResult.StatusCode.Should().Be(201);
            fixture.TeamMemberRepository.Verify(x => x.Create(teamMember), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestCreateIncorrectTeamMember()
        {
            var teamMember = (TeamMember)null;
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Create(teamMember))
                .Returns(teamMember);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Create(teamMember).Result;
            teamMember.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Create(teamMember), Times.Never);
        }

        [Fact]
        public void ShouldUpdateTeamMember()
        {
            var teamMember = new TeamMember();
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Update(teamMember))
                .Returns(teamMember);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.Update(teamMember).Result;
            teamMember = (TeamMember)actionResult.Value;
            teamMember.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.Update(teamMember), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestUpdateIncorrectTeamMember()
        {
            var teamMember = (TeamMember)null;
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Update(teamMember))
                .Returns(teamMember);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Update(teamMember).Result;
            teamMember.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Update(teamMember), Times.Never);
        }

        [Fact]
        public void ShouldDeleteTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(1));
            fixture.TeamMemberRepository
                .Setup(x => x.Read(1))
                .Returns(new TeamMember());
            var controller = fixture.GetTeamMemberController();
            var actionResult = (OkObjectResult)controller.Delete("48", "1");
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.TeamMemberRepository.Verify(x => x.Delete(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundDeleteIncorrectTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(45646546));
            fixture.TeamMemberRepository
                .Setup(x => x.Read(45646546))
                .Returns((TeamMember)null);
            var controller = fixture.GetTeamMemberController();
            var actionResult = (NotFoundObjectResult)controller.Delete("48", "45646546");
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.TeamMemberRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectTeamMemberId()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(-48));
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Delete("48", "-1");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectCompanyId()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(48));
            var controller = fixture.GetTeamMemberController();
            var actionResult = (BadRequestObjectResult)controller.Delete("-48", "1");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.TeamMemberRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldBeAbleToCreateReportHistory()
        {
            List<int[]> reportData = new()
            {
                new int[] { 1, 2, 3 },
                new int[] { 5, 4, 2 }
            };
            ReportHistory reportHistory = new()
            {
                TeamMemberName = "Ilya Kra",
                TeamMemberReports = reportData
            };
            reportHistory.Should().NotBeNull();
        }
    }

    public class TeamMemberControllerFixture
    {
        public Mock<ITeamMemberRepository<TeamMember>> TeamMemberRepository { get; set; }

        public TeamMemberControllerFixture()
        {
            TeamMemberRepository = new Mock<ITeamMemberRepository<TeamMember>>();
        }

        public TeamMemberController GetTeamMemberController()
        {
            return new TeamMemberController(TeamMemberRepository.Object);
        }
    }
}