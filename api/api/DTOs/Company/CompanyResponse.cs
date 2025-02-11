namespace api.DTOs.Company;

public class CompanyResponse
{
    public int Count { get; set; }
    public List<CompanyDto> Companies { get; set; }
}