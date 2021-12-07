using System;
using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName 
        { 
            get; 
            set; 
        }
        public List<TeamMember> TeamMembers 
        { 
            get; 
            set; 
        } = new List<TeamMember>();
        public string JoinDate 
        { 
            get; 
            set; 
        }
        public Company()
        {
        }

        public void UpdateCompanyName(string companyName)
        {
            if ((companyName != null) && (companyName.Length > 0))
            {
                CompanyName = companyName;
            }
        }
    }
}
