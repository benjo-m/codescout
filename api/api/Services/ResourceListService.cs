using api.Data;
using api.DTOs.Company;
using api.DTOs.Resource;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class ResourceListService
{
    private readonly ApplicationContext _context;
    private readonly AuthService _authService;

    public ResourceListService(ApplicationContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public CompanyResponse GetCompanies()
    {
        List<Company> companies = _context.Companies.Include(x => x.Address).ToList();

        List<CompanyDto> companyDtos = new();

        foreach (var copmany in companies)
        {
            companyDtos.Add(new CompanyDto(copmany));
        }

        CompanyResponse companyResponse = new()
        {
            Count = companyDtos.Count,
            Companies = companyDtos
        };

        return companyResponse;
    }

    public TechnologyResponse GetTechnologies()
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        List<Technology> technologies = _context.Technologies.ToList();

        List<string> techNameList = new();

        foreach (var tech in technologies)
        {
            techNameList.Add(tech.Name);
        }

        TechnologyResponse technologyResponse = new()
        {
            Count = technologies.Count,
            Technologies = techNameList
        };

        return technologyResponse;
    }

    public PositionResponse GetPositions()
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        List<Position> positions = _context.Positions.ToList();

        List<string> positionTitles = new();

        foreach (var pos in positions)
        {
            positionTitles.Add(pos.Title);
        }

        PositionResponse positionResponse = new()
        {
            Count = positions.Count,
            Positions = positionTitles
        };

        return positionResponse;
    }


    public WorkArrangementResponse GetWorkArrangements()
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        List<WorkArrangement> workArrangements = _context.WorkArrangements.ToList();

        List<string> workArrangementsNamesList = new();

        foreach (var wa in workArrangements)
        {
            workArrangementsNamesList.Add(wa.Name);
        }

        WorkArrangementResponse workArrangementResponse = new()
        {
            Count = workArrangements.Count,
            WorkArrangements = workArrangementsNamesList
        };

        return workArrangementResponse;
    }
}