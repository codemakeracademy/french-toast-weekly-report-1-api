using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport
    {
        public int WeeklyReportId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public Morales MoraleValueId { get; set; }
        public Morales StressValueId { get; set; }
        public Morales WorkloadValueId { get; set; }
        public string MoraleComment { get; set; }
        public string StressComment { get; set; }
        public string WorkloadComment { get; set; }
        public string WeekHighComment { get; set; }
        public string WeekLowComment { get; set; }
        public string AnythingElseComment { get; set; }
        public int TeamMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public WeeklyReport()
        {

        }
    }
}
