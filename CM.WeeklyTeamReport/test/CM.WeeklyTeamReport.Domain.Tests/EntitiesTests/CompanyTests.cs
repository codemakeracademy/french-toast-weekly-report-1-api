using System;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCompanyObject()
        {
            Company company = new()
            {
                CompanyId = 1,
                CompanyName = "Company Name",
                JoinDate = "2021-12-12"
            };
            Assert.NotNull(company);
            Assert.Equal("Company Name", company.CompanyName);
            Assert.Equal("2021-12-12", company.JoinDate);
            Assert.Equal(1, company.CompanyId);
        }
    }
}
