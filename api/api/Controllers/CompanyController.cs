using api.DTOs.Company;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public ActionResult<CompanyDto> RegisterCompany(CompanyDto companyDto)
    {
        CompanyDto company = _companyService.RegisterCompany(companyDto);

        if (company == null)
        {
            return BadRequest();
        }

        return Ok(company);
    }
}