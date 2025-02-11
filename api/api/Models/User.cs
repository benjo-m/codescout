using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    public Company? Company { get; set; }
    public List<Award> Awards { get; set; } = new();
    public List<Project> ProjectsAppliedTo { get; set; } = new();
    public string? Biography { get; set; }
    public bool TwoFactorEnabled { get; set; } = false;
    public Socials Socials { get; set; } = new();
}