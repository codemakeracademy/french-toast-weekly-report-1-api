using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/team-members")]
    public class TeamMemberController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IRepository<TeamMember> _repository;

        [ActivatorUtilitiesConstructor]
        public TeamMemberController(IRepository<TeamMember> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public TeamMemberController(IRepository<TeamMember> repository)
        {
            _repository = repository;
        }

        public TeamMemberController()
        {

        }

        [ExcludeFromCodeCoverage]
        [HttpGet]
        public ActionResult<List<TeamMember>> ReadAllById(string companyId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            TeamMemberRepository teamMemberRepository = new(_configuration);
            var result = teamMemberRepository.ReadAllById(Convert.ToInt32(companyId));
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
            TeamMemberRepository teamMemberRepository = new(_configuration);
            var result = teamMemberRepository.ReadAll();
            if (result == null)
            {
                return new NotFoundObjectResult($"TeamMembers Not Found");
            }
            return new OkObjectResult(result);
        }

        [Route("/api/team-members/{subject}")]
        [HttpGet]
        public ActionResult<TeamMember> ReadMemberBySub([FromRoute] string subject)
        {
            TeamMemberRepository teamMemberRepository = new(_configuration);
            var result = teamMemberRepository.ReadMemberBySub(subject);
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
            var teamMemberRepository = new TeamMemberRepository(_configuration);
            List<TeamMember> teamMembers = teamMemberRepository.ReadAllById(companyId);
            List<ReportHistory> result = new();
            foreach (TeamMember teamMember in teamMembers)
            {
                ReportHistory temp = new();
                temp.TeamMemberName = teamMember.FirstName + " " + teamMember.LastName;
                temp.TeamMemberReports = teamMemberRepository.ReadReportHistory(companyId, teamMember.TeamMemberId, dateFrom, dateTo);
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
