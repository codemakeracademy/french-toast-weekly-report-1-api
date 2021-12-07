using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamMemberTests
    {
        List<WeeklyReport> reportsList = new List<WeeklyReport>();
        List<TeamMember> reportsTo = new List<TeamMember>();
        List<TeamMember> reportsFrom = new List<TeamMember>();
        [Fact]
        public void ShouldBeAbleToCreateTeamMemberObject()
        {
            TeamMember teamMember = new TeamMember()
            {
                TeamMemberId = 1,
                FirstName = "Name",
                LastName = "Surname",
                Title = "CEO",
                InviteLink = "someUrl",
                Mail = "awdaw@ad.wad",
                ReportsList = reportsList,
                ReportsTo = reportsTo,
                ReportsFrom = reportsFrom,
                CompanyId = 2
            };
            Assert.NotNull(teamMember);
            Assert.Equal("Name", teamMember.FirstName);
            Assert.Equal("Surname", teamMember.LastName);
            Assert.Equal("CEO", teamMember.Title);
            Assert.Equal("someUrl", teamMember.InviteLink);
            Assert.Equal("awdaw@ad.wad", teamMember.Mail);
            Assert.Equal(reportsList, teamMember.ReportsList);
            Assert.Equal(reportsTo, teamMember.ReportsTo);
            Assert.Equal(reportsFrom, teamMember.ReportsFrom);
            Assert.Equal(1, teamMember.TeamMemberId);
            Assert.Equal(2, teamMember.CompanyId);
        }

        [Theory]
        [InlineData(null, "New Surname", "New title")]
        [InlineData("", "New Surname", "New title")]
        public void ShouldUpdateFirstNameCorrectly(string newName, string newSurname, string newTitle)
        {
            TeamMember teamMember = new TeamMember()
            {
                TeamMemberId = 1,
                FirstName = "Name",
                LastName = "Surname",
                Title = "CEO",
                InviteLink = "someUrl",
                Mail = "awdaw@ad.wad",
                ReportsList = reportsList,
                ReportsTo = reportsTo,
                ReportsFrom = reportsFrom,
                CompanyId = 2
            };
            string oldName = teamMember.FirstName;
            teamMember.UpdateMemberData(newName, newSurname, newTitle);
            Assert.Equal(oldName, teamMember.FirstName);
        }

        [Theory]
        [InlineData("New Name", null, "New title")]
        [InlineData("New Name", "", "New title")]
        public void ShouldUpdateLastNameCorrectly(string newName, string newSurname, string newTitle)
        {
            TeamMember teamMember = new TeamMember()
            {
                TeamMemberId = 1,
                FirstName = "Name",
                LastName = "Surname",
                Title = "CEO",
                InviteLink = "someUrl",
                Mail = "awdaw@ad.wad",
                ReportsList = reportsList,
                ReportsTo = reportsTo,
                ReportsFrom = reportsFrom,
                CompanyId = 2
            };
            string oldSurname = teamMember.LastName;
            teamMember.UpdateMemberData(newName, newSurname, newTitle);
            Assert.Equal(oldSurname, teamMember.LastName);
        }

        [Theory]
        [InlineData("New Name", "New Surname", null)]
        [InlineData("New Name", "New Surname", "")]
        public void ShouldUpdateTitleCorrectly(string newName, string newSurname, string newTitle)
        {
            TeamMember teamMember = new TeamMember()
            {
                TeamMemberId = 1,
                FirstName = "Name",
                LastName = "Surname",
                Title = "CEO",
                InviteLink = "someUrl",
                Mail = "awdaw@ad.wad",
                ReportsList = reportsList,
                ReportsTo = reportsTo,
                ReportsFrom = reportsFrom,
                CompanyId = 2
            };
            string oldTitle = teamMember.Title;
            teamMember.UpdateMemberData(newName, newSurname, newTitle);
            Assert.Equal(oldTitle, teamMember.Title);
        }
    }
}
