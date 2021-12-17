using CM.WeeklyTeamReport.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

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
        public ActionResult<List<TeamMember>> ReadAll(string companyId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            TeamMemberRepository teamMemberRepository = new TeamMemberRepository(_configuration);
            var result = teamMemberRepository.ReadAllById(Convert.ToInt32(companyId));
            if (result.Count == 0)
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
    }
}
