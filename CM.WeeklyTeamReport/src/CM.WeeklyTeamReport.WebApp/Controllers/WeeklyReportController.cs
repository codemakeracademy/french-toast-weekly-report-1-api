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

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/team-members/{teamMemberId}/reports")]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IRepository<WeeklyReport> _repository;

        [ActivatorUtilitiesConstructor]
        public WeeklyReportController(IRepository<WeeklyReport> repository)
        {
            _repository = repository;
        }
        public WeeklyReportController()
        {
        }

        [ExcludeFromCodeCoverage]
        [HttpGet]
        public ActionResult<List<WeeklyReport>> ReadAll(string teamMemberId)
        {
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            WeeklyReportRepository weeklyReportRepository = new WeeklyReportRepository();
            var result = weeklyReportRepository.ReadAllById(Convert.ToInt32(teamMemberId));
            if (result.Count == 0)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult<WeeklyReport> Read([FromRoute] string companyId, [FromRoute] string teamMemberId, [FromRoute] string weeklyReportId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            if (!Regex.IsMatch(weeklyReportId, @"^\d+$"))
            {
                return new BadRequestObjectResult("WeeklyReportId should be positive integer.");
            }
            var result = _repository.Read(Convert.ToInt32(weeklyReportId));
            if (result == null)
            {
                return new NotFoundObjectResult($"WeeklyReport {weeklyReportId} Not Found");
            }
            return new OkObjectResult(result);
        }

        [HttpPost]
        public ActionResult<WeeklyReport> Create([FromQuery] WeeklyReport weeklyReport, [FromRoute] string companyId)
        {
            if (weeklyReport == null)
            {
                return new BadRequestObjectResult("WeeklyReport should not be null.");
            }
            var result = _repository.Create(weeklyReport);
            return new CreatedResult($"/api/companies/{companyId}/team-members/{result.TeamMemberId}/reports/{result.WeeklyReportId}", result);
        }

        [HttpPut]
        public ActionResult<WeeklyReport> Update([FromQuery] WeeklyReport weeklyReport)
        {
            if (weeklyReport == null)
            {
                return new BadRequestObjectResult("WeeklyReport should not be null.");
            }
            var result = _repository.Update(weeklyReport);
            return new OkObjectResult(result);
        }


        [HttpDelete]
        public ActionResult Delete([FromRoute] string companyId, [FromRoute] string teamMemberId, [FromQuery] string weeklyReportId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            if (!Regex.IsMatch(weeklyReportId, @"^\d+$"))
            {
                return new BadRequestObjectResult("WeeklyReportId should be positive integer.");
            }
            if (_repository.Read(Convert.ToInt32(weeklyReportId)) == null)
            {
                return new NotFoundObjectResult($"WeeklyReport {weeklyReportId} Not Found");
            }
            _repository.Delete(Convert.ToInt32(weeklyReportId));
            return new OkObjectResult($"WeeklyReport {weeklyReportId} is deleted.");
        }
    }
}
