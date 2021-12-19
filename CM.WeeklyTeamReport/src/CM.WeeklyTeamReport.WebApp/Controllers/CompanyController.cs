using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> _repository;

        public CompanyController(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [Route("")]
        [HttpGet]
        public ActionResult<List<Company>> ReadAll()
        {
            var result = _repository.ReadAll();
            if (result.Count == 0)
            {
                return new NoContentResult();
            }
            return new OkObjectResult(result);
        }

        [Route("{companyId}")]
        [HttpGet]
        public ActionResult<Company> Read(string companyId)
        {
            if (!Regex.IsMatch(companyId, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            var result = _repository.Read(Convert.ToInt32(companyId));
            if (result == null)
            {
                return new NotFoundObjectResult($"Company {companyId} Not Found");
            }
            return new OkObjectResult(result);
        }

        [HttpPost]
        public ActionResult<Company> Create([FromBody] Company company)
        {
            if (company == null)
            {
                return new BadRequestObjectResult("Company should not be null.");
            }
            var result = _repository.Create(company);
            return new CreatedResult($"/api/companies/{result.CompanyId}", result);
        }

        [HttpPut]
        public ActionResult<Company> Update([FromBody] Company company)
        {
            if (company == null)
            {
                return new BadRequestObjectResult("Company should not be null.");
            }
            var result = _repository.Update(company);
            return new OkObjectResult(result);
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (!Regex.IsMatch(id, @"^\d+$"))
            {
                return new BadRequestObjectResult("CompanyId should be positive integer.");
            }
            if (_repository.Read(Convert.ToInt32(id)) == null)
            {
                return new NotFoundObjectResult($"Company {id} Not Found");
            }
            _repository.Delete(Convert.ToInt32(id));
            return new OkObjectResult($"Company {id} is deleted.");
        }
    }
}
