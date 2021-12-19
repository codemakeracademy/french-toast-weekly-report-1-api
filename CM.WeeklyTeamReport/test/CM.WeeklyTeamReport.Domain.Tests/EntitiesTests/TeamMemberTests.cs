using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamMemberTests
    {
        [Fact]
        public void ShouldBeAbleToCreateTeamMemberObject()
        {
            TeamMember teamMember = new()
            {
                TeamMemberId = 1,
                FirstName = "Name",
                LastName = "Surname",
                Title = "CEO",
                Mail = "awdaw@ad.wad",
                Subject = "dsfgsfdgdfsg",
                CompanyId = 2
            };
            Assert.NotNull(teamMember);
            Assert.Equal("Name", teamMember.FirstName);
            Assert.Equal("Surname", teamMember.LastName);
            Assert.Equal("CEO", teamMember.Title);
            Assert.Equal("awdaw@ad.wad", teamMember.Mail);
            Assert.Equal("dsfgsfdgdfsg", teamMember.Subject);
            Assert.Equal(1, teamMember.TeamMemberId);
            Assert.Equal(2, teamMember.CompanyId);
        }
    }
}
