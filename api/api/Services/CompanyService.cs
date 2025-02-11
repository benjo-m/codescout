using api.Data;
using api.DTOs.Company;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class CompanyService
{
    private readonly ApplicationContext _context;

    public CompanyService(ApplicationContext context)
    {
        _context = context;
    }

    private bool isValidCompany(CompanyDto company)
    {
        if (_context.Companies.Where(x => x.Name == company.Name).Any()) return false;

        if (!company.Address.isValidAddress()) return false;

        if (String.IsNullOrWhiteSpace(company.Name)) return false;

        return true;
    }

    public CompanyDto RegisterCompany(CompanyDto companyDto)
    {
        if (!isValidCompany(companyDto))
        {
            return null;
        }

        Company company = new()
        {
            Name = companyDto.Name,
            Address = companyDto.Address
        };

        _context.Add(company);
        _context.SaveChanges();

        return companyDto;
    }
}