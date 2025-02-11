using api.DTOs.Award;
using api.Models;

namespace api.DTOs.User;

public class UserStats
{
    public int ProjectsSubmitted { get; set; }
    public int AwardsUnlocked { get; set; }
    public int TechnologiesWorkedWith { get; set; }
    public Dictionary<string, int> TopTechnologies { get; set; } = new();
}