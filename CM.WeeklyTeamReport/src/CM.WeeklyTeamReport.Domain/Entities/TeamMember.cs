namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public int CompanyId { get; set; }

        public TeamMember()
        {

        }
    }
}
