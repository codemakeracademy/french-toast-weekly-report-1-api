using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime JoinDate { get; set; }

        public Company()
        {

        }
    }
}
