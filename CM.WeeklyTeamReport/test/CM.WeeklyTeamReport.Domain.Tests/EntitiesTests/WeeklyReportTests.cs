using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeeklyReportTests
    {
        [Fact]
        public void ShouldBeAbleToCreateWeeklyReportObject()
        {
            WeeklyReport weeklyReport = new WeeklyReport() 
            {
                StartDate = "2021-12-12",
                EndDate = "2021-12-12",
                MoraleValue = Morales.Okay,
                StressValue = Morales.Low,
                WorkloadValue = Morales.Great,
                MoraleComment = "wadad",
                StressComment = "wadad",
                WorkloadComment = "wadad",
                WeekHighComment = "wadad",
                WeekLowComment = "wadad",
                AnythingElseComment = "wadad",
                WeeklyReportId = 1,
                TeamMemberId = 2
            };
            Assert.NotNull(weeklyReport);
            Assert.Equal("2021-12-12", weeklyReport.StartDate);
            Assert.Equal("2021-12-12", weeklyReport.EndDate);
            Assert.Equal(Morales.Okay, weeklyReport.MoraleValue);
            Assert.Equal(Morales.Low, weeklyReport.StressValue);
            Assert.Equal(Morales.Great, weeklyReport.WorkloadValue);
            Assert.Equal("wadad", weeklyReport.MoraleComment);
            Assert.Equal("wadad", weeklyReport.StressComment);
            Assert.Equal("wadad", weeklyReport.WorkloadComment);
            Assert.Equal("wadad", weeklyReport.WeekHighComment);
            Assert.Equal("wadad", weeklyReport.WeekLowComment);
            Assert.Equal("wadad", weeklyReport.AnythingElseComment);
            Assert.Equal(1, weeklyReport.WeeklyReportId);
            Assert.Equal(2, weeklyReport.TeamMemberId);
        }
    }
}
