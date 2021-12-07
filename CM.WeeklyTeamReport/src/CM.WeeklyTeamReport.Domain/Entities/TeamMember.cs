using System;
using System.Collections.Generic;
using System.Text;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string InviteLink { get; set; }
        public string Mail { get; set; }
        public List<WeeklyReport> ReportsList { get; set; }
        public List<TeamMember> ReportsTo { get; set; }
        public List<TeamMember> ReportsFrom { get; set; }
        public int CompanyId { get; set; }

        public TeamMember()
        {
        }

        public void UpdateMemberData(string firstName, string lastName, string title)
        {
            if ((firstName != null) && (firstName.Length > 0))
            {
                FirstName = firstName;
            }
            if ((lastName != null) && (lastName.Length > 0))
            {
                LastName = lastName;
            }
            if ((title != null) && (title.Length > 0))
            {
                Title = title;
            }
        }
    }
}
