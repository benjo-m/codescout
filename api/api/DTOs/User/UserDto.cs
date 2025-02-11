using api.DTOs.Award;
using api.DTOs.Company;
using api.Models;

namespace api.DTOs.User;

public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public CompanyDto? Company { get; set; }
    public List<AwardDto>? Awards { get; set; } = new();
    public string? Biography { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public Socials Socials { get; set; }

    public UserDto(Models.User user)
    {
        Username = user.Username;
        Email = user.Email;
        Role = user.Role.Name;
        Company = new CompanyDto(user.Company);
        foreach (var award in user.Awards)
        {
            Awards.Add(new AwardDto(award));
        }
        Biography = user.Biography;
        TwoFactorEnabled = user.TwoFactorEnabled;
        Socials = user.Socials;
    }
}