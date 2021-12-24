using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Linq;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/team-members")]
    public class TeamMemberController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ITeamMemberRepository<TeamMember> _repository;

        [ExcludeFromCodeCoverage]
        [ActivatorUtilitiesConstructor]
        public TeamMemberController(ITeamMemberRepository<TeamMember> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public TeamMemberController(ITeamMemberRepository<TeamMember> repository)
        {
            _repository = repository;
        }

        [ExcludeFromCodeCoverage]
        public TeamMemberController()
        {

        }

        [HttpGet]
        public ActionResult<List<TeamMember>> ReadAllById(string companyId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            var result = _repository.ReadAllById(Convert.ToInt32(companyId));
            if (result.Count == 0)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }

        [Route("/api/team-members")]
        [HttpGet]
        public ActionResult<TeamMember> ReadAll()
        {
            var result = _repository.ReadAll();
            if (result.Count == 0)
            {
                return new NotFoundObjectResult($"TeamMembers Not Found");
            }
            return new OkObjectResult(result);
        }

        [Route("/api/team-members/{subject}")]
        [HttpGet]
        public ActionResult<TeamMember> ReadMemberBySub([FromRoute] string subject)
        {
            var result = _repository.ReadMemberBySub(subject);
            if (result == null)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }


        [Route("{teamMemberId}")]
        [HttpGet]
        public ActionResult<TeamMember> Read([FromRoute] string companyId, [FromRoute] string teamMemberId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            var result = _repository.Read(Convert.ToInt32(teamMemberId));
            if (result == null)
            {
                return new NotFoundObjectResult($"TeamMember {teamMemberId} Not Found");
            }
            return new OkObjectResult(result);
        }

        [HttpPost]
        public ActionResult<TeamMember> Create([FromBody] TeamMember teamMember)
        {
            if (teamMember == null)
            {
                return new BadRequestObjectResult("TeamMember should not be null.");
            }
            var result = _repository.Create(teamMember);
            return new CreatedResult($"/api/companies/{result.CompanyId}/team-members/{result.TeamMemberId}", result);
        }

        [HttpPut]
        public ActionResult<TeamMember> Update([FromBody] TeamMember teamMember)
        {
            if (teamMember == null)
            {
                return new BadRequestObjectResult("TeamMember should not be null.");
            }
            var result = _repository.Update(teamMember);
            return new OkObjectResult(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] string companyId, [FromQuery] string teamMemberId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            if (_repository.Read(Convert.ToInt32(teamMemberId)) == null)
            {
                return new NotFoundObjectResult($"TeamMember {teamMemberId} Not Found");
            }
            _repository.Delete(Convert.ToInt32(teamMemberId));
            return new OkObjectResult($"TeamMember {teamMemberId} is deleted.");
        }

        [Route("/api/companies/{companyId}/team-members/reports")]
        [HttpGet]
        public ActionResult<List<ReportHistory>> ReadReportHistory([FromRoute] int companyId, [FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            if (!Regex.IsMatch(companyId.ToString(), @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            var teamMembers = _repository.ReadAllById(companyId);
            List<ReportHistory> result = new();
            Dictionary<string, int[]> tempDBResult;
            string nineWeeksAgoDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - (7 * 9) + 1).ToString("yyyy-MM-dd");
            
            foreach (TeamMember teamMember in teamMembers)
            {
                tempDBResult = _repository.ReadReportHistory(companyId, teamMember.TeamMemberId, dateFrom, dateTo);
                string mondayDate = nineWeeksAgoDate;
                List<int[]> tempResult = new();
                for (int i = 0; i < 10; i++) 
                {
                    if (tempDBResult.Keys.Contains(mondayDate))
                    {
                        tempResult.Add(tempDBResult[mondayDate]);
                    }
                    else
                    {
                        tempResult.Add(new int[] { 0, 0, 0 });
                    }
                    mondayDate = DateTime.Parse(mondayDate).AddDays(7).ToString("yyyy-MM-dd");
                    
                }       
                                               
                ReportHistory temp = new();
                temp.TeamMemberName = teamMember.FirstName + " " + teamMember.LastName;
                temp.TeamMemberReports = tempResult;
                result.Add(temp);
            }
            if (result == null)
            {
                return new NotFoundObjectResult($"Reports Not Found");
            }
            return new OkObjectResult(result);
        }


        [Route("/api/companies/{companyId}/team-members/reports/immediate")]
        [HttpGet]
        public ActionResult<List<ReportHistory>> ReadReportHistory([FromRoute] int companyId, [FromQuery] string dateFrom, [FromQuery] string dateTo, [FromQuery] int to)
        {
            if (!Regex.IsMatch(companyId.ToString(), @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }

            var teamMembers = _repository.ReadAllById(companyId);
            List<ReportHistory> result = new();
            Dictionary<string, int[]> tempDBResult;
            string nineWeeksAgoDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - (7 * 9) + 1).ToString("yyyy-MM-dd");

            foreach (TeamMember teamMember in teamMembers)
            {
                tempDBResult = _repository.ReadReportHistoryTo(companyId, teamMember.TeamMemberId, dateFrom, dateTo, to);
                if (tempDBResult.Count == 0) continue;
                string mondayDate = nineWeeksAgoDate;
                List<int[]> tempResult = new();
                for (int i = 0; i < 10; i++)
                {
                    if (tempDBResult.Keys.Contains(mondayDate))
                    {
                        tempResult.Add(tempDBResult[mondayDate]);
                    }
                    else
                    {
                        tempResult.Add(new int[] { 0, 0, 0 });
                    }
                    mondayDate = DateTime.Parse(mondayDate).AddDays(7).ToString("yyyy-MM-dd");

                }

                ReportHistory temp = new();
                temp.TeamMemberName = teamMember.FirstName + " " + teamMember.LastName;
                temp.TeamMemberReports = tempResult;
                result.Add(temp);
            }
            if (result == null)
            {
                return new NotFoundObjectResult($"Reports Not Found");
            }
            return new OkObjectResult(result);
        }
    }

    public class ReportHistory
    {
        public string TeamMemberName { get; set; }
        public List<int[]> TeamMemberReports { get; set; }
    }
}
