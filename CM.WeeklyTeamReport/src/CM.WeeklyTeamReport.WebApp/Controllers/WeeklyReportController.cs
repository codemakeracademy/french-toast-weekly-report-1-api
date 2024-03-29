﻿using CM.WeeklyTeamReport.Domain;
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
    [Route("api/companies/{companyId}/team-members/{teamMemberId}/reports")]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWeeklyReportRepository<WeeklyReport> _repository;

        [ExcludeFromCodeCoverage]
        [ActivatorUtilitiesConstructor]
        public WeeklyReportController(IWeeklyReportRepository<WeeklyReport> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public WeeklyReportController(IWeeklyReportRepository<WeeklyReport> repository)
        {
            _repository = repository;
        }

        [ExcludeFromCodeCoverage]
        public WeeklyReportController() { }

        
        [HttpGet]
        public ActionResult<List<WeeklyReport>> ReadAllByMemberId([FromRoute] string teamMemberId)
        {
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            var result = _repository.ReadAllById(Convert.ToInt32(teamMemberId));
            if (result.Count == 0)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }

        [Route("{weeklyReportId}")]
        [HttpGet]
        public ActionResult<WeeklyReport> Read([FromRoute] string teamMemberId, [FromRoute] string weeklyReportId)
        {
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
        public ActionResult<WeeklyReport> Create([FromRoute] string companyId, [FromBody] WeeklyReport weeklyReport)
        {
            if (weeklyReport == null)
            {
                return new BadRequestObjectResult("WeeklyReport should not be null.");
            }
            var result = _repository.Create(weeklyReport);
            return new CreatedResult($"/api/companies/{companyId}/team-members/{result.TeamMemberId}/reports/{result.WeeklyReportId}", result);
        }

        [HttpPut]
        public ActionResult<WeeklyReport> Update([FromBody] WeeklyReport weeklyReport)
        {
            if (weeklyReport == null)
            {
                return new BadRequestObjectResult("WeeklyReport should not be null.");
            }
            var result = _repository.Update(weeklyReport);
            return new OkObjectResult(result);
        }

        [Route("{weeklyReportId}")]
        [HttpDelete]
        public ActionResult Delete([FromRoute] string teamMemberId, [FromRoute] string weeklyReportId)
        {
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

        [Route("to/{dateFrom}/{dateTo}")]
        [HttpGet]
        public ActionResult<List<object>> GetWeeklyReports ([FromRoute] string companyId, 
            [FromRoute] string teamMemberId, [FromRoute] string dateFrom, [FromRoute] string dateTo)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (!Regex.IsMatch(teamMemberId, @"^\d+$"))
            {
                return new BadRequestObjectResult("TeamMemberId should be positive integer.");
            }
            var result = _repository.GetWeeklyReports(Convert.ToInt32(companyId),
                Convert.ToInt32(teamMemberId), dateFrom, dateTo);
            if (result == null)
            {
                return new NotFoundObjectResult($"Reports Not Found");
            }
            return new OkObjectResult(result);
        }
    }
}