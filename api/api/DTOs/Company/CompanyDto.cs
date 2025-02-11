using api.Models;
using System.Text.Json.Serialization;

namespace api.DTOs.Company;

public class CompanyDto
{
    public string Name { get; set; }
    public Address Address { get; set; }

    [JsonConstructor]
    public CompanyDto()
    { }

    public CompanyDto(Models.Company company)
    {
        if (company != null)
        {
            Name = company.Name;
            Address = company.Address;
        }
    }
}