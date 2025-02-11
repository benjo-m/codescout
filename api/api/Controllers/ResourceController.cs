using api.DTOs.Company;
using api.DTOs.Resource;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    private readonly ResourceListService resourceListService;

    public ResourceController(ResourceListService resourceListService)
    {
        this.resourceListService = resourceListService;
    }

    [HttpGet("companies")]
    public CompanyResponse GetCompanies()
    {
        return resourceListService.GetCompanies();
    }

    [HttpGet("technologies")]
    public TechnologyResponse GetTechnologies()
    {
        return resourceListService.GetTechnologies();
    }

    [HttpGet("positions")]
    public PositionResponse GetPositions()
    {
        return resourceListService.GetPositions();
    }

    [HttpGet("workArrangements")]
    public WorkArrangementResponse GetWorkArrangements()
    {
        return resourceListService.GetWorkArrangements();
    }
}