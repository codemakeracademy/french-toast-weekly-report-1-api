using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.WebApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.WebApp.Tests
{
    public class CompanyControllerTests
    {
        [Fact]
        public void ShouldReturnAllCompanies()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<Company>()
                {
                    new Company(),
                    new Company(),
                });
            var controller = fixture.GetCompanyController();
            var actionResult = (OkObjectResult)controller.ReadAll().Result;
            var companies = (List<Company>)actionResult.Value;
            companies.Should().NotBeNull();
            companies.Should().HaveCount(2);
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.CompanyRepository.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public void ShouldReturnNoCompanies()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<Company>() { });
            var controller = fixture.GetCompanyController();
            var actionResult = (NoContentResult)controller.ReadAll().Result;
            actionResult.Should().BeOfType<NoContentResult>();
            actionResult.StatusCode.Should().Be(204);
            fixture.CompanyRepository.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public void ShouldReturnCompanyById()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(48))
                .Returns(new Company());
            var controller = fixture.GetCompanyController();
            var actionResult = (OkObjectResult)controller.Read("48").Result;
            var company = (Company)actionResult.Value;
            company.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.CompanyRepository.Verify(x => x.Read(48), Times.Once);
        }

        [Fact]
        public void ShouldReturnNoCompanyById()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(48123123))
                .Returns((Company)null);
            var controller = fixture.GetCompanyController();
            var actionResult = (NotFoundObjectResult)controller.Read("48123123").Result;
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.CompanyRepository.Verify(x => x.Read(48123123), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestCompanyByIncorrectId()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(-48))
                .Returns((Company)null);
            var controller = fixture.GetCompanyController();
            var actionResult = (BadRequestObjectResult)controller.Read("-48").Result;
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.CompanyRepository.Verify(x => x.Read(-48), Times.Never);
        }

        [Fact]
        public void ShouldCreateCompany()
        {
            var company = new Company();
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns(company);
            var controller = fixture.GetCompanyController();
            var actionResult = (CreatedResult)controller.Create(company).Result;
            company = (Company)actionResult.Value;
            company.Should().NotBeNull();
            actionResult.Should().BeOfType<CreatedResult>();
            actionResult.StatusCode.Should().Be(201);
            fixture.CompanyRepository.Verify(x => x.Create(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestCreateIncorrectCompany()
        {
            var company = (Company)null;
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns(company);
            var controller = fixture.GetCompanyController();
            var actionResult = (BadRequestObjectResult)controller.Create(company).Result;
            company.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.CompanyRepository.Verify(x => x.Create(company), Times.Never);
        }

        [Fact]
        public void ShouldUpdateCompany()
        {
            var company = new Company();
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Update(company))
                .Returns(company);
            var controller = fixture.GetCompanyController();
            var actionResult = (OkObjectResult)controller.Update(company).Result;
            company = (Company)actionResult.Value;
            company.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.CompanyRepository.Verify(x => x.Update(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestUpdateIncorrectCompany()
        {
            var company = (Company)null;
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Update(company))
                .Returns(company);
            var controller = fixture.GetCompanyController();
            var actionResult = (BadRequestObjectResult)controller.Update(company).Result;
            company.Should().BeNull();
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.CompanyRepository.Verify(x => x.Update(company), Times.Never);
        }

        [Fact]
        public void ShouldDeleteCompany()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Delete(48));
            fixture.CompanyRepository
                .Setup(x => x.Read(48))
                .Returns(new Company());
            var controller = fixture.GetCompanyController();
            var actionResult = (OkObjectResult)controller.Delete("48");
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.StatusCode.Should().Be(200);
            fixture.CompanyRepository.Verify(x => x.Delete(48), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundDeleteIncorrectCompany()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Delete(48));
            fixture.CompanyRepository
                .Setup(x => x.Read(48))
                .Returns((Company)null);
            var controller = fixture.GetCompanyController();
            var actionResult = (NotFoundObjectResult)controller.Delete("48");
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.StatusCode.Should().Be(404);
            fixture.CompanyRepository.Verify(x => x.Delete(48), Times.Never);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteIncorrectCompanyId()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Delete(-48));
            var controller = fixture.GetCompanyController();
            var actionResult = (BadRequestObjectResult)controller.Delete("-48");
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.StatusCode.Should().Be(400);
            fixture.CompanyRepository.Verify(x => x.Delete(48), Times.Never);
        }
    }

    public class CompanyControllerFixture
    {
        public Mock<IRepository<Company>> CompanyRepository { get; set; }
        public CompanyControllerFixture()
        {
            CompanyRepository = new Mock<IRepository<Company>>();
        }

        public CompanyController GetCompanyController()
        {
            return new CompanyController(CompanyRepository.Object);
        }
    }
}
